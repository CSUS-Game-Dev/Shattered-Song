using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAesthetics : MonoBehaviour {

	public SpriteRenderer renderer;
	
	public void loadPlaceholderSprite(string characterName){

		Texture2D sprite = Resources.Load("Characters/" + characterName + "/placeholder") as Texture2D;

		renderer.sprite = Sprite.Create(sprite, new Rect(0f, 0f, sprite.width, sprite.height), new Vector2(.5f, .5f));

	}
}
