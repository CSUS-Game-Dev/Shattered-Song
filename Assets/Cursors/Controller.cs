using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public static Controller instance = null;
    private Cursor activeCursor;


    // Use this for initialization
    void Start()
    {
        //Singleton
        if (instance == null)
        {
            instance = this;
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

    }
}
