using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page : MonoBehaviour {
	public PageController controller;
	private bool opened = false;

	public void open(){
		controller.closeAll();
		if(!controller.isOpened()){
			controller.open();
		}
		opened = true;
	}

	public virtual void close(){
		opened = false;
	}

	public bool isOpened(){
		return opened;
	}

	public bool isClosed(){
		return !opened;
	}

	public void toggle(){
		//opened ^= true;
		if(opened)
			close();
		else
			open();
	}
}
