using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPage : Page {
	public GameObject shopSlotPrefab;
	public GameObject content;
	public List<ShopItem> shopItems = new List<ShopItem>();

	void Start(){
		int x = 0;
		int y = 0;
		for(int i=0;i<shopItems.Count;i++, x++){
			GameObject o = Instantiate(shopSlotPrefab);
			o.GetComponent<ShopItemHandler>().display(shopItems[i]);
			o.SetActive(true);
			o.transform.SetParent(content.transform);
			RectTransform rect = o.GetComponent<RectTransform>();
			rect.anchoredPosition = Vector3.zero;
			rect.anchoredPosition += new Vector2(((rect.sizeDelta.x+10)*x), (rect.sizeDelta.y+10)*-y);

			if(rect.anchoredPosition.x >= content.GetComponent<RectTransform>().sizeDelta.x){
				x = -1;
				y++;
			}
		}
	}
}
