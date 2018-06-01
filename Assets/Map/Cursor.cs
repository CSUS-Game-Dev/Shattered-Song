using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour {

	//We might want to change this so that this is the manager for ALL cursors throughout the game.
	//Keep multiple saved and use them as they are made the active cursor

	private Camera mainCam;

	public float camLerpSpeed;

	public BattleGrid battleGrid;
	
	private GridTargeting gridTargeting;

	private int gridMaxX, gridMaxY;

	private int posX = 0;
	private int posY = 0;

	private float timeToHold = 0.4f;
	private float timeBetweenSteps = 0.2f;
	private float holdingTime = 0f;
	private bool buttonHeld = false;

	private bool hasCharacterSelected;
	private Character characterSelected;
	private GridSpace originalPosition;
	
	// Update is called once per frame
	void Update () {
		cursorInput();

		if(mainCam != null){
			mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, new Vector3(transform.position.x, transform.position.y, -10), camLerpSpeed);
		}
	}

	public void setup(int gridPosX, int gridPosY, BattleGrid battleGrid){
		this.battleGrid = battleGrid;
		gridTargeting = battleGrid.gridTargeting;

		gridMaxX = battleGrid.grid.GetLength(0);
		gridMaxY = battleGrid.grid.GetLength(1);

		posX = gridPosX;
		posY = gridPosY;

		transform.position = battleGrid.grid[posX, posY].transform.position + new Vector3(0f, 0f, -.5f);

		mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
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

		//Handles when the buttons are held down - the cursor should move faster automatically
		if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W)){
			holdingTime += Time.deltaTime;
			if(holdingTime > timeToHold){
				buttonHeld = true;
				if(Input.GetKey(KeyCode.W)){
					move(GridDirection.UP);
				}
				if(Input.GetKey(KeyCode.S)){
					move(GridDirection.DOWN);
				}
				if(Input.GetKey(KeyCode.A)){
					move(GridDirection.LEFT);
				}
				if(Input.GetKey(KeyCode.D)){
					move(GridDirection.RIGHT);
				}

				holdingTime = 0f;
			}

			if(buttonHeld){
				holdingTime += Time.deltaTime;
				if(holdingTime >= timeBetweenSteps){
					if(Input.GetKey(KeyCode.W)){
						move(GridDirection.UP);
					}
					if(Input.GetKey(KeyCode.S)){
						move(GridDirection.DOWN);
					}
					if(Input.GetKey(KeyCode.A)){
						move(GridDirection.LEFT);
					}
					if(Input.GetKey(KeyCode.D)){
						move(GridDirection.RIGHT);
					}
					holdingTime = 0f;
				}
			}
		}
		else{
			holdingTime = 0f;
			buttonHeld = false;
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
				//battleGrid.targetPoint(posX, posY);
				battleGrid.targetSpaces(gridTargeting.getSpace(posX, posY));
			}
		}

		if(Input.GetKeyDown(KeyCode.Z)){
			battleGrid.targetSpaces(gridTargeting.getSpacesWithinRange(posX, posY, 2));
		}

		if(Input.GetKeyDown(KeyCode.X)){
			battleGrid.targetSpaces(gridTargeting.getSpacesInSquare(posX, posY, 2));
		}

		if(Input.GetKeyDown(KeyCode.C)){
			battleGrid.targetSpaces(gridTargeting.getSpacesInPlusExclusive(posX, posY, 3));
		}

		if(Input.GetKeyDown(KeyCode.V)){
			battleGrid.targetSpaces(gridTargeting.getSpacesInCross(posX, posY, 4));
		}

		if(Input.GetKeyDown(KeyCode.B)){
			battleGrid.targetSpaces(gridTargeting.getSpacesWithinRange(posX, posY, 3));
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
