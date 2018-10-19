using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCursor : MonoBehaviour, ICursor
{
    private Camera mainCam;

    public float camLerpSpeed;

    public BattleGrid battleGrid;
    private GridTargeting gridTargeting;

    private int posX = 0;
    private int posY = 0;

    private float timeToHold = 0.4f;
    private float timeBetweenSteps = 0.2f;
    private float holdingTime = 0f;
    private bool buttonHeld = false;

    private GridSpace originalPosition;
    private bool active;

    // Update is called once per frame

    public void setup(int gridPosX, int gridPosY, BattleGrid battleGrid)
    {
        this.battleGrid = battleGrid;
        gridTargeting = battleGrid.gridTargeting;

        posX = gridPosX;
        posY = gridPosY;

        transform.position = battleGrid.grid[posX, posY].transform.position + new Vector3(0f, 0f, -.5f);

        // mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();

        setActive(true);
    }

    protected virtual void move(InputType direction)
    {
        int moveX = 0;
        int moveY = 0;

        if (direction == InputType.Up)
        {
            moveY = 1;
        }
        if (direction == InputType.Down)
        {
            moveY = -1;
        }
        if (direction == InputType.Left)
        {
            moveX = -1;
        }
        if (direction == InputType.Right)
        {
            moveX = 1;
        }


        int newX = posX + moveX;
        int newY = posY + moveY;

        if (battleGrid.spaceExistsInGrid(newX, newY))
        {
            posX = newX;
            posY = newY;

            transform.position = battleGrid.grid[posX, posY].transform.position + new Vector3(0f, 0f, -.5f);
        }
    }

    public void processInput(InputType input)
    {
        switch (input)
        {
            case InputType.Up:
            case InputType.Down:
            case InputType.Left:
            case InputType.Right:
                move(input);
                break;
            default:
                break;
        }
    }
    public bool getActive()
    {
        return active;
    }

    public void setActive(bool active)
    {
        CameraManager camMgr = CameraManager.instance;
        this.active = active;
        if (active)
        {
            camMgr.follow(gameObject);
            GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            if (camMgr.isFollowing() && camMgr.getFollowing() == gameObject)
            {
                camMgr.release();
            }

            GetComponent<SpriteRenderer>().enabled = false;
        }

    }
    public IControllable getSubject() { return battleGrid; }
}
