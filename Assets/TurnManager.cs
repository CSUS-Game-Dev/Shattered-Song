using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

	private List<CharacterTempoTemplates> characters;

	// Use this for initialization
	void Start () {
		characters = new List<CharacterTempoTemplates>();
		characters
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("space")){
			characters[0].takeTurn();
			updateList();
		}
	}

	public void updateList(){
		bool noCharacterIsReady = true;

		while(noCharacterIsReady){
		for(int i = 0; i < character.Count; i++){
			if()
		}
	}


}
