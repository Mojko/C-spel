using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New ShopItem", menuName="Shop Item")]
public class ShopItem : ScriptableObject {
	public int cost;
	public GameObject prefab;
	public Sprite sprite;

	public void purchase(){

	}
}
