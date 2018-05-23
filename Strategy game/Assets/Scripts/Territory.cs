using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Territory {
	public GameObject gameObject;
	private Renderer renderer;
	private Team team;
	//private Unit unit;
	//private Structure structure;
	private IPlaceAble placeable;
	private bool marked = false;
	private Game game;

	public Territory(Team owner, GameObject node, Game game){
		this.team = owner;
		this.gameObject = node;
		this.renderer = node.transform.GetChild(0).GetComponent<Renderer>();
		ColorUtility.setColor(renderer, team.GetColor());
		this.game = game;
	}

	public class MarkType {
		public static readonly int MOVE = 2;
		public static readonly int ATTACK = 1; 
	}

	public void capture(Team team){
		this.team = team;
		ColorUtility.setColor(renderer, team.GetColor());
	}

	public bool find(int x, int y, int z){
		return gameObject.transform.position.x == x && gameObject.transform.position.y == y && gameObject.transform.position.z == z;
	}

	public void mark(int type){
		float r;
		r = team.GetColor().r/(type*2);
		float g = team.GetColor().g/4;
		float b = team.GetColor().b/4;
		ColorUtility.setColor(renderer, new Color(r,g,b));
		marked = true;
	}

	public void unmark(){
		ColorUtility.setColor(renderer, team.GetColor());
		marked = false;
	}

	/*public bool hasUnit(){
		return unit != null;
	}

	public void setUnit(Unit unit, Team team){
		capture(team);
		this.unit = unit;
		this.unit.place(this);
	}

	public void removeUnit(Unit unit){
		this.unit = null;
	}*/

	public Territory[] getNearbyTerritories(Map map, int radius){
		ArrayList nearby = new ArrayList();
		for(int x=(int)gameObject.transform.position.x-radius;x<(int)gameObject.transform.position.x+(radius+1);x++){
			for(int z=(int)gameObject.transform.position.z-radius;z<(int)gameObject.transform.position.z+(radius+1);z++){
				Territory t = map.getTerritory(x,0,z);
				if(t != null)
					nearby.Add(t);
			}
		}

		Territory[] territories = (Territory[])nearby.ToArray(typeof(Territory));
		foreach(Territory te in territories){
			Territory above = te.getAboveBlock(map, 1);
			if(above != null){
				nearby.Remove(te);
				if(above.hasAboveBlock(map))
					continue;
				nearby.Add(above);
			}
			/*Territory above = map.getTerritory((int)te.gameObject.transform.position.x, (int)te.gameObject.transform.position.y+1, (int)te.gameObject.transform.position.z);
			if(above != null){
				nearby.Remove(te);
				nearby.Add(above);
			}*/
		}

		/*for(int x=(int)gameObject.transform.position.x-radius;x<(int)gameObject.transform.position.x+(radius+1);x++){
			for(int y=(int)gameObject.transform.position.y-radius;y<(int)gameObject.transform.position.y+(radius+1);y++){
				for(int z=(int)gameObject.transform.position.z-radius;z<(int)gameObject.transform.position.z+(radius+1);z++){
					Territory t = map.getTerritory(x,y,z);
					if(t != null)
						nearby.Add(t);
				}
			}
		}*/
		return (Territory[])nearby.ToArray(typeof(Territory));
	}

	public static void parse(Node[] nodes){

	}

	/*public Unit getUnit(){
		return unit;
	}*/

	public bool isMarked(){
		return marked;
	}

	public Team getTeam(){
		return team;
	}

	public Territory getTopBlock(Map map){
		int i = 1;
		Territory above = null;
		Territory t = null;
		while((t = map.getTerritory((int)gameObject.transform.position.x, (int)gameObject.transform.position.y+i, (int)gameObject.transform.position.z)) != null){
			i++;
			above = t;
		}
		return above;
	}

	public Territory getAboveBlock(Map map, int range){
		Territory above = map.getTerritory((int)gameObject.transform.position.x, (int)gameObject.transform.position.y+range, (int)gameObject.transform.position.z);
		return above;
	}

	public bool hasAboveBlock(Map map){
		Territory above = map.getTerritory((int)gameObject.transform.position.x, (int)gameObject.transform.position.y+1, (int)gameObject.transform.position.z);
		return above != null;
	}

	public void place(IPlaceAble placeable){
		if(this.placeable == null){
			capture(game.getCurrentPlayer().getTeam());
			this.placeable = placeable;
		}
	}

	public void removePlaceable(){
		this.placeable = null;
	}

	public Unit getUnit(){
		return placeable as Unit;
	}

	public Structure getStructure(){
		return placeable as Structure;
	}

	public bool hasPlaceable(){
		return placeable != null;
	}
}
