using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTempoTemplate {

	private int tempo;
	private int nextTurnModifier; 
	private int timeWaiting;

	public int Tempo
    {
        get
        {
            return tempo;
        }
    }

	public int NextTurnModifier
	{
		get{
			return nextTurnModifier;
		}
		set{
			nextTurnModifier = value;
		}
	}

	public CharacterTempoTemplate(){

	}

	public void takeTurn(){
		nextTurnModifier = 0;
	}

	public void takeTurn(int nextModifier){
		nextTurnModifier = nextModifier;
	}

	public int getNextTurnWait(){
		return tempo + nextTurnModifier;
	}


}
