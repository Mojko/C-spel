using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Ability", menuName="Ability")]
public class AbilityData : ScriptableObject {
	public string name;
	public int damage;
	public int range;
	public AbilityType type;
	public bool neutral;
}
