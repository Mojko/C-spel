using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Game : MonoBehaviour {
	public Texture2D cursor;
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
		Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
		players[currentPlayer].start();
		foreach(Player p in players){
			p.initilize(this);
		}
	}

	public Territory[] getPath(Territory from, Territory to, int range){
		Node[] path = map.getPathFinder().findPath(map.getPathFinder().getNode(from.gameObject.transform.position.x, from.gameObject.transform.position.z, 0), map.getPathFinder().getNode(to.gameObject.transform.position.x, to.gameObject.transform.position.z, 0), range);
		return map.getPathFinder().toTerritories(path);
	}

	public Player getCurrentPlayer(){
		Debug.Log("Player:  " + players[currentPlayer]);
		return players[currentPlayer];
	}
}
