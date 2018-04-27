using System.Collections;
using System.Collections.Generic;

public class Stat {
	public string statName;
	public int baseValue;
	public int flatModifier;
	public float multModifier;

	public Stat(string name, int baseVal, int flatMod = 0, int multMod = 1){
		statName = name;
		baseValue = baseVal;
		flatModifier = flatMod;
		multModifier = multMod;
	}

	public int getCurrentValue(){
		return (int)((baseValue * multModifier) + flatModifier);
	}

	//Prints the CURRENT VALUE of the stat, not the base value
	public override string ToString(){
		string str = "";
		str += statName.ToUpper() + ": " + getCurrentValue().ToString();
		return str;
	}
}
