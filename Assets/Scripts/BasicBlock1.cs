using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBlock1 : MonoBehaviour {
    public bool walkable;
    public bool occupied;
    public GameObject occupee;
    public GameObject up;
    public GameObject down;
    public GameObject left;
    public GameObject right;
    public int xLoc, yLoc;
    // Use this for initialization
    void Start () {
        occupied = false;
        occupee = null;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
