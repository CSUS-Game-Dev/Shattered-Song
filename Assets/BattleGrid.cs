using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleGrid : MonoBehaviour {

	//There are 100 pixels to 1 Unity "unit"
	//Therefore a 64x64 pixel sprite will measure at 0.64 Unity "units"
	public static float TILE_SIZE = 0.64f;

	public Vector2 target;

	public GridSpace[,] grid;

	public int gridSizeX, gridSizeY;

	public GameObject gridSpacePrefab;

	public Sprite img1, img2;

	// Use this for initialization
	void Start () {
		
		gridSizeX = gridSizeY = 5;
		grid = new GridSpace[gridSizeX, gridSizeY];

		createTestGrid();
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			grid[(int)target.x, (int)target.y].animator.SetTrigger("Target");
			//animator.SetTrigger("Target");
		}
		else if(Input.GetKeyDown(KeyCode.U)){
			//animator.SetTrigger("Untarget");
		}
	}

	public void createTestGrid(){

		for(int posX = 0; posX < 5; posX++){
			for(int posY = 0; posY < 5; posY++){

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

}
