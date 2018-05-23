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
		Debug.Log("team: " + team + ", " + teamType);
	}

	public void start(){
		myTurn = true;
	}

	public void end(){
		myTurn = false;
		Debug.Log("round ended");
	}

	void LateUpdate(){
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
				if(territory.hasPlaceable() && territory.getUnit().isActive() && territory.getTeam().Equals(this.team)){
					game.unitManager.selectUnit(territory.getUnit());
					return;
				} 
			}
		} else if(EventSystem.current.IsPointerOverGameObject() && game.unitManager.characterPage.isClosed()){
			game.unitManager.unSelectUnit();
		}

		if(selected != null && selected.hasPreparedAbility()){
			GameObject mouseNode = Mouse.getInstance().hoverObject(); //This is the node the mouse is hovering over
			Territory[] pathTerritories = null;
			if(selected.getPreparedAbility().getData().range > 0){

				//Mark the gray nodes
				selected.markNearbyTerritories(game.map);

				//Mark the attack nodes
				if(mouseNode != null && !Mouse.getInstance().compareOldNode(mouseNode)){ //if mouse has moved

					int range = selected.getPreparedAbility().getData().range;

					pathTerritories = game.getPath(selected.getTerritory(), game.map.getTerritory((int)mouseNode.gameObject.transform.position.x, (int)mouseNode.gameObject.transform.position.z), range);

					if(selected.getPreparedAbility().getData().attackType == AbilityAttackType.SINGLE_TILE){
						Territory[] newPath = new Territory[1];
						newPath[0] = pathTerritories[pathTerritories.Length-1];
						pathTerritories = newPath;
					}

					if(pathTerritories != null){
						for(int i=0;i<pathTerritories.Length;i++){
							Territory above = pathTerritories[i].getAboveBlock(game.map, 1);
							if(above != null){
								if(!above.hasAboveBlock(game.map)){
									pathTerritories[i] = above;
								} else {
									pathTerritories[i] = null;
								}
							}
						}
					}

					if(!selected.getPreparedAbility().getData().neutral){
						game.map.markTerritories(pathTerritories);
					}
				}
			}

			if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()){
				if(game.map.getTerritory((int)mouseNode.transform.position.x, (int)mouseNode.transform.position.y, (int)mouseNode.transform.position.z).isMarked() && pathTerritories != null){
					selected.getPreparedAbility().execute(selected.getTerritory(), pathTerritories);
				}
				game.unitManager.unSelectUnit();
			}
			/*//Use ability
			if(Input.GetMouseButtonDown(0) && pathTerritories != null && ){
				selected.getPreparedAbility().execute(selected.getTerritory(), pathTerritories);
				game.unitManager.unSelectUnit();
			}*/
		}




		//Temporary spawning
		if(Input.GetMouseButtonDown(1)){
			GameObject o = Mouse.getInstance().hoverObject();
			if(o == null) return;
			Territory territory = game.map.getTerritory((int)o.transform.position.x, (int)o.transform.position.z);
			if(territory != null){
				if(!territory.hasPlaceable()){
					Unit u = null;
					if(Random.Range(0,2) == 1){
						u = game.unitManager.createNewInstance(game.unitManager.warrior);
					} else {
						u = game.unitManager.createNewInstance(game.unitManager.mage);
					}
					territory.place(u);
					units.Add(u);
				} 
			}
		}
	}

	public Team getTeam(){
		return team;
	}
}
