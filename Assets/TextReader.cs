using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextReader : MonoBehaviour {

    [SerializeField] public string filePath;
    [SerializeField] public string fileName;

    //The parent object of all of the items in the dialogue window
    private Transform talkWindow;

    //The objects inside the window. A text box, an image for the text box background, and 7 positions for characters to occupy, and a picture background
    private Image background;
    private Image foreground;
    private Image textBox;
    private Image pos1;
    private Image pos2;
    private Image pos3;
    private Image pos4;
    private Image pos5;
    private Image pos6;
    private Image pos7;
    private Text text;

    //Class that handles reading the text file in
    private StreamReader streamReader;

    //This boolean keeps track of whether or not the next line in the text file is going to be commands for the window.
    //True means a command like "Add face to position 1", false means next line read is plaintext.
    //First line is always a command.
    private bool commandNext = true;

    //These keep track of the variables for the text, and tell us whether or not the scene is over
    private bool done = false;
    private List<string> commands;
    private int currentLine = 0;

	// Use this for initialization
	void Start () {

        talkWindow = GameObject.Find("TalkScreen").transform;

        background = talkWindow.Find("Background").GetComponent<Image>();
        foreground = talkWindow.Find("Foreground").GetComponent<Image>();
        textBox = talkWindow.Find("TextBox").GetComponent<Image>();
        pos1 = talkWindow.Find("Image1").GetComponent<Image>();
        pos2 = talkWindow.Find("Image2").GetComponent<Image>();
        pos3 = talkWindow.Find("Image3").GetComponent<Image>();
        pos4 = talkWindow.Find("Image4").GetComponent<Image>();
        pos5 = talkWindow.Find("Image5").GetComponent<Image>();
        pos6 = talkWindow.Find("Image6").GetComponent<Image>();
        pos7 = talkWindow.Find("Image7").GetComponent<Image>();
        text = talkWindow.Find("Text").GetComponent<Text>();

        commands = new List<string>();

        filePath = Application.dataPath;
        Debug.Log("The current filepath is: " + filePath);
        try
        {
            Debug.Log("Attempting to open the file at location : " + filePath + "/TextFiles/" + fileName);
            streamReader = new StreamReader(filePath + "/TextFiles/" + fileName);
            textBox.enabled = true;
            
            
        }
        catch
        {
            Debug.Log("The file in question could not be opened. Exiting Scene");
            SceneManager.UnloadSceneAsync("TextScene");
        }

        readCommands();


	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)){
            readNext();
        }
	}

    public void readNext(){
        if(!done){
            if(commandNext){
                readCommands();
            }
            else{
                displayText();
            }
        }
        else{
             SceneManager.UnloadSceneAsync("TextScene");
        }
    }

    //Reads the "Commands" in from the text file.
    private void readCommands(){
        if(streamReader.Peek() > -1){
            commands = getTokens(streamReader.ReadLine());
            switch(commands[0]){
                case("##Text"):
                        commandNext = false;
                        break;
                default:
                        break;
            }
        }
        else{
            SceneManager.UnloadSceneAsync("TextScene");
        }
        
    }

    //Handles displaying explicit text to the text box
    public void displayText(){
        try{
            text.text = streamReader.ReadLine();   
        }
        catch{
            Debug.Log("There was a problem reading through the text file");
        }

        //This bit just handles whether or not the next line is read as plaintext or not
        int nextLine = currentLine + 1;
        if(nextLine >= int.Parse(commands[1])){
            currentLine = 0;
            commandNext = true;
        }
        else{
            currentLine++;
        }
    }

    //Reads through the comma separated commands in the current line and returns them in a list
    public List<string> getTokens(string input){
        List<string> results = new List<string>();
        StringBuilder stringBuilder = new StringBuilder();

        for(int i = 0; i < input.Length; i++){
            if(!(input[i] == ',')){
                stringBuilder.Append(input[i]);
            }
            else{
                results.Add(stringBuilder.ToString());
                stringBuilder = new StringBuilder();
            }
        }
        results.Add(stringBuilder.ToString());

        return results;
    }
}
