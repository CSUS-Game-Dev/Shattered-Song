using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour {

	public BattleGrid battleGrid;
	public JSONReader jsonReader;
	public GameObject characterParent;

	public TurnManager turnManager;

	public GameObject playerCharacterPrefab;
	public GameObject enemyCharacterPrefab;

	private List<Character> characters;



	JSONObject battleData;
	//Going to need a bunch of variables that will manage win/loss conditions
	//Maybe a new class that listens to the BattleManager for changes, and updates the UI accordingly
	
	void Start(){
		beginBattle("Battle1");
	}

	void Update(){
	}

	public void beginBattle(string battleName){
		battleData = new JSONObject(jsonReader.readInJSON(battleName, JSONReader.FileType.Battle));

		JSONObject gridSize = battleData.GetField("grid_size");
		generateGrid(gridSize);
			
		JSONObject playerCharacters = battleData.GetField("player_characters");
		loadPlayerCharacters(playerCharacters);
		
		JSONObject genericEnemyCharacters = battleData.GetField("generic_enemy_characters");
		loadGenericEnemyCharacters(genericEnemyCharacters);
	}

	private void generateGrid(JSONObject gridSize){
		int sizeX = int.Parse(gridSize.list[0].str);
		int sizeY = int.Parse(gridSize.list[1].str);

		battleGrid.generateMap(sizeX, sizeY);

		
	}

	private void loadPlayerCharacters(JSONObject playerCharacters){
		for(int i = 0; i < playerCharacters.list.Count; i++){
			JSONObject tempCharacter = playerCharacters.list[i];
			JSONObject location = tempCharacter.GetField("position");
			int posX = int.Parse(location.list[0].str);
			int posY = int.Parse(location.list[1].str);

			GameObject characterObject;

			if(battleGrid.spaceExistsInGrid(posX, posY)){
				//Vector3 pos = battleGrid.grid[posX, posY].transform.position + new Vector3(0f, 0f, -0.5f);

				characterObject = battleGrid.addCharacter(playerCharacterPrefab, posX, posY);

				//characterObject.transform.position = pos;


				PlayerCharacter tempPC = characterObject.GetComponent<PlayerCharacter>();

				//battleGrid.grid[posX, posY].addCharacter(tempPC);

				//characterObject.transform.position = battleGrid.grid[posX, posY].transform.position + characterParent.transform.position;

				string tempCharacterName = tempCharacter.GetField("character_name").str;

				JSONObject tempCharData = new JSONObject(jsonReader.readInJSON(tempCharacterName, JSONReader.FileType.Character)); 
				CharacterStats stats = new CharacterStats(tempCharData); 

				tempPC.characterAesthetics.loadPlaceholderSprite(tempCharacterName);

				tempPC.characterStats = stats;

				Debug.Log(tempCharacterName + " Created at " + posX + ", " + posY);
			}

			//GameObject tempObject = Instantiate(playerCharacterPrefab, )

			
		}
	}

	private void loadGenericEnemyCharacters(JSONObject genericCharacters){

	}

	private void turnTaken(){
		
	}

	private void checkIfBattleEnded(){

	}

	private void battleEnd(){

	}
}
