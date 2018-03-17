using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* initialization of map script
 * currently spawns 1 "hero" skeleton and some "enemy" orcs and links them together with thier tiles*/

public class BattleMap : MonoBehaviour {
    private int height = 16;    //height and width of gamemap
    private int width = 35;
<<<<<<< HEAD
    public GameObject[,] Map;   //array to store tile object
    public GameObject basicblock1;  //prefab for grass
    public GameObject basicblock2;  //prefab for water
    public GameObject testHero;     //prefab for hero
    public GameObject testEnemy;    //prefab for enemy
    private bool spawned = false;   //bug fix variable to link map together
    void Start () {
        GameObject newtemp;         //new tile spawned
        GameObject oldtemp = null; //old tile for reference and linking
        Map = new GameObject[width, height];    //initializes array
=======
    public GameObject[,] Map;
    public GameObject basicblock1;
    public GameObject basicblock2;
    public GameObject testHero;
    public GameObject testEnemy;
    // Use this for initialization
    void Start () {
        buildMap();
    }
	
	// Update is called once per frame
	void Update () {
	}

    private void buildMap(){
        GameObject newtemp;
        GameObject oldtemp = null;
        Map = new GameObject[width, height];
>>>>>>> 5107cb68d66cb6b71c9b61881e1b644f0bc1290a

        for (int i = 0; i < height; i++)        //start on bottom left and going right then up
        {
            for(int j = 0; j < width; j++)
            {
<<<<<<< HEAD

                Vector3 tempLocation = new Vector3((-10.6f + (.63f * (float)j)), (-4.75f + (.63f * (float)i)), 0f);     //base poisiotn based on tile size configure for otehr tiles sizes if needed
                int number = Random.Range(0,4); //25% chance tile will be water tile
=======
// TODO place magic numbers in variables 
                //idk what these numbers are. Need to have these quantified
                Vector3 tempLocation = new Vector3((-10.6f + (.63f * (float)j)), (-4.75f + (.63f * (float)i)), 0f);
                int number = Random.Range(0,4);
>>>>>>> 5107cb68d66cb6b71c9b61881e1b644f0bc1290a
                if (number >= 1)
                {
                    newtemp = Instantiate(basicblock1, tempLocation, Quaternion.identity);
                    BasicBlock1 tempinfo = newtemp.GetComponent<BasicBlock1>();
                    tempinfo.xLoc = j;
                    tempinfo.yLoc = i;
                    newtemp.name = i + "   " + j;
                }
                else
                {
                    newtemp = Instantiate(basicblock2, tempLocation, Quaternion.identity);
                    BasicBlock1 tempinfo = newtemp.GetComponent<BasicBlock1>();
                    tempinfo.xLoc = j;
                    tempinfo.yLoc = i;
                    newtemp.name = i + "   " + j;
                }
                if (j == 0)     //if on left column
                {
                    BasicBlock1 newtempscr = newtemp.GetComponent<BasicBlock1>();   //set up references
                    newtempscr.left = null;
                    if(i == 0)  //bottom row
                    {
                        newtempscr.down = null;
                    }
                    else if(i == height - 1)    //top row
                    {
                        newtempscr.down = Map[j, i - 1];
                        newtempscr.up = null;
                    }
                    else        //anywhere else
                    {
                        newtempscr.down = Map[j, i - 1];
                        BasicBlock1 oldtempscr = Map[j, i-1].GetComponent<BasicBlock1>();
                        oldtempscr.up = newtemp;
                    }
                    oldtemp = newtemp;

                }
                else if(j == width-1)   //right column
                {
                    BasicBlock1 oldtempscr;
                    BasicBlock1 newtempscr = newtemp.GetComponent<BasicBlock1>();
                    newtempscr.left = oldtemp;
                    newtempscr.right = null;
                    if (i == 0)         //bottom row
                    {
                        newtempscr.down = null;
                    }
                    else if (i == height - 1)   //top row
                    {
                        newtempscr.down = Map[j, i - 1];
                        newtempscr.up = null;
                    }
                    else           //anywhere else
                    {
                        newtempscr.down = Map[j, i - 1];
                        oldtempscr = Map[j, i - 1].GetComponent<BasicBlock1>();
                        oldtempscr.up = newtemp;
                    }
                    oldtempscr = oldtemp.GetComponent<BasicBlock1>();
                    oldtempscr.right = newtemp;
                    Map[j - 1, i] = oldtemp;
                    Map[j, i] = newtemp;
                    oldtemp = newtemp;
                }
                else          //middle positions
                {
                    BasicBlock1 oldtempscr;
                    BasicBlock1 newtempscr = newtemp.GetComponent<BasicBlock1>();
                    newtempscr.left = oldtemp;
                    newtempscr.right = null;
                    if (i == 0)    //bottom row
                    {
                        newtempscr.down = null;
                    }
                    else if (i == height - 1)    //top row
                    {
                        newtempscr.down = Map[j, i - 1];
                        newtempscr.up = null;
                    }
                    else      //middle of eveything
                    {
                        newtempscr.down = Map[j, i - 1];
                        oldtempscr = Map[j, i - 1].GetComponent<BasicBlock1>();
                        oldtempscr.up = newtemp;
                    }
                    oldtempscr = oldtemp.GetComponent<BasicBlock1>();
                    oldtempscr.right = newtemp;
                    Map[j - 1, i] = oldtemp;
                    oldtemp = newtemp;
                }
                
            }
        }

        addCharacters();
    }
<<<<<<< HEAD
	
	// Update is called once per frame
	void Update () {     //update only used to fix reference bug found in previous version
		if(spawned == false)
        {
=======

    private void addCharacters(){
>>>>>>> 5107cb68d66cb6b71c9b61881e1b644f0bc1290a
            SpawnHeros();
            SpawnEnemys();      
    }

    private void Spawn(GameObject unit, GameObject block)     //move unit to block and update references in both
    {
        TestUnit unitInfo = unit.GetComponent<TestUnit>();
        BasicBlock1 tempinfo = block.GetComponent<BasicBlock1>();
        unit.transform.localPosition = block.transform.localPosition;
        unitInfo.currentLocation = block;
        tempinfo.occupied = true;
        tempinfo.occupee = unit;
    }

    private void SpawnHeros()      //spawn one skeleton hero
    {
        GameObject templocation;
        BasicBlock1 tempinfo;
        do //spawn in valid square in a random location
        {
            templocation = Map[Random.Range(0, width), Random.Range(0, height)];
            tempinfo = templocation.GetComponent<BasicBlock1>();
        } while (tempinfo.walkable != true || tempinfo.occupied == true);
        Vector3 location = templocation.transform.localPosition;
        GameObject critter = Instantiate(testHero, location, Quaternion.identity);
        Spawn(critter, templocation);
    }

    private void SpawnEnemys() //spawn ten enemy orcs
    {
        for(int i = 0; i < 10; i++)
        {
            GameObject templocation;
            BasicBlock1 tempinfo;
            do //chcek for location validity
            {
                templocation = Map[Random.Range(0, width), Random.Range(0, height)];
                tempinfo = templocation.GetComponent<BasicBlock1>();
            } while (tempinfo.walkable != true || tempinfo.occupied == true);
            Vector3 location = templocation.transform.localPosition;
            GameObject critter = Instantiate(testEnemy, location, Quaternion.identity);

            Spawn(critter, templocation);
        }
        
    }


}
