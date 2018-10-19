using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCursor : MonoBehaviour, ICursor
{
    private IControllable subject;
    private bool active;
    private List<GameObject> menuObjects;
    private int currentPos = 0;
    private Vector3 offset = new Vector3(-90f, -30f, 0f);
    // Use this for initialization

    public void setup(List<GameObject> items)
    {
        menuObjects = items;
        if (menuObjects.Count > 0)
        {
            moveToItem(0);
        }
    }

    public void processInput(InputType input)
    {
        if (input == InputType.Up || input == InputType.Right)
        {
            moveToItem(--currentPos);
        }

        if (input == InputType.Down || input == InputType.Left)
        {
            moveToItem(++currentPos);
        }
    }

    private void moveToItem(int pos)
    {
        if (pos == -1)
        {
            int newPos = menuObjects.Count - 1;
            currentPos = newPos;
            Transform targetPos = menuObjects[newPos].transform;
            transform.position = targetPos.position + offset;
        }
        else
        {
            int newPos = pos % menuObjects.Count;
            currentPos = newPos;
            Transform targetPos = menuObjects[newPos].transform;
            transform.position = targetPos.position + offset;
        }

        Debug.Log("Moving to position " + pos);
    }

    public bool getActive()
    {
        return false;
    }

    public void setActive(bool active)
    {
        this.active = active;
    }

    public IControllable getSubject()
    {
        return subject;
    }
}
