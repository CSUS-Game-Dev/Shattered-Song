using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleGrid : MonoBehaviour {

	//There are 100 pixels to 1 Unity "unit"
	//Therefore a 64x64 pixel sprite will measure at 0.64 Unity "units"
	public static float TILE_SIZE = 0.64f;

	public GridSpace[,] grid;

	public List<GridSpace> targeted;

	public int gridSizeX, gridSizeY;

	public GameObject gridSpacePrefab;

	public GameObject cursorPrefab;

	public GameObject testCharacterPrefab;

	public Sprite img1, img2;

	public GridTargeting gridTargeting;
	public GridPathing gridPathing;

	// Use this for initialization
	void Start () {
		gridTargeting = new GridTargeting(this);
		gridPathing = new GridPathing(this);
	}

	public void generateMap(int sizeX, int sizeY){
		targeted = new List<GridSpace>();

		gridSizeX = sizeX;
		gridSizeY = sizeY;

		grid = new GridSpace[gridSizeX, gridSizeY];

		createTestGrid(sizeX, sizeY);
		//createTestCharacter();
		
		GameObject cursorTemp = Instantiate(cursorPrefab, transform.position, Quaternion.identity, transform);
		cursorTemp.GetComponent<Cursor>().setup(sizeX / 2, sizeY / 2, this);

	}

	public void createTestGrid(int sizeX, int sizeY){

		for(int posX = 0; posX < sizeX; posX++){
			for(int posY = 0; posY < sizeY; posY++){

				Vector3 offset = new Vector3(posX * TILE_SIZE, posY * TILE_SIZE, 0f);

				GameObject temp = Instantiate(gridSpacePrefab, transform.position + offset, Quaternion.identity, transform);
				grid[posX, posY] = temp.GetComponent<GridSpace>();
				grid[posX, posY].positionInGrid = new Vector2(posX, posY);

				SpriteRenderer renderer = temp.GetComponent<SpriteRenderer>();
				int img = Random.Range(0, 2);

				if(img == 0){
					renderer.sprite = img1;
				}
				else{
					renderer.sprite = img2;
				}
			}
		}
	}

	public void createTestCharacter(){
		int locX = Random.Range(0, gridSizeX);
		int locY = Random.Range(0, gridSizeY);

		GameObject tempCharacter = Instantiate(testCharacterPrefab);

		grid[locX, locY].addCharacter(tempCharacter.GetComponent<Character>());
		tempCharacter.transform.position = grid[locX, locY].transform.position + new Vector3(0f, 0f, -0.5f);
	}

	public GameObject addCharacter(GameObject characterPrefab, int posX, int posY){
		GameObject tempCharacter = Instantiate(characterPrefab);

		grid[posX, posY].addCharacter(tempCharacter.GetComponent<Character>());
		tempCharacter.transform.position = grid[posX, posY].transform.position + new Vector3(0f, 0f, -0.5f);

		return tempCharacter;
	}

	public void targetSpaces(List<GridSpace> spaces){
		foreach( GridSpace space in spaces){
			space.animator.SetTrigger("Target");
			//targeted.Add(space);
		}
	}

	public void targetSpaces(GridSpace space){
		space.animator.SetTrigger("Target");
		//targeted.Add(space);
	}

	public void displaySpaces(List<GridSpace> spaces){
		foreach( GridSpace space in spaces){
			space.animator.SetTrigger("Display");
			//targeted.Add(space);
		}
	}

	public void displaySpaces(GridSpace space){
		space.animator.SetTrigger("Display");
		//targeted.Add(space);
	}
	public void untargetAll(){

	}

	public bool spaceExistsInGrid(int targetX, int targetY){

		return (targetX >= 0 && targetX < gridSizeX && targetY >= 0 && targetY < gridSizeY);

	}

	public Vector2 gridDirToVec2(GridDirection gridDirection){
		Vector2 dir = new Vector2(0f, 0f);

		switch(gridDirection){
			case GridDirection.UP:
				dir.y = 1.0f;
				break;
			case GridDirection.DOWN:
				dir.y = -1.0f;
				break;
			case GridDirection.RIGHT:
				dir.x = 1.0f;
				break;
			case GridDirection.LEFT:
				dir.x = -1.0f;
				break;
			case GridDirection.UPLEFT:
				dir.x = -1.0f;
				dir.y = 1.0f;
				break;
			case GridDirection.UPRIGHT:
				dir.x = 1.0f;
				dir.y = 1.0f;
				break;
			case GridDirection.DOWNLEFT:
				dir.x = -1.0f;
				dir.y = -1.0f;
				break;
			case GridDirection.DOWNRIGHT:
				dir.x = 1.0f;
				dir.y = -1.0f;
				break;
			default:
				break;
		}

		return dir;
	}

}

public enum GridDirection {UP, DOWN, LEFT, RIGHT, UPLEFT, UPRIGHT, DOWNLEFT, DOWNRIGHT}
public enum TargetType {SPACE, LINE, SQUARE, CIRCLE, PLUS, CROSS}
