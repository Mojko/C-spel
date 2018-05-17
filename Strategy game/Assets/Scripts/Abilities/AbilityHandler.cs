using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHandler {

	private static AbilityHandler instance = new AbilityHandler();

	public void handleAbility(Ability ability, Territory from, Territory to){
		switch(ability.getData().type){
		case AbilityType.MOVE:
			handleMove(ability, from, to);
			break;
		case AbilityType.FIREBALL:
			handleFireball(ability, to);
			break;
		}
	}

	private void handleMove(Ability ability, Territory from, Territory to){
		Vector3 pos = from.getUnit().gameObject.transform.position;
		from.getUnit().move(to);
		//from.getUnit().gameObject.transform.position = new Vector3(to.gameObject.transform.position.x, pos.y, to.gameObject.transform.position.z);
	}

	private void handleFireball(Ability ability, Territory territory){
		GameObject o = Object.Instantiate(ability.gameObject);
		o.transform.position = new Vector3(territory.gameObject.transform.position.x, o.transform.position.y, territory.gameObject.transform.position.z);
	}

	public static AbilityHandler getInstance(){
		if(instance == null)
			instance = new AbilityHandler();
		return instance;
	}
}
