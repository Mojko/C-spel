using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour {

	public float speed;
	public Axis axis;

	public enum Axis{
		X,Y,Z
	}


	void Update () {
		switch(axis){
		case Axis.X:
			float x = this.transform.position.x;
			x += speed;
			this.transform.position = new Vector3(x, this.transform.position.y, this.transform.position.z);
			break;
		case Axis.Y:
			float y = this.transform.position.y;
			y += speed;
			this.transform.position = new Vector3(this.transform.position.x, y, this.transform.position.z);
			break;
		case Axis.Z:
			float z = this.transform.position.z;
			z += speed;
			this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, z);
			break;
		}
	}
}
