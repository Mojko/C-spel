using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemHandler : MonoBehaviour {
	public Text cost;
	public Image image;
	public Map map;
	private ShopItem item;
	private GameObject purchasedObject;
	private GridMover gridMover;

	void Start(){
		gridMover = Camera.main.GetComponent<GridMover>();
	}

	public void display(ShopItem item){
		cost.text = item.cost.ToString();
		image.sprite = item.sprite;
		this.item = item;
	}

	public void onPurchase(){
		item.purchase();
		purchasedObject = Instantiate(item.prefab);
		Debug.Log("onpurchase");
	}

	void Update(){
		if(purchasedObject != null){
			purchasedObject.transform.position = gridMover.gridObject.transform.position;
			if(Input.GetMouseButtonDown(0)){
				purchasedObject.GetComponent<Structure>().place(map.getTerritory(gridMover.gridObject.transform.position));
				purchasedObject = null;
			}
		}
	}
}
