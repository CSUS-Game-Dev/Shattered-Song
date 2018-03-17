using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Script to handle damage number popups
 * singleston so only one can be created at time
 * method accessed through DamageTextManager.Instance.methodName);*/

public class DamageTextManager : MonoBehaviour {

    private static DamageTextManager instance;  //reference to itself

    public GameObject textPrefab;   //prefab for text
    public RectTransform canvasTransform;   //canvas reference
    public Camera cam;

    public static DamageTextManager Instance    //reurns reference to singleton so no multipl copies
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<DamageTextManager>();
            }
            return instance;
        }
    }

    public void CreateText(Vector3 pos, string text, float speed, Vector3 direction)    //create text with certain speed and direction
    {

		GameObject temp = Instantiate(textPrefab);		//instantiate prefab at location
		temp.transform.position = pos;					//set position
		temp.transform.rotation = Quaternion.identity;    //set rotation
        temp.transform.SetParent(canvasTransform);                              //make child of canvas
        temp.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);   //set scale
        temp.GetComponent<TestDamage>().Initialize(speed, direction, .5f);           //call setup method of prefab
        temp.GetComponent<Text>().text = text;                                  //set text
    }

    public void CreateText(Vector3 pos, string text, float speed)                       //same as above but only variable speed
    {

		GameObject temp = Instantiate(textPrefab);
		temp.transform.position = pos;
		temp.transform.rotation = Quaternion.identity;
        temp.transform.SetParent(canvasTransform);
        temp.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        temp.GetComponent<TestDamage>().Initialize(speed, new Vector3(0, 1, 0), .5f);
        temp.GetComponent<Text>().text = text;
    }

    public void CreateText(Vector3 pos, string text,  Vector3 direction)            //same as above but only variable direction
    {
		GameObject temp = Instantiate(textPrefab);
		temp.transform.position = pos;
		temp.transform.rotation = Quaternion.identity;
		temp.transform.SetParent(canvasTransform);
        temp.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        temp.GetComponent<TestDamage>().Initialize(1f, direction, .5f);
        temp.GetComponent<Text>().text = text;
    }

    public void CreateText(Vector3 pos, string text)                                //basic text creation method
    {
<<<<<<< HEAD
		GameObject temp = Instantiate(textPrefab);
		temp.transform.position = pos;
		temp.transform.rotation = Quaternion.identity;
=======
        GameObject temp = Instantiate(textPrefab);
        temp.transform.position = pos;
        temp.transform.rotation = Quaternion.identity;
>>>>>>> 5107cb68d66cb6b71c9b61881e1b644f0bc1290a
        temp.transform.SetParent(canvasTransform);
        temp.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        temp.GetComponent<TestDamage>().Initialize(1f, new Vector3(0, 1, 0), .5f);
        temp.GetComponent<Text>().text = text;
    }

    public void CreateTextFade(Vector3 pos, string text, float speed, Vector3 direction, float fade)    //create text with certain speed and direction
    {
<<<<<<< HEAD

        GameObject temp = Instantiate(textPrefab);      //instantiate prefab at location
        temp.transform.position = pos;                  //set position
        temp.transform.rotation = Quaternion.identity;    //set rotation
        temp.transform.SetParent(canvasTransform);                              //make child of canvas
        temp.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);   //set scale
        temp.GetComponent<TestDamage>().Initialize(speed, direction, fade);           //call setup method of prefab
        temp.GetComponent<Text>().text = text;                                  //set text
    }

    public void CreateTextFade(Vector3 pos, string text, float speed, float fade)                       //same as above but only variable speed
    {

=======
>>>>>>> 5107cb68d66cb6b71c9b61881e1b644f0bc1290a
        GameObject temp = Instantiate(textPrefab);
        temp.transform.position = pos;
        temp.transform.rotation = Quaternion.identity;
        temp.transform.SetParent(canvasTransform);
        temp.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        temp.GetComponent<TestDamage>().Initialize(speed, new Vector3(0, 1, 0), fade);
        temp.GetComponent<Text>().text = text;
    }

    public void CreateTextFade(Vector3 pos, string text, Vector3 direction, float fade)            //same as above but only variable direction
    {
        GameObject temp = Instantiate(textPrefab);
        temp.transform.position = pos;
        temp.transform.rotation = Quaternion.identity;
        temp.transform.SetParent(canvasTransform);
        temp.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        temp.GetComponent<TestDamage>().Initialize(1f, direction, fade);
        temp.GetComponent<Text>().text = text;
    }

    public void CreateTextFade(Vector3 pos, string text, float fade)                                //basic text creation method
    {
        GameObject temp = Instantiate(textPrefab);
        temp.transform.position = pos;
        temp.transform.rotation = Quaternion.identity;
        temp.transform.SetParent(canvasTransform);
        temp.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        temp.GetComponent<TestDamage>().Initialize(1f, new Vector3(0, 1, 0), fade);
        temp.GetComponent<Text>().text = text;
    }
}
