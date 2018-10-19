using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public static Controller instance = null;
    private IControllable activeListener;
    private Stack<ICursor> listeners;


    void Awake()
    {
        //Singleton
        if (instance == null)
        {
            instance = this;
            listeners = new Stack<ICursor>();
        }
        else { Destroy(gameObject, 0f); }
    }

    // Update is called once per frame
    void Update()
    {
        gatherInput();
    }

    void gatherInput()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) passInput(InputType.Down);
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) passInput(InputType.Up);
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) passInput(InputType.Left);
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) passInput(InputType.Right);
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) passInput(InputType.Confirm);
        if (Input.GetKeyDown(KeyCode.LeftCommand) || Input.GetKeyDown(KeyCode.LeftControl)
                                                  || Input.GetKeyDown(KeyCode.Z)) passInput(InputType.Up);

        if (Input.GetKeyDown(KeyCode.B)) { removeListener(); }
    }

    public void addListener(ICursor listener)
    {
        if (listeners.Count != 0)
        {
            listeners.Peek().setActive(false);
        }

        if (listener != null)
        {
            listeners.Push(listener);
        }

        Debug.Log("There are " + listeners.Count + " items in the stack");
    }

    public void removeListener(ICursor listener)
    {
        if (listeners.Peek().Equals(listener)) { listeners.Pop(); }
    }

    public void removeListener()
    {
        if (listeners.Count != 0)
        {
            listeners.Peek().setActive(false);
            listeners.Pop();
            if (listeners.Count > 0)
            {
                listeners.Peek().setActive(true);
            }
        }
    }

    void passInput(InputType input)
    {
        //Deals with null members on the top of the stack
        while (listeners.Peek() == null || listeners.Count == 0) { listeners.Pop(); }
        //Passes Input to the top of the stack
        if (listeners.Count > 0) { listeners.Peek().processInput(input); }
    }
}

public enum InputType { Left, Right, Up, Down, Confirm, Back }
