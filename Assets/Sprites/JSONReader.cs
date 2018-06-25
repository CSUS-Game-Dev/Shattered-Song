using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class JSONReader : MonoBehaviour {

	public enum FileType {Character, Battle, Text, Map};

	void Start(){
		
	}

/* 
	public void test(){
		JSONObject obj = new JSONObject(readInJSON("testCharacter", FileType.Character));

		CharacterStats characterStats = new CharacterStats(obj);

		Debug.Log(characterStats.ToString());
	}
*/

	public string readInJSON(string fileName, FileType fileType ){
		StringBuilder builder = new StringBuilder();
		
		string intermediatePath = "";

		switch(fileType){
			case FileType.Character:
				intermediatePath = "/Characters/JSONFiles/";
				break;
			case FileType.Battle:
				intermediatePath = "/Battles/";
				break;
			case FileType.Map:
				intermediatePath = "";
				break;
		}


		string filePath = Application.dataPath;
        try
        {
            StreamReader streamReader = new StreamReader(filePath + intermediatePath + fileName + ".JSON");

			while(!streamReader.EndOfStream){
				builder.Append(streamReader.ReadLine());
			}

			streamReader.Close();
        }
        catch
        {
            Debug.Log(filePath + intermediatePath + fileName + ".JSON could not be opened");
        }

		return builder.ToString();
	}

	public JSONObject makeJSONObject(string data){
		JSONObject jsonObj = new JSONObject(data);

		return jsonObj;
	}


}
