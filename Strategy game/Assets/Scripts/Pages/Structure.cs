using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour, IPlaceAble {

	void Start () {
		
	}

	void Update () {
		
	}

	public void place(Territory territory){
		territory.place(this);
		//this.transform.position = territory.gameObject.transform.position;
	}
}
