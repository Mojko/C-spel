using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour{
	public CharacterPage characterPage;
	public Unit mage;
	private Unit selected;

	void Start(){
	}

	public void selectUnit(Unit unit){
		this.selected = unit;
		characterPage.open(unit);
	}

	public Unit getSelectedUnit(){
		return selected;
	}

	public Unit createNewInstance(Unit unit){
		return Instantiate(unit).GetComponent<Unit>();
	}

	public void unSelectUnit(){
		if(selected != null){
			if(selected.hasPreparedAbility()){
				selected.removeAbility();
			}
			characterPage.close();
			Debug.Log("unselected " + selected);
		}
		this.selected = null;
	}

	public bool hasSelectedUnit(){
		return this.selected != null;
	}
}
