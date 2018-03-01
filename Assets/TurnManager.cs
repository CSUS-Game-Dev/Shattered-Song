using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

	private List<CharacterTempoTemplate> characters;
	private List<PlayerTurn> turnOrder;
	private static int TURN_TRACKER_COUNT = 10;

	// Use this for initialization
	void Start () {
		characters = new List<CharacterTempoTemplate>();
		turnOrder = new List<PlayerTurn>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("space")){
			takeTurn();
		}
		if(Input.GetKeyDown("w")){
			takeTurn(10);
		}
		if(Input.GetKeyDown("s")){
			characters.Add(new CharacterTempoTemplate());
			characters.Add(new CharacterTempoTemplate(4, "Raven"));
			characters.Add(new CharacterTempoTemplate(5, "Ignis"));
			updateList();
			Debug.Log("TurnOrder list length: " + turnOrder.Count);
		}
		if(Input.GetKeyDown("p")){
			printTurnOrder();
		}
	}

	public void updateList(){
		sortByTurnOrder();
	}

	//Standard turn being taken where the action taken has no effect on the tempo for next turn
	public void takeTurn(){
		//while the character first in the turn order is not ready to go, update time
		if(characters.Count > 0 || turnOrder.Count > 0){
			while(!turnOrder[0].character.readyToGo()){
				for(int i = 0; i < characters.Count; i++){
					characters[i].incTimeWaiting();
				}
			}

			turnOrder[0].character.takeTurn();
			updateList();
		}
		else{
			Debug.Break();
			Debug.Log("Either the characters or the turnorder list is empty.");
		}
	}

	//The int passed 
	public void takeTurn(int nextTurnMod ){
		if(characters.Count > 0 || turnOrder.Count > 0){
			while(!turnOrder[0].character.readyToGo()){
				for(int i = 0; i < characters.Count; i++){
					characters[i].incTimeWaiting();
				}
			}

			turnOrder[0].character.takeTurn(nextTurnMod);
			updateList();
		}
		else{
			Debug.Break();
			Debug.Log("Either the characters or the turnorder list is empty.");
		}
	}

	private void sortByTurnOrder(){
		List<PlayerTurn> newTurnOrder = new List<PlayerTurn>();

		while(newTurnOrder.Count < TURN_TRACKER_COUNT){
			newTurnOrder.Add(findLowestTurnCostNotInList(newTurnOrder));
		}

		turnOrder = newTurnOrder;
	}
	
	public PlayerTurn findLowestTurnCostNotInList(List<PlayerTurn> newTurnOrder){
		//Temp turn while we iterate through the characters list
		PlayerTurn newTurn;
		PlayerTurn resultTurn = new PlayerTurn();
		resultTurn.turnInList = int.MaxValue;

		//Finds the least cost turn for each character in the characters list
		//Returns the one that costs the least that is NOT already in the turnOrder list
		//Right now, in the case of a tie, the character that comes first in the 'characters' list goes first
		for(int i = 0; i < characters.Count; i++){

			newTurn.character = characters[i];
			newTurn.turnInList = 0;

			while(playerTurnInList(newTurn, newTurnOrder)){
				newTurn.turnInList++;
			}

			if(resultTurn.character == null || newTurn.timeUntilTurn() < resultTurn.timeUntilTurn()){
				resultTurn = newTurn;
			}
		}

		return resultTurn;
	}

	//returns true if the PlayerTurn is in the list
	private bool playerTurnInList(PlayerTurn playerTurn, List<PlayerTurn> turnList){

		for(int i = 0; i < turnList.Count; i++){
			if(turnList[i].isEqual(playerTurn)){
				return true;
			}
		}
		return false;
	}

	//Largely for development purposes
	//Helps us test whether or not the algorithm is working as intended
	public void printTurnOrder(){
		for(int i = 0; i < turnOrder.Count; i++){
			Debug.Log(turnOrder[i].character.charName + " goes at turn: " + (i+1));
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

		public int timeUntilTurn(){
			return character.timeToNextTurn() + character.tempo * turnInList;
		}
	}

}
