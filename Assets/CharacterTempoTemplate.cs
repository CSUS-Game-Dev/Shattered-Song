using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTempoTemplate {

	public static int DEFAULT_TEMPO = 9;
	public static string DEFAULT_NAME = "Generic_Name";

	public int tempo;
	public int nextTurnModifier;
	public string charName;
	private int timeWaiting;


	public CharacterTempoTemplate(){
		tempo = DEFAULT_TEMPO;
		charName = DEFAULT_NAME;
		nextTurnModifier = 0;
		timeWaiting = 0;
	}

	public CharacterTempoTemplate(int startTempo, string name){
		tempo = startTempo;
		charName = name;
		nextTurnModifier = 0;
		timeWaiting = 0;
	}

	public void takeTurn(){
		nextTurnModifier = 0;
		timeWaiting = 0;
	}

	public void takeTurn(int nextModifier){
		nextTurnModifier = nextModifier;
		timeWaiting = 0;
	}

	public int getNextTurnWait(){
		return tempo + nextTurnModifier;
	}

	public void incTimeWaiting(){
			timeWaiting++;
	}

	public int timeToNextTurn(){
		int result = tempo + nextTurnModifier - timeWaiting;
		if(result < 0){ result = 0;}
		return result;
	}

	//Quick test to see if the player is ready to go
	public bool readyToGo(){
		bool ready;
		if(timeWaiting > tempo + nextTurnModifier){
			ready = true;
		}
		else{ ready = false;}

		return ready;
	}

	public int extraTimeWaiting(){
		return timeWaiting - (tempo + nextTurnModifier);
	}

}
