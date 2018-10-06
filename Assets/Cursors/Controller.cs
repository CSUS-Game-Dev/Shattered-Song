using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public enum InputType { Left, Right, Up, Down, Confirm, Back }
    public static Controller instance = null;
    private IControllable activeListener;
    private Stack<IControllable> listeners;


    // Use this for initialization
    void Start()
    {
        //Singleton
        if (instance == null)
        {
            instance = this;
            listeners = new Stack<IControllable>();
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
    }

    void addListener(IControllable listener)
    {
        if (listener != null)
        {
            listeners.Push(listener);
        }
    }

    void passInput(InputType input)
    {
        //Deals with null members on the top of the stack
        while (listeners.Peek() == null || listeners.Count == 0) { listeners.Pop(); }
        //Passes Input to the top of the stack
        if (listeners.Count > 0) { listeners.Peek().takeInput(input); }
    }
}
