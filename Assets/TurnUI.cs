using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnUI : MonoBehaviour {

	private Image characterImage;
	private int orderNumber;
	public Text orderInList;
	private Text characterName;

	public PlayerTurn heldTurn;


	// Use this for initialization
	void Start () {
		characterImage = transform.Find("CharacterImage").GetComponent<Image>();
		orderInList = transform.Find("OrderInList").GetComponent<Text>();
		characterName = transform.Find("CharacterName").GetComponent<Text>();
	}
	
	public void setOrderNumber(int i){
		orderNumber = i;
		orderInList.text = i.ToString();
	}

	public void updateUI(PlayerTurn p){
		heldTurn = p;
		characterImage.color = p.character.playerColor;
		characterName.text = p.character.charName;
	}
}
