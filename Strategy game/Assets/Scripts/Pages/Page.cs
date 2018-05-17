using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page : MonoBehaviour {
	public PageController controller;
	private bool opened = false;

	public void open(){
		controller.open();
		opened = true;
	}

	public virtual void close(){
		controller.close();
		opened = false;
	}

	public bool isOpened(){
		return opened;
	}

	public bool isClosed(){
		return !opened;
	}
}
