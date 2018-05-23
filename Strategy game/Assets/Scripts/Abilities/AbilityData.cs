using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityAttackType {
	PATH, SINGLE_TILE, RADIUS
}

[CreateAssetMenu(fileName="New Ability", menuName="Ability")]
public class AbilityData : ScriptableObject {
	public string name;
	public int damage;
	public int range;
	public AbilityType type;
	public AbilityAttackType attackType;
	public bool neutral;
}
