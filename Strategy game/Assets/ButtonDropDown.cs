using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDropDown : MonoBehaviour {

	private bool up = true;
	private RectTransform rect;

	void Start(){
		rect = GetComponent<RectTransform>();
		rect.rotation = Quaternion.Euler(0,0,180);
	}

	public void toggle(){
		up ^= true;
		if(up)
			rect.rotation = Quaternion.Euler(0,0,180);
		else
			rect.rotation = Quaternion.Euler(0,0,0);
	}
}
