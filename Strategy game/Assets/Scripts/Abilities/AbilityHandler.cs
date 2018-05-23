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
			handleFireball(ability, from, to);
			break;
		case AbilityType.ATTACK:
			handleAttack(ability, from, to);
			break;
		}
	}

	private void handleMove(Ability ability, Territory from, Territory to){
		Vector3 pos = from.getUnit().gameObject.transform.position;
		from.getUnit().rotate(to);
		from.getUnit().move(to);
		//from.getUnit().gameObject.transform.position = new Vector3(to.gameObject.transform.position.x, pos.y, to.gameObject.transform.position.z);
	}

	private void handleFireball(Ability ability, Territory from, Territory to){
		GameObject o = Object.Instantiate(ability.gameObject);
		o.transform.position = new Vector3(to.gameObject.transform.position.x, to.gameObject.transform.position.y+0.5f, to.gameObject.transform.position.z);
		from.getUnit().rotate(to);
		if(to.getUnit() != null)
			to.getUnit().hurt();
	}

	private void handleAttack(Ability ability, Territory from, Territory to){
		//from.getUnit().getAnimator().SetTrigger("attack");
		from.getUnit().rotate(to);
		from.getUnit().basicAttack(to);
	}

	public static AbilityHandler getInstance(){
		if(instance == null)
			instance = new AbilityHandler();
		return instance;
	}
}
