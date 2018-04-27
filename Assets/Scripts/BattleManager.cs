using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour {

	private BattleGrid battleGrid;
	private List<Character> characters;
	private TurnManager turnManager;

	//Going to need a bunch of variables that will manage win/loss conditions
	//Maybe a new class that listens to the BattleManager for changes, and updates the UI accordingly
	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
