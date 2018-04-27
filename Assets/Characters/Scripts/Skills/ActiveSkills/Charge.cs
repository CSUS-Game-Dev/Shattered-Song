using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : MonoBehaviour, ActiveSkill {

	public SkillType getSkillType(){
		return SkillType.Active;
	}

	public void setup(BattleManager battleManager, Character owner){
    }

	//Display on the map where the skill will hit
	public void target(){
		
	}

	//Stop displaying target area
	public void untarget(){

	}

	//Activate the skill
	public void activate(){

	}
}
