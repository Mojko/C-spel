using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team {
	public static Team NEUTRAL = new Team(Color.white);
	public static Team BLUE = new Team(Color.blue);
	public static Team RED = new Team(Color.red);

	/* This enum is only for the inspector and be able to choose teams from there */
	public enum Type {
		NEUTRAL, BLUE, RED
	}

	private Color color;

	public Team(Color color){
		this.color = color;
	}

	public Color GetColor(){
		return color;
	}

	public static Team parseType(Type type){
		switch(type){
		case Type.NEUTRAL:
			return Team.NEUTRAL;
		case Type.BLUE:
			return Team.BLUE;
		case Type.RED:
			return Team.RED;
		}
		return null;
	}
}