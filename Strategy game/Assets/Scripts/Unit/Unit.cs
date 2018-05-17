using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[CreateAssetMenu(fileName = "New Unit", menuName = "Unit")]
public class Unit : MonoBehaviour {
	public Text damageText;
	private Team team;
	public int damage;
	public Ability[] abilities;
	private Territory territory;
	private Ability activeAbility;
	private bool active = true;
	private bool dead = false;

	private Renderer[] renderers;

	void Start(){
		this.renderers = new Renderer[transform.childCount];
		for(int i=0;i<renderers.Length;i++){
			renderers[i] = transform.GetChild(i).GetComponent<Renderer>();
		}
		setAbilities();
	}

	void Update(){
	}

	public void place(Territory territory){
		this.territory = territory;
		this.team = territory.getTeam();
		updatePosition();
	}

	public void select(){

	}

	public void move(Territory territory){
		this.territory.removeUnit(this);
		territory.setUnit(this, team);
		this.territory = territory;
		updatePosition();
	}

	public void updatePosition(){
		Vector3 pos = territory.gameObject.transform.position;
		transform.position = new Vector3(pos.x, pos.y + territory.gameObject.transform.lossyScale.y, pos.z);
	}

	public Renderer getRenderer(){
		return renderers[0];
	}

	public Renderer[] getRenderers(){
		return renderers;
	}

	public GameObject getGameObject(){
		return gameObject;
	}

	public Unit createNewInstance(){
		return Instantiate(this);
	}

	public void prepareAbility(Ability ability){
		this.activeAbility = ability;
	}

	public void toggleAbility(Ability ability){
		if(this.activeAbility == null)
			activeAbility = ability;
		else
			activeAbility = null;
	}

	public void attack(Territory territory){

	}

	public void markNearbyTerritories(Map map){
		Territory[] nearby = territory.getNearbyTerritories(map, activeAbility.getData().range);

		foreach(Territory t in nearby){
			if(!t.isMarked())
				t.mark(Territory.MarkType.MOVE);
		}
	}

	public void executeAbility(Territory territory){
		if(canAttack()){
			attack(territory);
		} else if(canMove()){
			move(territory);
		}
		this.activeAbility = null;
	}

	public bool hasPreparedAbility(){
		return this.activeAbility != null;
	}

	public Territory getTerritory(){
		return territory;
	}

	public Ability getPreparedAbility(){
		return this.activeAbility;
	}

	public bool canAttack(){
		if(this.activeAbility != null)
			return !this.activeAbility.getData().neutral;
		return false;
	}

	public bool canMove(){
		if(this.activeAbility != null)
			return this.activeAbility.getData().neutral;
		return false;
	}

	public void removeAbility(){
		this.activeAbility = null;
	}

	protected virtual void setAbilities(){

	}

	public bool isActive(){
		return active;
	}

	public void deactivate(){
		this.active = false;
	}

	public void hurt(){
		Text text = Instantiate(damageText);
		text.gameObject.SetActive(true);
		text.gameObject.transform.SetParent(transform.Find("Canvas"));
		text.gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
		text.transform.position = new Vector3(transform.position.x, transform.position.y+0.1f, transform.position.z);//damageText.transform.position;
		//text.gameObject.GetComponent<RectTransform>().anchoredPosition = damageText.GetComponent<RectTransform>().anchoredPosition;
		text.text = "4";
	}

	public void kill(){
		dead = true;
		Destroy(this.gameObject);
	}

	public void reset(){
		this.active = true;
	}

	public bool isAlive(){
		return !dead;
	}

	public Team getTeam(){
		return team;
	}
}
