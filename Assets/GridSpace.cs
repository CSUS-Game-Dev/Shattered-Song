using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpace : MonoBehaviour {

	public SpriteRenderer image;
	public Animator animator;

	public Vector2 positionInGrid;

	public bool isOccupied;
	public Character occupant;

	// Use this for initialization
	void Start () {
		
	}
	
	//public void setup(Sprite tileImage, )

	// Update is called once per frame
	void Update () {
		
	}

	public void target(){
		animator.SetTrigger("Target");
	}

	void select(){

	}

	void deselect(){
		animator.SetTrigger("Untarget");
	}
}
