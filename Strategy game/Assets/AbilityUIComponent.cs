using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUIComponent : MonoBehaviour {
	
	public Ability ability;

	public void setAbility(Ability ability){
		this.ability = ability;
	}

	public Ability getAbility(){
		return ability;
	}
}
