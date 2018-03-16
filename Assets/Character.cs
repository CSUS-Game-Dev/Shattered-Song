using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character{

	private CharacterAesthetics characterAesthetics;
	private CharacterTempoManager tempoManager;
	private CharacterStats characterStats;
	private SkillList skills;


	public Character(){

		tempoManager = new CharacterTempoManager();
	}

	public Character(string characterName){

	}

	public Character(int characterID){

	}

	public void createGenericCharacter(CharacterTempoManager ctm, CharacterStats cs, SkillList s){

	}

}