using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMap : MonoBehaviour {
    private int height = 16;
    private int width = 35;
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

        for (int i = 0; i < height; i++)
        {
            for(int j = 0; j < width; j++)
            {
// TODO place magic numbers in variables 
                //idk what these numbers are. Need to have these quantified
                Vector3 tempLocation = new Vector3((-10.6f + (.63f * (float)j)), (-4.75f + (.63f * (float)i)), 0f);
                int number = Random.Range(0,4);
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
                if (j == 0)
                {
                    BasicBlock1 newtempscr = newtemp.GetComponent<BasicBlock1>();
                    newtempscr.left = null;
                    if(i == 0)
                    {
                        newtempscr.down = null;
                    }
                    else if(i == height - 1)
                    {
                        newtempscr.down = Map[j, i - 1];
                        newtempscr.up = null;
                    }
                    else
                    {
                        newtempscr.down = Map[j, i - 1];
                        BasicBlock1 oldtempscr = Map[j, i-1].GetComponent<BasicBlock1>();
                        oldtempscr.up = newtemp;
                    }
                    oldtemp = newtemp;

                }
                else if(j == width-1)
                {
                    BasicBlock1 oldtempscr;
                    BasicBlock1 newtempscr = newtemp.GetComponent<BasicBlock1>();
                    newtempscr.left = oldtemp;
                    newtempscr.right = null;
                    if (i == 0)
                    {
                        newtempscr.down = null;
                    }
                    else if (i == height - 1)
                    {
                        newtempscr.down = Map[j, i - 1];
                        newtempscr.up = null;
                    }
                    else
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
                else
                {
                    BasicBlock1 oldtempscr;
                    BasicBlock1 newtempscr = newtemp.GetComponent<BasicBlock1>();
                    newtempscr.left = oldtemp;
                    newtempscr.right = null;
                    if (i == 0)
                    {
                        newtempscr.down = null;
                    }
                    else if (i == height - 1)
                    {
                        newtempscr.down = Map[j, i - 1];
                        newtempscr.up = null;
                    }
                    else
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

    private void addCharacters(){
            SpawnHeros();
            SpawnEnemys();      
    }

    private void Spawn(GameObject unit, GameObject block)
    {
        TestUnit unitInfo = unit.GetComponent<TestUnit>();
        BasicBlock1 tempinfo = block.GetComponent<BasicBlock1>();
        unit.transform.localPosition = block.transform.localPosition;
        unitInfo.currentLocation = block;
        tempinfo.occupied = true;
        tempinfo.occupee = unit;
    }

    private void SpawnHeros()
    {
        GameObject templocation;
        BasicBlock1 tempinfo;
        do
        {
            templocation = Map[Random.Range(0, width), Random.Range(0, height)];
            tempinfo = templocation.GetComponent<BasicBlock1>();
        } while (tempinfo.walkable != true || tempinfo.occupied == true);
        Vector3 location = templocation.transform.localPosition;
        GameObject critter = Instantiate(testHero, location, Quaternion.identity);
        Spawn(critter, templocation);
    }

    private void SpawnEnemys()
    {
        for(int i = 0; i < 10; i++)
        {
            GameObject templocation;
            BasicBlock1 tempinfo;
            do
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
