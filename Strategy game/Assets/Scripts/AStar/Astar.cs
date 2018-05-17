using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Astar {

	private Node[] nodes;
	private Map map;
	public Node start;
	public Node end;
	private ArrayList path = new ArrayList();
	private bool finished = false;
	private int range;

	public Astar(Map map){
		Territory[] territories = map.getTerritories();
		this.map = map;
		nodes = new Node[territories.Length];
		for(int i=0;i<nodes.Length;i++){
			nodes[i] = new Node(new Vector3(territories[i].gameObject.transform.position.x, territories[i].gameObject.transform.position.z, 0));
		}
	}

	public Node[] findPath(Node start, Node end, int range){
		this.start = start;
		this.end = end;
		this.range = range;

		if(start.Equals(end)) return null;
		updateNodes(start);
		recursive(start);
		finished = true;
		return getPath();
	}

	public void recursive(Node current){
		float lowestFCost = 999;
		float lowestHCost = 999;
		Node lowest = current;
		//Node[] neighbours = lowest.findNeighbours(this)
		foreach(Node n in nodes){
			if(n.getFCost() == 0) continue;
			if(n.getFCost() < lowestFCost && !n.closed){
				lowestFCost = n.getFCost();
				lowestHCost = n.getHCost();
				lowest = n;
			}
		}
		if(!lowest.Equals(start))
			path.Add(lowest);

		if(lowest.Equals(end) || path.Count >= range){
			foreach(Node n in nodes){
				n.reset();
			}
		} else if(!lowest.Equals(end)){
			lowest.close();
			updateNodes(lowest);
			recursive(lowest);
		}
	}

	public void updateNodes(Node node){
		Node[] nodes = node.findNeighbours(this);

		foreach(Node n in nodes){
			if(n != null)
				n.update(start,end);
		}
	}

	public Territory[] toTerritories(Node[] nodes){
		if(nodes == null) return null;
		Territory[] territories = new Territory[nodes.Length];
		for(int i=0;i<nodes.Length;i++){
			territories[i] = map.getTerritory((int)nodes[i].pos.x, (int)nodes[i].pos.y);
		}
		return territories;
	}

	public Node getNode(float x, float y, float z){
		foreach(Node n in nodes){
			if(n.pos.x == x && n.pos.y == y && n.pos.z == z)
				return n;
		}
		return null;
	}

	public Node[] getPath(){
		Node[] r = (Node[])path.ToArray(typeof(Node));
		path.Clear();
		return r;
	}

	public bool getFinished(){
		return finished;
	}
}
