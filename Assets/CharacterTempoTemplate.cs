using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTempoTemplate {

	public int tempo
    {
        get
        {
            return tempo;
        }
    }

	public int nextTurnModifier
	{
		get{
			return nextTurnModifier;
		}
		set{
			nextTurnModifier = value;
		}
	}

	public string charName
	{
		get{
			return charName;
		}
		set{
			charName = value;
		}
	}

	public int timeWaiting
	{
		get{
			return timeWaiting;
		}
		set{
			timeWaiting = value;
		}
	}

	public CharacterTempoTemplate(){

	}

	public void takeTurn(){
		nextTurnModifier = 0;
		timeWaiting = 0;
	}

	public void takeTurn(int nextModifier){
		nextTurnModifier = nextModifier;
	}

	public int getNextTurnWait(){
		return tempo + nextTurnModifier;
	}

	public void incTimeWaiting(){
			timeWaiting++;
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
