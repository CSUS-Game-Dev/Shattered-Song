using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CharacterAesthetics : MonoBehaviour {

	public Character character;
	public string characterName;
	public string filePath;

	public SpriteRenderer renderer;

	public Sprite tempoPortrait;

	public Sprite uiPortrait;

	public Sprite mapSprite;

	public Dictionary<PortraitExpression, Sprite> expressions;
	
	public bool battlePortraitsLoaded = false;
	public bool dialoguePortraitsLoaded = false;

	public IEnumerator battleCoroutine;
	public IEnumerator dialogueCoroutine;

	public void setup(Character character, SpriteRenderer sr){
		this.character = character;
		characterName = character.characterName;
		Debug.Log(characterName);

		filePath = "Characters/" + characterName;

		renderer = sr;
	}

	public void loadForBattle(Action callback){
		battleCoroutine = loadBattleImages(callback);
		StartCoroutine(battleCoroutine);
	}

	IEnumerator loadBattleImages(Action callback){
		Texture2D tempTempoUI = Resources.Load(filePath + "/" + characterName + "-portrait") as Texture2D;
		tempoPortrait = createStandardSprite(tempTempoUI);

		Texture2D tempUIPortrait = Resources.Load(filePath + "/" + characterName + "-portrait") as Texture2D;
		uiPortrait = createStandardSprite(tempUIPortrait);
		
		Texture2D tempMapSprite = Resources.Load(filePath + "/" + characterName + "-sprite") as Texture2D;
		mapSprite = createStandardSprite(tempMapSprite);

		yield return null;

		battlePortraitsLoaded = true;

		if(callback != null){
			callback();
		}
	}

	public void loadForDialogue(Action callback){
		dialogueCoroutine = loadDialogueImages(callback);
		StartCoroutine(dialogueCoroutine);
	}

	IEnumerator loadDialogueImages(Action callback){
		int numberOfImages = Enum.GetNames(typeof(PortraitExpression)).Length;

		for(int i = 0; i < numberOfImages; i++){
			string portraitName = Enum.GetName(typeof(PortraitExpression), i);
			string tempPath = filePath + "/Dialogue/" + characterName + "-" + portraitName;
			if(File.Exists(tempPath)){
				Texture2D temp = Resources.Load(tempPath) as Texture2D;
				expressions.Add((PortraitExpression)i, createStandardSprite(temp));
			}
		}

		yield return null;
		
		dialoguePortraitsLoaded = true;

		if(callback != null){
			callback();
		}
	}

	public void loadForDialogueSpecific(Action callback, PortraitExpression[] portraits){
		dialogueCoroutine = loadDialogueImagesSpecific(callback, portraits);
		StartCoroutine(dialogueCoroutine);
	}

	IEnumerator loadDialogueImagesSpecific(Action callback, PortraitExpression[] portraits){
		int numberOfImages = Enum.GetNames(typeof(PortraitExpression)).Length;

		for(int i = 0; i < numberOfImages; i++){
			string portraitName = Enum.GetName(typeof(PortraitExpression), i);
			Texture2D temp = Resources.Load(filePath + "/Dialogue/" + characterName + "-" + portraitName) as Texture2D;
			expressions.Add((PortraitExpression)i, createStandardSprite(temp));
		}

		yield return null;
		
		dialoguePortraitsLoaded = true;

		if(callback != null){
			callback();
		}
	}

	//Temporary stuff for testing
	public void loadPlaceholderSprite(string characterName){

		Texture2D sprite = Resources.Load("Characters/" + characterName + "/placeholder") as Texture2D;

		uiPortrait = Sprite.Create(sprite, new Rect(0f, 0f, sprite.width, sprite.height), new Vector2(.5f, .5f));
	}

	public Sprite createStandardSprite(Texture2D tex){
		return Sprite.Create(tex, new Rect(0f, 0f, tex.width, tex.height), new Vector2(.5f, .5f));
	}
}

public enum PortraitExpression {Neutral, Angry, Happy, Suprised, Melancholy, Embarassed, Nervous, Laughing, Relieved,}
