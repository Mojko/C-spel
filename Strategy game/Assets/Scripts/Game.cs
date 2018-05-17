using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Game : MonoBehaviour {
	public Map map;
	public UnitManager unitManager;
	public Player[] players;
	private int currentPlayer;

	public void endTurn(){
		players[currentPlayer].end();
		currentPlayer++;
		if(currentPlayer >= players.Length) currentPlayer = 0;
		players[currentPlayer].start();
		Debug.Log(currentPlayer);
	}

	void Start(){
		players[currentPlayer].start();
		foreach(Player p in players){
			p.initilize(this);
		}
	}

	/*void Update(){
		map.clear();
		Unit selected = unitManager.getSelectedUnit();

		if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()){		
			GameObject o = Mouse.getInstance().hoverObject();
			if(o == null) return;
			Territory territory = map.getTerritory((int)o.transform.position.x, (int)o.transform.position.z);
			if(territory != null){
				if(selected != null){
					if(selected.Equals(territory.getUnit())){
						unitManager.unSelectUnit();
						return;
					}
				}
				if(territory.hasUnit() && territory.getUnit().isActive()){
					unitManager.selectUnit(territory.getUnit());
				} 
			}
		}

		if(selected != null && selected.hasPreparedAbility()){
			Territory[] pathTerritories = null;
			if(selected.getPreparedAbility().getData().range > 0){

				//Mark the gray nodes
				selected.markNearbyTerritories(map);

				//Mark the attack nodes
				GameObject mouseNode = Mouse.getInstance().hoverObject(); //This is the node the mouse is hovering over
				if(mouseNode != null && !Mouse.getInstance().compareOldNode(mouseNode)){ //if mouse has moved
					
					pathTerritories = getPath(selected.getTerritory(), map.getTerritory((int)mouseNode.gameObject.transform.position.x, (int)mouseNode.gameObject.transform.position.z), selected.getPreparedAbility().getData().range);

					if(!selected.getPreparedAbility().getData().neutral){
						map.markTerritories(pathTerritories);
					}
				}
			}

			//Use ability
			if(Input.GetMouseButtonDown(0) && pathTerritories != null && !EventSystem.current.IsPointerOverGameObject()){
				selected.getPreparedAbility().execute(selected.getTerritory(), pathTerritories);
				unitManager.unSelectUnit();
			}
		}




		//Temporary spawning
		if(Input.GetMouseButtonDown(1)){
			GameObject o = Mouse.getInstance().hoverObject();
			if(o == null) return;
			Territory territory = map.getTerritory((int)o.transform.position.x, (int)o.transform.position.z);
			if(territory != null){
				if(!territory.hasUnit()){
					territory.setUnit(unitManager.createNewInstance(unitManager.mage));
				} 
			}
		}
	}*/

	public Territory[] getPath(Territory from, Territory to, int range){
		Node[] path = map.getPathFinder().findPath(map.getPathFinder().getNode(from.gameObject.transform.position.x, from.gameObject.transform.position.z, 0), map.getPathFinder().getNode(to.gameObject.transform.position.x, to.gameObject.transform.position.z, 0), range);
		return map.getPathFinder().toTerritories(path);
	}
}
