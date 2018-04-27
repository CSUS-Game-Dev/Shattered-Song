using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour{

	private CharacterAesthetics characterAesthetics;
	private CharacterTempoManager tempoManager;
	private CharacterStats characterStats;
	private SkillList skills;

	private bool isSetup = false;

	void Start(){

	}

	public void setup(int character_ID){

	}

	public void setup(string characterName){
		
	}

	public void createGenericCharacter(CharacterTempoManager ctm, CharacterStats cs, SkillList s, CharacterAesthetics ca){

	}

}