using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Player : MonoBehaviour {
	public Team.Type teamType;
	private Team team;
	public bool CPU;
	private Game game;
	private List<Unit> units = new List<Unit>();
	public bool myTurn = false;

	public void initilize(Game game){
		this.team = Team.parseType(teamType);
		this.game = game;
	}

	public void start(){
		myTurn = true;
	}

	public void end(){
		myTurn = false;
		Debug.Log("round ended");
	}

	void Update(){
		for(int i=0;i<units.Count;i++){
			if(!units[i].isAlive()){
				units.Remove(units[i]);
			}

			if(myTurn == false){
				units[i].reset();
			}
		}

		if(this.game == null || myTurn == false) return;
		Debug.Log("wew");
		game.map.clear();
		Unit selected = game.unitManager.getSelectedUnit();

		if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()){		
			GameObject o = Mouse.getInstance().hoverObject();
			if(o == null) return;
			Territory territory = game.map.getTerritory((int)o.transform.position.x, (int)o.transform.position.z);
			if(territory != null){
				if(selected != null){
					if(selected.Equals(territory.getUnit())){
						game.unitManager.unSelectUnit();
						return;
					}
				}
				if(territory.hasUnit() && territory.getUnit().isActive() && territory.getUnit().getTeam().Equals(this.team)){
					game.unitManager.selectUnit(territory.getUnit());
					return;
				} 
			}
		}

		if(selected != null && selected.hasPreparedAbility()){
			Territory[] pathTerritories = null;
			if(selected.getPreparedAbility().getData().range > 0){

				//Mark the gray nodes
				selected.markNearbyTerritories(game.map);

				//Mark the attack nodes
				GameObject mouseNode = Mouse.getInstance().hoverObject(); //This is the node the mouse is hovering over
				if(mouseNode != null && !Mouse.getInstance().compareOldNode(mouseNode)){ //if mouse has moved

					pathTerritories = game.getPath(selected.getTerritory(), game.map.getTerritory((int)mouseNode.gameObject.transform.position.x, (int)mouseNode.gameObject.transform.position.z), selected.getPreparedAbility().getData().range);

					if(!selected.getPreparedAbility().getData().neutral){
						game.map.markTerritories(pathTerritories);
					}
				}
			}

			//Use ability
			if(Input.GetMouseButtonDown(0) && pathTerritories != null && !EventSystem.current.IsPointerOverGameObject()){
				selected.getPreparedAbility().execute(selected.getTerritory(), pathTerritories);
				game.unitManager.unSelectUnit();
			}
		}




		//Temporary spawning
		if(Input.GetMouseButtonDown(1)){
			GameObject o = Mouse.getInstance().hoverObject();
			if(o == null) return;
			Territory territory = game.map.getTerritory((int)o.transform.position.x, (int)o.transform.position.z);
			if(territory != null){
				if(!territory.hasUnit()){
					Unit u = game.unitManager.createNewInstance(game.unitManager.mage);
					territory.setUnit(u, team);
					units.Add(u);
				} 
			}
		}
	}
}
