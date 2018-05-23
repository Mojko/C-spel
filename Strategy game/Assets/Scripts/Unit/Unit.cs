using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[CreateAssetMenu(fileName = "New Unit", menuName = "Unit")]
public class Unit : MonoBehaviour, IPlaceAble {
	public Text damageText;
	private Team team = Team.NEUTRAL;
	public int damage;
	public Ability[] abilities;
	private Territory territory;
	private Ability activeAbility;
	private bool active = true;
	private int moves = 2;
	private int startMoves = 2;
	private bool dead = false;

	private Renderer[] renderers;

	void Start(){
		this.renderers = new Renderer[transform.childCount];
		for(int i=0;i<renderers.Length;i++){
			renderers[i] = transform.GetChild(i).GetComponent<Renderer>();
		}
		setAbilities();
	}

	public void place(Territory territory){
		this.territory = territory;
		this.team = territory.getTeam();
		updatePosition();
	}

	public void select(){

	}

	public void move(Territory territory){
		this.territory.removePlaceable();
		//territory.setUnit(this, team);
		territory.place(this);
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

	public void markNearbyTerritories(Map map){
		Territory[] nearby = territory.getNearbyTerritories(map, activeAbility.getData().range);

		foreach(Territory t in nearby){
			if(!t.isMarked())
				t.mark(Territory.MarkType.MOVE);
		}
	}

	/*public void executeAbility(Territory territory){
		if(canAttack()){
			attack(territory);
		} else if(canMove()){
			move(territory);
		}
		this.activeAbility = null;
	}*/

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

	public void useMove(){
		this.moves--;
		if(moves <= 0)
			deactivate();
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
		this.moves = startMoves;
	}

	public bool isAlive(){
		return !dead;
	}

	public Team getTeam(){
		return team;
	}

	public Animator getAnimator(){
		return gameObject.GetComponent<Animator>();
	}

	public void rotate(Territory territory){
		gameObject.transform.LookAt(new Vector3(territory.gameObject.transform.position.x, gameObject.transform.position.y, territory.gameObject.transform.position.z));//gameObject.transform.rotation = Quaternion.LookRotation();
		snapRotation(90);
	}

	public void snapRotation(float degree){
		Vector3 euler = transform.rotation.eulerAngles;
		euler.x = Mathf.RoundToInt(euler.x/degree)*degree;
		euler.y = Mathf.RoundToInt(euler.y/degree)*degree;
		euler.z = Mathf.RoundToInt(euler.z/degree)*degree;
		transform.rotation = Quaternion.Euler(euler);
	}

	public void basicAttack(Territory territory){
		StartCoroutine(attack(territory));
		Debug.Log("basic attacked");
	}

	public IEnumerator attack(Territory territory){
		//if(territory.getUnit().gameObject == null) yield return null;
		Vector3 oldPos = gameObject.transform.position;
		Vector3 target = new Vector3(territory.gameObject.transform.position.x, gameObject.transform.position.y, territory.gameObject.transform.position.z);
		while(Vector3.Distance(gameObject.transform.position, target) > 0.1f){
			gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, target, 0.8f);
			//Debug.Log(gameObject.transform.position.x + ", " + gameObject.transform.position.z + " | " + target.x + ", " + target.z);
			yield return null;
		}
		yield return new WaitForSeconds(0.1f);
		while(Vector3.Distance(gameObject.transform.position, oldPos) >= 0.1f){
			//Debug.Log(Vector3.Distance(gameObject.transform.position, oldPos));
			gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, oldPos, 0.8f);
			yield return null;
		}
		gameObject.transform.position = oldPos;
	}
}
