using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*basic Unit with hp and health
 * other stats will be added in the future in order to add other game mechanics
 */


public class TestUnit : MonoBehaviour {
    public GameObject currentLocation;//saved current position of the character allows the unit to access map information
    public GameObject textDis;  //link to pop up text manager to allow createion of temporary text on ui
    private bool move = false;  //used to set up move queue so that moves are more refined
    public string faction;      //currently used to disallow non player chartacter from taking inputs can be used later to for other purposes
    public int hp;              //stock value for hp
    public int attack;          //stock value for attack
    private GameObject[] moveQueue = new GameObject[10];    //move queue holder, allows ten move to be queued and executed sequentially
    public int moveIndex = 0;   //index to track what is the current empty place in the moce queue



    public GameObject newLocation = null;   //used for movement in the four cardinal directions
    private BasicBlock1 oldtempscr = null;  //used to acces currentLocations information such as who is near it.
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(faction == "Hero")       //current allows player to only control skeleton.
        {
            if (Input.GetKeyDown(KeyCode.W))    //movement. ToDo replace with clicking and pathfinding
            {
                moveDir("Up");
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                moveDir("Left");
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                moveDir("Down");
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                moveDir("Right");
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))    //attack places right next to eachother.
            {
                AttackAround("Left");
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                AttackAround("Right");
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                AttackAround("Up");
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                AttackAround("Down");
            }
        }
        if (move == true)       //move queue handler
        {
            if (newLocation == null)    //if no current movement grab next movement
                newLocation = NextMove();
            if ((Mathf.Abs(transform.position.x - newLocation.transform.position.x) > 0.05f) || (Mathf.Abs(transform.position.y - newLocation.transform.position.y) > 0.05f))   //enables sliding effect to next square
            {
                transform.position = Vector2.MoveTowards(transform.position, newLocation.transform.position, .8f*Time.deltaTime);   //to make sprite move faster slower modify value*deltaTime
            }
                
            else    //if sprite close enough and movement done
            {
                transform.position = newLocation.transform.position; //teleport a unoticable amount to complete movement
                if (MoveEmpty())    //check if move in queue
                {
                    move = false;   //if no change current location to new location and update blcok and unit references and variables
                    oldtempscr = currentLocation.GetComponent<BasicBlock1>();
                    oldtempscr.occupied = false;
                    oldtempscr.occupee = null;
                    BasicBlock1 newtempscr = newLocation.GetComponent<BasicBlock1>();
                    newtempscr.occupied = true;
                    newtempscr.occupee = gameObject;
                    currentLocation = newLocation;
                    newLocation = null;         //set new location to null for next inputed move
                }
                else       //if move in queue
                {
                    oldtempscr = currentLocation.GetComponent<BasicBlock1>();  //same as above but set new location to next move
                    oldtempscr.occupied = false;
                    oldtempscr.occupee = null;
                    BasicBlock1 newtempscr = newLocation.GetComponent<BasicBlock1>();
                    newtempscr.occupied = true;
                    newtempscr.occupee = gameObject;
                    currentLocation = newLocation;
                    newLocation = NextMove();
                }
            }
        } 
    }

    void AttackAround(string Direction)         //attack action for unit right next to unit 
    {
        BasicBlock1 tempArea = currentLocation.GetComponent<BasicBlock1>();     //get current location block information
        GameObject tempLocation = null;
        switch (Direction)          //choose direction baed off parameter
        {
            case "Up":
                tempLocation = tempArea.up;
                break;
            case "Down":
                tempLocation = tempArea.down;
                break;
            case "Left":
                tempLocation = tempArea.left;
                break;
            case "Right":
                tempLocation = tempArea.right;
                break;
        }
        if(tempLocation != null)    //if it is a real square
        {
            BasicBlock1 tempLocationInfo = tempLocation.GetComponent<BasicBlock1>();    //get unit occupying spot if there is one
            GameObject target = tempLocationInfo.occupee;
            if (target != null)     //if there is a target damage it
                Damage(target, attack);
        }
    }

    void Damage(GameObject temp, int dmg)
    {
        DamageTextManager.Instance.CreateText(temp.transform.position , dmg.ToString());    //call dmgTextManager to create dmg number

        TestUnit tempInfo = temp.GetComponent<TestUnit>();
        tempInfo.hp -= dmg;     //minus hp
    }

    public void moveDir(string Direction)       //movement methodf
    {
        if(newLocation == null)     //if no move in move queue and not moving
            oldtempscr = currentLocation.GetComponent<BasicBlock1>(); //movement off of currentl location
        else // if moving already
        {
            if(moveIndex > 0)   //if already moving then base move off of last move entered
            {
                oldtempscr = moveQueue[moveIndex-1].GetComponent<BasicBlock1>();
            }
            else
                oldtempscr = newLocation.GetComponent<BasicBlock1>();
        }
            
        GameObject templocation = null; 
        switch (Direction)  //determin direction based off of parameter
        {
            case "Up":
                templocation = oldtempscr.up;
                break;
            case "Down":
                templocation = oldtempscr.down;
                break;
            case "Left":
                templocation = oldtempscr.left;
                break;
            case "Right":
                templocation = oldtempscr.right;
                break;
        }
        if (templocation != null)   //if not out of map
        {
            BasicBlock1 newtempscr = templocation.GetComponent<BasicBlock1>();
            if ((newtempscr.walkable == true) && (newtempscr.occupied == false))    //check if valid movement square
            {
                if (MoveEmpty())    //if no movement already
                {
                    AddMove(templocation); //add to queue and star movent code in update
                    move = true;
                }
                else        //if already moving add movment to queue
                {
                    AddMove(templocation);
                }
                    

            }
            else   //if not valid movment do nothing
            {
            }
        }
    }

    private void AddMove(GameObject newObj)    //methid to add moves to queu based off target desination
    {
        if (moveIndex < 9)
        {
            moveQueue[moveIndex] = newObj;
            moveIndex++;
        }
    }

    private GameObject NextMove()       //method to get next move in queue and null the end of the array
    {
        GameObject temp = moveQueue[0];
        for (int i = 0; i < moveIndex; i++)
        {
            if (i != moveIndex)
            {
                moveQueue[i] = moveQueue[i + 1];
            }
            else
                moveQueue[i] = null;
        }
        moveIndex--;
        return temp;
    }

    private bool MoveEmpty()  //method to check if queu empty
    {
        if (moveIndex == 0)
            return true;
        else
            return false;
    }

}
