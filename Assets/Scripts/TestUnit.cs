using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUnit : MonoBehaviour {
    public GameObject currentLocation;
    public GameObject textDis;
    private bool move = false;
    public string faction;
    public int hp;
    public int attack;
    private GameObject[] moveQueue = new GameObject[10];
    public int moveIndex = 0;



    public GameObject newLocation = null;
    private BasicBlock1 oldtempscr = null;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(faction == "Hero")
        {
            if (Input.GetKeyDown(KeyCode.W))
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
            if (Input.GetKeyDown(KeyCode.LeftArrow))
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
        if (move == true)
        {
            if (newLocation == null)
                newLocation = NextMove();
            if ((Mathf.Abs(transform.position.x - newLocation.transform.position.x) > 0.05f) || (Mathf.Abs(transform.position.y - newLocation.transform.position.y) > 0.05f))
            {
                transform.position = Vector2.MoveTowards(transform.position, newLocation.transform.position, .8f*Time.deltaTime);
            }
                
            else
            {
                transform.position = newLocation.transform.position;
                if (MoveEmpty())
                {
                    move = false;
                    oldtempscr = currentLocation.GetComponent<BasicBlock1>();
                    oldtempscr.occupied = false;
                    oldtempscr.occupee = null;
                    BasicBlock1 newtempscr = newLocation.GetComponent<BasicBlock1>();
                    newtempscr.occupied = true;
                    newtempscr.occupee = gameObject;
                    currentLocation = newLocation;
                    newLocation = null;
                }
                else
                {
                    oldtempscr = currentLocation.GetComponent<BasicBlock1>();
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

    void AttackAround(string Direction)
    {
        Debug.Log("I got called");
        BasicBlock1 tempArea = currentLocation.GetComponent<BasicBlock1>();
        GameObject tempLocation = null;
        switch (Direction)
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
        if(tempLocation != null)
        {
            BasicBlock1 tempLocationInfo = tempLocation.GetComponent<BasicBlock1>();
            GameObject target = tempLocationInfo.occupee;
            if (target != null)
                Damage(target, attack);
        }
    }

    void Damage(GameObject temp, int dmg)
    {
        DamageTextManager.Instance.CreateText(temp.transform.position , dmg.ToString());

        TestUnit tempInfo = temp.GetComponent<TestUnit>();
        tempInfo.hp -= dmg;
    }

    public void moveDir(string Direction)
    {
        if(newLocation == null)
            oldtempscr = currentLocation.GetComponent<BasicBlock1>();
        else
        {
            if(moveIndex > 0)
            {
                oldtempscr = moveQueue[moveIndex-1].GetComponent<BasicBlock1>();
            }
            else
                oldtempscr = newLocation.GetComponent<BasicBlock1>();
        }
            
        GameObject templocation = null;
        switch (Direction)
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
        if (templocation != null)
        {
            BasicBlock1 newtempscr = templocation.GetComponent<BasicBlock1>();
            if ((newtempscr.walkable == true) && (newtempscr.occupied == false))
            {
                if (MoveEmpty())
                {
                    AddMove(templocation);
                    move = true;
                }
                else
                {
                    AddMove(templocation);
                }
                    

            }
            else
            {
            }
        }
    }

    private void AddMove(GameObject newObj)
    {
        if (moveIndex < 9)
        {
            moveQueue[moveIndex] = newObj;
            moveIndex++;
        }
    }

    private GameObject NextMove()
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

    private bool MoveEmpty()
    {
        if (moveIndex == 0)
            return true;
        else
            return false;
    }

}
