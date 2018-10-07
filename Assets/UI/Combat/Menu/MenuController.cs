using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
{

    public Button[] actionButtons;
    public Button[] skillButtons;

	void Start ()
    {
	    for (int i = 0; i < actionButtons.Length; i++)
        {
            actionButtons[i].onClick.AddListener(TaskOnClick);
        }
        for (int i = 0; i < skillButtons.Length; i++)
        {
            skillButtons[i].onClick.AddListener(TaskOnClick);
        }
	}

    //For testing purposes of making sure we are able to get proper info of button's pressed.
    public void TaskOnClick()
    {
        //Debug.Log("You pressed " + EventSystem.current.currentSelectedGameObject.name + " button!");

        switch(EventSystem.current.currentSelectedGameObject.name)
        {
            case "Move" :
                Debug.Log("You pressed " + EventSystem.current.currentSelectedGameObject.name + " button!");
                break;
            case "Attack" :
                Debug.Log("You pressed " + EventSystem.current.currentSelectedGameObject.name + " button!");
                break;
            case "Items" :
                Debug.Log("You pressed " + EventSystem.current.currentSelectedGameObject.name + " button!");
                break;
            case "Skills" :
                Debug.Log("You pressed " + EventSystem.current.currentSelectedGameObject.name + " button!");
                break;
            case "Item/Skill 1" :
                Debug.Log("You pressed " + EventSystem.current.currentSelectedGameObject.name + " button!");
                break;
            case "Item/Skill 2" :
                Debug.Log("You pressed " + EventSystem.current.currentSelectedGameObject.name + " button!");
                break;
            case "Item/Skill 3" :
                Debug.Log("You pressed " + EventSystem.current.currentSelectedGameObject.name + " button!");
                break;
            default :
                Debug.Log("The fuck is this?");
                break;
        }
    }
}
