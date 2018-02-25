using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

	private List<CharacterTempoTemplate> characters;
	private List<PlayerTurn> turnOrder;

	// Use this for initialization
	void Start () {
		characters = new List<CharacterTempoTemplate>();
		turnOrder = new List<PlayerTurn>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("space")){
			characters[0].takeTurn();
			updateList();
		}
	}

	public void updateList(){
		sortByTurnOrder();
	}


	private void sortByTurnOrder(){
		List<PlayerTurn> newTurnOrder = new List<PlayerTurn>();


	}
	
	//Largely for development purposes
	//Helps us test whether or not the algorithm is working as intended
	public void printTurnOrder(){
		for(int i = 0; i < turnOrder.Count; i++){
			Debug.Log("Character " + characters[i].charName + "goes in turn position" + (i+1));
		}
	}

	public struct PlayerTurn{
		public CharacterTempoTemplate character;

		//This is the turn number in the list i.e. the first, second, or third time in the list that the character moves.
		// The "time" the turn happens is equal to character.nextTurnModifier + (turnInList * character.tempo)
		public int turnInList;

		public PlayerTurn(CharacterTempoTemplate c, int turn){
			turnInList = turn;
			character = c;
		}

		public bool isEqual(PlayerTurn p){
			if(p.character == character && p.turnInList == turnInList){
				return true;
			}
			else{
				return false;
			}
		}
	}

}
