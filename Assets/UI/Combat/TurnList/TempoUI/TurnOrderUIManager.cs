using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnOrderUIManager : MonoBehaviour {

	private int numTurnsDisplayed = 7;

	public List<TurnUI> turnsDisplayed;
	public List<PlayerTurn> turns;
	public Dictionary<string, Image> pictures;


	public void setup(){
		turnsDisplayed = new List<TurnUI>();
		turns = new List<PlayerTurn>();

	
		for(int i = 0; i < numTurnsDisplayed; i++){
			turnsDisplayed.Add(transform.Find("ListItem" + (i + 1).ToString()).GetComponent<TurnUI>());
			turnsDisplayed[i].setup();
			turnsDisplayed[i].setOrderNumber(i + 1);
		}
	}

	public void displayNewTurns(List<PlayerTurn> newTurns){
		turns = newTurns;
		for(int i = 0; i < turnsDisplayed.Count; i++){
			turnsDisplayed[i].updateUI(turns[i]);
		}
	} 

	public void addCharacter(Character c){

	}

}
