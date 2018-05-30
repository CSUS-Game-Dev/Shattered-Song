using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleGrid : MonoBehaviour {

	//There are 100 pixels to 1 Unity "unit"
	//Therefore a 64x64 pixel sprite will measure at 0.64 Unity "units"
	public static float TILE_SIZE = 0.64f;

/* 
	public Vector2 target;

	public GridDirection direction;

	public int distance;
*/
	

	public GridSpace[,] grid;

	public int gridSizeX, gridSizeY;

	public GameObject gridSpacePrefab;

	public GameObject cursorPrefab;

	public GameObject testCharacterPrefab;

	public Sprite img1, img2;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void generateMap(int sizeX, int sizeY){
		gridSizeX = sizeX;
		gridSizeY = sizeY;

		grid = new GridSpace[gridSizeX, gridSizeY];

		createTestGrid(sizeX, sizeY);
		createTestCharacter();
		
		GameObject cursorTemp = Instantiate(cursorPrefab, transform.position, Quaternion.identity, transform);
		cursorTemp.GetComponent<Cursor>().setup(sizeX / 2, sizeY / 2, this);

	}

	public void createTestGrid(int sizeX, int sizeY){

		for(int posX = 0; posX < sizeX; posX++){
			for(int posY = 0; posY < sizeY; posY++){

				Vector3 offset = new Vector3(posX * TILE_SIZE, posY * TILE_SIZE, 0f);

				GameObject temp = Instantiate(gridSpacePrefab, transform.position + offset, Quaternion.identity, transform);
				grid[posX, posY] = temp.GetComponent<GridSpace>();

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

	public void targetPoint(int targetX, int targetY){
		grid[targetX, targetY].animator.SetTrigger("Target");
	}

	public void targetRangeLine(int targetX, int targetY, int distance, GridDirection d){
		List<GridSpace> spaces = new List<GridSpace>();
		/* 
		Vector2 dir = new Vector2(0f, 0f);
		int currentX = targetX;
		int currentY = targetY;

		switch(d){
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
			default:
				break;
		}

		for(int i = 0; i < distance; i++){
			if(!spaceExistsInGrid(currentX, currentY)){
				break;
			}

			spaces.Add(grid[currentX, currentY]);

			currentX += (int)dir.x;
			currentY += (int)dir.y;
		}

		*/

		spaces = getSpacesInLine(targetX, targetY, distance, d);

		targetSpaces(spaces);
	}

	public void targetRangeCircle(int targetX, int targetY, int range){
		if(targetX >= 0 && targetX < gridSizeX && targetY >= 0 && targetY < gridSizeY){
			List<GridSpace> spaces = getSpacesAroundPoint(targetX, targetY, range);
			targetSpaces(spaces);
		}
	}

	public List<GridSpace> getSpacesInLine(int targetX, int targetY, int range, GridDirection d){
		List<GridSpace> spaces = new List<GridSpace>();

		if(range <= 0){
			return spaces;
		}

		int currentX = targetX;
		int currentY = targetY;

		Vector2 dir = new Vector2(0f, 0f);

		switch(d){
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
			default:
				break;
		}

		for(int i = 0; i < range; i++){
			if(!spaceExistsInGrid(currentX, currentY)){
				break;
			}

			spaces.Add(grid[currentX, currentY]);

			currentX += (int)dir.x;
			currentY += (int)dir.y;
		}


		return spaces;
	}


	public List<GridSpace> getSpacesAroundPoint(int targetX, int targetY, int range){
		List<GridSpace> spaces = new List<GridSpace>();

		//Gets Horizontal line through center
		spaces.AddRange(getSpacesInLine(targetX, targetY, range, GridDirection.LEFT));
		spaces.AddRange(getSpacesInLine(targetX + 1, targetY, range - 1, GridDirection.RIGHT));

		int currentVal = 0;
 
		//Gets vertical lines 
		for(int i = -range + 1; i < range - 1; i++){
			int dist = range - Mathf.Abs(i) - 1;
			spaces.AddRange(getSpacesInLine(targetX + i, targetY + 1, dist, GridDirection.UP));
			spaces.AddRange(getSpacesInLine(targetX + i, targetY - 1, dist, GridDirection.DOWN));
		}

		return spaces;
	}

	public void targetSpaces(List<GridSpace> spaces){
		foreach( GridSpace space in spaces){
			space.animator.SetTrigger("Target");
		}
	}

	public bool spaceExistsInGrid(int targetX, int targetY){

		return (targetX >= 0 && targetX < gridSizeX && targetY >= 0 && targetY < gridSizeY);

	}

}

public enum GridDirection {UP, DOWN, LEFT, RIGHT}
