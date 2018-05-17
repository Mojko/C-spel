using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Map : MonoBehaviour {

	public UnitManager unitManager;
	private Territory[] territories;
	private Astar pathFinder;

	private void Start () {
		territories = new Territory[transform.childCount];
		for(int i = 0; i < territories.Length; i++){
			territories[i] = new Territory(Team.NEUTRAL, transform.GetChild(i).gameObject);
		}

		try {
			getTerritory(10, 9).capture(Team.RED);
			getTerritory(10, 8).capture(Team.RED);
			getTerritory(10, 7).capture(Team.RED);
			getTerritory(0, 0).capture(Team.BLUE);

		} catch(NullReferenceException e){
			Debug.LogError("Territory not found" + e.Message);
		}

		pathFinder = new Astar(this);

	}

	/*private void Update(){
		
		Unit selected = unitManager.getSelectedUnit();

		if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()){		
			GameObject o = Mouse.getInstance().hoverObject();
			if(o == null) return;
			Territory territory = getTerritory((int)o.transform.position.x, (int)o.transform.position.z);
			if(territory != null){
				if(selected != null){
					if(!selected.hasPreparedAbility()){
						unitManager.unSelectUnit();
					}
				}
				if(territory.hasUnit() && territory.getUnit().isActive()){
					clear();
					unitManager.selectUnit(territory.getUnit());
				} 
			}
		}

		if(selected != null && selected.hasPreparedAbility()){
			clear();
			Territory[] pathTerritories = null;
			if(selected.getPreparedAbility().getData().range > 0){
				GameObject mouseNode = Mouse.getInstance().hoverObject(); //This is the node the mouse is hovering over
				if(mouseNode != null && !Mouse.getInstance().compareOldNode(mouseNode)){ //if mouse has moved
					Vector3 currentTerritory = unitManager.getSelectedUnit().getTerritory().gameObject.transform.position;
					Debug.Log(selected.getPreparedAbility().getData().range);
					Node[] path = pathFinder.findPath(pathFinder.getNode(currentTerritory.x, currentTerritory.z, 0), pathFinder.getNode(mouseNode.transform.position.x, mouseNode.transform.position.z, 0), selected.getPreparedAbility().getData().range);
					pathTerritories = pathFinder.toTerritories(path);
					if(pathTerritories != null && !selected.getPreparedAbility().getData().neutral){
						foreach(Territory t in pathTerritories){
							t.mark(Territory.MarkType.ATTACK);
						}
					} else if(pathTerritories != null && selected.getPreparedAbility().getData().neutral){
						pathTerritories[pathTerritories.Length-1].mark(Territory.MarkType.ATTACK);
					}
				}
			}
			selected.markNearbyTerritories(this);

			if(Input.GetMouseButtonDown(0) && pathTerritories != null && !EventSystem.current.IsPointerOverGameObject()){
				selected.getPreparedAbility().execute(selected.getTerritory(), pathTerritories);
				unitManager.unSelectUnit();
				clear();
			}
		} else {
			clear();
		}




		//Temporary spawning
		if(Input.GetMouseButtonDown(1)){
			GameObject o = Mouse.getInstance().hoverObject();
			if(o == null) return;
			Territory territory = getTerritory((int)o.transform.position.x, (int)o.transform.position.z);
			if(territory != null){
				if(!territory.hasUnit()){
					territory.setUnit(unitManager.createNewInstance(unitManager.mage));
				} 
			}
		}
	}*/

	public Territory getTerritory(int x, int z){
		foreach(Territory t in territories){
			if(t.find(x,z)){
				return t;
			}
		}
		return null;
	}

	public void clear(){
		if(territories == null) return;
		foreach(Territory t in territories){
			t.unmark();
		}
	}

	public Territory[] getTerritories(){
		return territories;
	}

	public Astar getPathFinder(){
		return pathFinder;
	}

	public void markTerritories(Territory[] territories){
		if(territories != null){
			foreach(Territory t in territories){
				t.mark(Territory.MarkType.ATTACK);
			}
		}

		/*if(territories != null && !selected.getPreparedAbility().getData().neutral){
			foreach(Territory t in territories){
				t.mark(Territory.MarkType.ATTACK);
			}
		} else if(territories != null && selected.getPreparedAbility().getData().neutral){
			territories[territories.Length-1].mark(Territory.MarkType.ATTACK);
		}*/
	}
}
