using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnUI : MonoBehaviour {

	private Image characterImage;
	private int orderNumber;
	private Text orderInList;
	private Text characterName;

	public PlayerTurn heldTurn;


	// Use this for initialization
	void Start () {
		
	}

	public void setup(){
		characterImage = transform.Find("SampleMask").Find("CharacterImage").GetComponent<Image>();
		//orderInList = transform.Find("OrderInList").GetComponent<Text>();
		//characterName = transform.Find("CharacterName").GetComponent<Text>();
	}
	
	public void setOrderNumber(int i){
		orderNumber = i;
		//orderInList.text = i.ToString();
	}

	public void updateUI(PlayerTurn p){
		heldTurn = p;

		Debug.Log(orderNumber);

		PlayerTurn tempPT = p;
		CharacterTempoManager tempCTM = p.character;
		Character tempC = p.character.character;
		CharacterAesthetics tempCA = p.character.character.characterAesthetics;

		if(p.character.character.characterAesthetics.uiPortrait != null){
			characterImage.sprite = p.character.character.characterAesthetics.uiPortrait;
		}
		//characterImage.color = p.character.playerColor;
		//characterName.text = p.character.charName;
	}
}
