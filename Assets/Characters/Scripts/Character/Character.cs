using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public abstract class Character : MonoBehaviour{

	private CharacterAesthetics characterAesthetics;
	private CharacterTempoManager tempoManager;
	private CharacterStats characterStats;
	private SkillList skills;

	private bool isSetup = false;

	void Start(){

	}

	public virtual void setup(int character_ID){

	}

	public virtual void setup(string characterName){
		
	}

	public void createGenericCharacter(CharacterTempoManager ctm, CharacterStats cs, SkillList s, CharacterAesthetics ca){

	}

	public void moveTo(Transform destination, float lerpSpeed, Action callback = null){
		IEnumerator coroutine = moving(destination, lerpSpeed);
		StartCoroutine(coroutine);

	}

	IEnumerator moving(Transform destination, float lerpSpeed, Action callback = null){
		while((transform.position - destination.position).magnitude > 0.1f){
			transform.position = Vector3.Lerp(transform.position, destination.position, lerpSpeed);
			yield return null;
		}

		transform.position = destination.position + new Vector3(0f, 0f, -.5f);
		yield return new WaitForSeconds(0.25f);
	}

}