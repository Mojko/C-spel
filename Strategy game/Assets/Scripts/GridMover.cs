using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMover : MonoBehaviour {

	public GameObject gridObject;
	private Vector3 pos = Vector3.zero;
	private bool moved = false;

	void Start () {
		
	}

	void Update () {
		moved = false;
		GameObject o = Mouse.getInstance().hoverObject();
		if(o != null){
			Debug.Log(o.tag);
			if(o.CompareTag("Grid")){
				if(pos != gridObject.transform.position){
					gridObject.transform.position = new Vector3(o.transform.position.x, o.transform.position.y+0.517f, o.transform.position.z);
					moved = true;
					Debug.Log("moved");
				}
				pos = o.transform.position;
			}
		}
	}

	public bool hasMoved(){
		return moved;
	}
}
