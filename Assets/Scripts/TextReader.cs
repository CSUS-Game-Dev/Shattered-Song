using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextReader : MonoBehaviour
{

    [SerializeField] public string fileName;
    [SerializeField] public float pixelsPerUnit = 100f;
    private string filePath;

    //The parent object of all of the items in the dialogue window
    private Transform talkWindow;

    //The objects inside the window. A text box, an image for the text box background, and 7 positions for characters to occupy, and a picture background
    private Image backgroundImage;
    private Image foregroundImage;
    private Image speechBox;
    private Image pos1;
    private Image pos2;
    private Image pos3;
    private Image pos4;
    private Image pos5;
    private Image pos6;
    private Image pos7;
    private Image pos8;
    private Text text;
    private AudioSource audioPlayer;

    //Class that handles reading the text file in
    private StreamReader streamReader;

    //This boolean keeps track of whether or not the next line in the text file is going to be commands for the window.
    //True means a command like "Add face to position 1", false means next line read is plaintext.
    //First line is always a command.
    private bool commandNext = true;

    //These keep track of the variables for the text, and tell us whether or not the scene is over
    private bool textComplete = true;
    private bool done = false;
    private List<string> commands;
    private int currentLine = 0;

	// Use this for initialization
	void Start ()
    {

        talkWindow = GameObject.Find("TalkScreen").transform;

        backgroundImage = talkWindow.Find("Background").GetComponent<Image>();
        foregroundImage = talkWindow.Find("Foreground").GetComponent<Image>();
        speechBox = talkWindow.Find("TextBox").GetComponent<Image>();
        pos1 = talkWindow.Find("Image1").GetComponent<Image>();
        pos2 = talkWindow.Find("Image2").GetComponent<Image>();
        pos3 = talkWindow.Find("Image3").GetComponent<Image>();
        pos4 = talkWindow.Find("Image4").GetComponent<Image>();
        pos5 = talkWindow.Find("Image5").GetComponent<Image>();
        pos6 = talkWindow.Find("Image6").GetComponent<Image>();
        pos7 = talkWindow.Find("Image7").GetComponent<Image>();
        pos8 = talkWindow.Find("Image8").GetComponent<Image>();
        text = talkWindow.Find("Text").GetComponent<Text>();
        audioPlayer = talkWindow.Find("Audio").GetComponent<AudioSource>();

        commands = new List<string>();

        filePath = Application.dataPath;
        Debug.Log("The current filepath is: " + filePath);
        try
        {
            Debug.Log("Attempting to open the file at location : " + filePath + "/TextFiles/" + fileName);
            streamReader = new StreamReader(filePath + "/TextFiles/" + fileName + ".txt");
            speechBox.enabled = true;
        }
        catch
        {
            Debug.Log("The file in question could not be opened. Exiting Scene");
            SceneManager.UnloadSceneAsync("TextScene");
        }

        readCommands();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || commandNext)
        {
                readNext();
        }
	}

    public void readNext()
    {
        if(!done)
        {
            if(commandNext)
            {
                readCommands();       
            }
            else{
                displayText();
            }
        }
        else
        {
            streamReader.Close();
            SceneManager.UnloadSceneAsync("TextScene");
        }
    }

    //Reads the "Commands" in from the text file.
    private void readCommands()
    {
        if(streamReader.Peek() > -1)
        {
            commands = getTokens(streamReader.ReadLine());
            switch(commands[0])
            {
                case("##Text"):
                        commandNext = false;
                        displayText();
                        break;
                case("##Portrait"):
                        portrait();
                        break;
                case("##Background"):
                        background();
                        break;
                case("##Foreground"):
                        foreground();
                        break;
                case("##Box"):
                        box();
                        break;
                case("##Audio"):
                        sound();
                        break;
                default:
                        break;
            }
        }
        else
        {
            SceneManager.UnloadSceneAsync("TextScene");
        }
    }

    //Handles displaying explicit text to the text box
    public void displayText()
    {
        int nextLine = currentLine + 1;
        if(!(nextLine > int.Parse(commands[1])))
        {
            try
            {
                text.text = streamReader.ReadLine();   
            }
            catch
            {
                Debug.Log("There was a problem reading through the text file");
            }
        }
        //This bit just handles whether or not the next line is read as plaintext or not
        
        if(nextLine > int.Parse(commands[1]))
        {
            currentLine = 0;
            commandNext = true;
        }
        else
        {
            currentLine++;
        }
    }

    //Controls which portrait function is called based on the second command in the line
    public void portrait()
    {
        switch(commands[1])
        {
            case("move"):
                movePortrait();
                break;
            case("remove"):
                removePortrait();
                break;
            case("add"):
                addPortrait();
                break;
            default:
                break;
        }
    }

    public void addPortrait()
    {
        string character = commands[2];
        string expression = commands[3];
        int position = int.Parse(commands[4]);

        Texture2D t = Resources.Load("Characters/" + character + "/" + expression) as Texture2D;
        Sprite s = Sprite.Create(t, new Rect(0.0f, 0.0f, t.width, t.height), new Vector2((float)t.width/2, (float)t.height/2), pixelsPerUnit);
 
        Image image = getImageAtPos(position);
        image.enabled = true;
        image.sprite = s;
    }

    public void removePortrait()
    {
        int position = int.Parse(commands[2]);

        Image image = getImageAtPos(position);
        image.enabled = false;
    }

    //Use this whenever you can. It's faster to move a portrait that's already in the scene than it is to load a new one in.
    public void movePortrait()
    {
        int from = int.Parse(commands[2]);
        int to = int.Parse(commands[3]);

        Image image1 = getImageAtPos(from);
        Sprite sprite = image1.sprite;
        Image image2 = getImageAtPos(to);
        image1.enabled = false;
        image2.enabled = true;
        image2.sprite = sprite;
    }

    public Image getImageAtPos(int num)
    {

        Image image;

        switch(num)
        {
            case 1:
                image = pos1;
                break;
            case 2:
                image = pos2;
                break;
            case 3:
                image = pos3;
                break;
            case 4:
                image = pos4;
                break;
            case 5:
                image = pos5;
                break;
            case 6:
                image = pos6;
                break;
            case 7:
                image = pos7;
                break;
            case 8:
                image = pos8;
                break;
            default:
                image = pos4;
                break;
        }

        return image;

    }

    //Handles adding and removing images in the background
    public void background()
    {
        switch(commands[1])
        {
            case("add"):
                backgroundImage.enabled = true;
                Texture2D t = Resources.Load("BackgroundImages/" + commands[2]) as Texture2D;
                Sprite s = Sprite.Create(t, new Rect(0.0f, 0.0f, t.width, t.height), new Vector2(Screen.width/2,Screen.height/2), pixelsPerUnit);
                backgroundImage.sprite = s;
                break;
            case("remove"):
                backgroundImage.enabled = false;
                break;
            default:
                break;
        }
    }

    //Handles adding and removing images in the foreground
    public void foreground()
    {
        switch(commands[1])
        {
            case("add"):
                foregroundImage.enabled = true;
                Texture2D t = Resources.Load("ForegroundImages/" + commands[2]) as Texture2D;
                Sprite s = Sprite.Create(t, new Rect(0.0f, 0.0f, t.width, t.height), new Vector2(Screen.width/2,Screen.height/2), pixelsPerUnit);
                foregroundImage.sprite = s;
                break;
            case("remove"):
                foregroundImage.enabled = false;
                break;
            default:
                break;
        }
    }

    //Handles changing the image used as the text box
    public void box()
    {
        Texture2D t = Resources.Load("SpeechBoxes/" + commands[1]) as Texture2D;
        Sprite s = Sprite.Create(t, new Rect(0.0f, 0.0f, t.width, t.height), new Vector2(t.width/2,t.height/2), pixelsPerUnit);
        speechBox.sprite = s;
    }

    public void sound()
    {
        string audioType = commands[1];

        switch(audioType)
        {
            case("Speech"):
                AudioClip a = Resources.Load("Audio/Speech/" + commands[2] + "/" + commands[3]) as AudioClip;
                audioPlayer.clip = a;
                audioPlayer.Play();
                break;
            case("Other"):
                AudioClip b = Resources.Load("Audio/"+ commands[1] + "/" + commands[2] + "/" + commands[3]) as AudioClip;
                audioPlayer.clip = b;
                audioPlayer.Play();
                break;
            default:
                break;
        }

    }


    //Reads through the comma separated commands in the current line and returns them in a list
    public List<string> getTokens(string input)
    {
        List<string> results = new List<string>();
        StringBuilder stringBuilder = new StringBuilder();

        for(int i = 0; i < input.Length; i++)
        {
            if(!(input[i] == ','))
            {
                stringBuilder.Append(input[i]);
            }
            else
            {
                results.Add(stringBuilder.ToString());
                stringBuilder = new StringBuilder();
            }
        }
        results.Add(stringBuilder.ToString());

        return results;
    }
}
