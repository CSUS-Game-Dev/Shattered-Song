using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerTurn{
	
		public CharacterTempoManager character;

		//This is the turn number in the list i.e. the first, second, or third time in the list that the character moves.
		// The "time" the turn happens is equal to character.nextTurnModifier + (turnInList * character.tempo)
		public int turnInList;



		public PlayerTurn(CharacterTempoManager c, int turn){
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
