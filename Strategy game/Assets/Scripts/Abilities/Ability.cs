using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityType {
	MOVE, FIREBALL, ATTACK
}

public class Ability : MonoBehaviour {
	[SerializeField] private AbilityData data;

	private int useTimer = 0;
	private int territoryIndex = 0;
	private Territory[] territories;
	private Territory from;
	private bool executed = false;
	private int timer = 90;

	void Start(){

	}

	void Update(){
		if(timer > 0)
			timer--;
		else
			Destroy(gameObject);
		
		if(executed && territoryIndex < territories.Length && Time.frameCount % 10 == 0){
			execute(from, territories[territoryIndex]);
			territoryIndex++;
			Debug.Log(territoryIndex);
		}
	}

	public void execute(Territory from, Territory[] territories){
		from.getUnit().useMove();
		GameObject o = Instantiate(this.gameObject);
		o.transform.position = new Vector3(territories[0].gameObject.transform.position.x, o.transform.position.y, territories[0].gameObject.transform.position.z);
		Ability a = o.GetComponent<Ability>();
		a.activate(from, territories);
	}

	private void execute(Territory from, Territory territory){
		AbilityHandler.getInstance().handleAbility(this, from, territory);
	}

	private void activate(Territory from, Territory[] territories){
		this.territories = territories;
		this.from = from;
		executed = true;
	}

	public bool hasExecuted(){
		if(territories != null)
			return territoryIndex >= territories.Length;
		return false;
	}

	public AbilityData getData(){
		return data;
	}
}
