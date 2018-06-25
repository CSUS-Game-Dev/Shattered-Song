using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public abstract class Character : MonoBehaviour{

	public CharacterAesthetics characterAesthetics;
	public CharacterTempoManager tempoManager;
	public CharacterStats characterStats;
	public SkillList skills;

	private JSONObject characterJSON;
	private SpriteRenderer battleSprite;

	private TurnManager turnManager;

	public string characterName;
	public string screenName;

	//private bool isSetup = false;

	public virtual void setup(JSONObject characterInfo, TurnManager turnManager){
		battleSprite = GetComponent<SpriteRenderer>();
		characterJSON = characterInfo;
		this.turnManager = turnManager;

		characterStats = new CharacterStats(characterInfo);
		characterName = characterStats.characterName;
		screenName = characterStats.screenName;

		characterAesthetics = gameObject.AddComponent<CharacterAesthetics>();
		characterAesthetics.setup(this, battleSprite);

		tempoManager = new CharacterTempoManager(this);

		characterAesthetics.loadForBattle(aestheticsFinishedLoading);
		//TODO create skill lists
		//skills = new SkillList(characterInfo);
	}

	public void createGenericCharacter(CharacterTempoManager ctm, CharacterStats cs, SkillList s, CharacterAesthetics ca){

	}

	private void aestheticsFinishedLoading(){
		battleSprite.sprite = characterAesthetics.mapSprite;
		turnManager.addCharacter(this);
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

	public void moveByPath(List<GridSpace> path, Action callback = null){

	}

	public abstract void takeTurn();

}