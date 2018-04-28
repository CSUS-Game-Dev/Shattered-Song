using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour {

	//We might want to change this so that this is the manager for ALL cursors throughout the game.
	//Keep multiple saved and use them as they are made the active cursor

	public BattleGrid battleGrid;

	private int gridMaxX, gridMaxY;

	private int posX = 0;
	private int posY = 0;

	private bool hasCharacterSelected;
	private Character characterSelected;
	private GridSpace originalPosition;
	
	// Update is called once per frame
	void Update () {
		cursorInput();
	}

	public void setup(int gridPosX, int gridPosY, BattleGrid battleGrid){
		this.battleGrid = battleGrid;

		gridMaxX = battleGrid.grid.GetLength(0);
		gridMaxY = battleGrid.grid.GetLength(1);

		posX = gridPosX;
		posY = gridPosY;

		transform.position = battleGrid.grid[posX, posY].transform.position + new Vector3(0f, 0f, -.5f);
	}

	private void cursorInput(){
		if(Input.GetKeyDown(KeyCode.W)){
			move(GridDirection.UP);
		}
		if(Input.GetKeyDown(KeyCode.S)){
			move(GridDirection.DOWN);
		}
		if(Input.GetKeyDown(KeyCode.A)){
			move(GridDirection.LEFT);
		}
		if(Input.GetKeyDown(KeyCode.D)){
			move(GridDirection.RIGHT);
		}

		if(Input.GetKeyDown(KeyCode.Space)){
			if(battleGrid.grid[posX, posY].isOccupied && !hasCharacterSelected){
				battleGrid.grid[posX, posY].selectCharacter();
				originalPosition = battleGrid.grid[posX, posY];
				characterSelected = battleGrid.grid[posX, posY].occupant;
				hasCharacterSelected = true;
				
			}
			else if(hasCharacterSelected && !battleGrid.grid[posX, posY].isOccupied){
				hasCharacterSelected = false;
				characterSelected.moveTo(battleGrid.grid[posX, posY].transform, 0.7f);

				battleGrid.grid[posX, posY].isOccupied = true;
				battleGrid.grid[posX, posY].occupant = characterSelected;

				originalPosition.isOccupied = false;
				originalPosition = null;
				characterSelected = null;				
			}
			else{
				battleGrid.targetPoint(posX, posY);
			}
		}
	}

	private void move(GridDirection direction){
		int moveX = 0;
		int moveY = 0;

		if(direction == GridDirection.UP){
			moveY = 1;
		}
		if(direction == GridDirection.DOWN){
			moveY = -1;
		}
		if(direction == GridDirection.LEFT){
			moveX = -1;
		}
		if(direction == GridDirection.RIGHT){
			moveX= 1;
		}


		int newX = posX + moveX;
		int newY = posY + moveY;

		if(battleGrid.spaceExistsInGrid(newX, newY)){
			posX = newX;
			posY = newY;

			transform.position = battleGrid.grid[posX, posY].transform.position + new Vector3(0f, 0f, -.5f);
		}
	}

}
