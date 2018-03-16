using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageTextManager : MonoBehaviour {

    private static DamageTextManager instance;

    public GameObject textPrefab;
    public RectTransform canvasTransform;

    public static DamageTextManager Instance
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

    public void CreateText(Vector3 pos, string text, float speed, Vector3 direction)
    {
        GameObject temp = Instantiate(textPrefab);
        temp.transform.position = pos;
        temp.transform.rotation = Quaternion.identity;
        temp.transform.SetParent(canvasTransform);
        temp.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        temp.GetComponent<TestDamage>().Initialize(speed, direction);
        temp.GetComponent<Text>().text = text;
    }

    public void CreateText(Vector3 pos, string text, float speed)
    {
        GameObject temp = Instantiate(textPrefab);
        temp.transform.position = pos;
        temp.transform.rotation = Quaternion.identity;
        temp.transform.SetParent(canvasTransform);
        temp.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        temp.GetComponent<TestDamage>().Initialize(speed, new Vector3(0, 1, 0));
        temp.GetComponent<Text>().text = text;
    }

    public void CreateText(Vector3 pos, string text,  Vector3 direction)
    {
        GameObject temp = Instantiate(textPrefab);
        temp.transform.position = pos;
        temp.transform.rotation = Quaternion.identity;
        temp.transform.SetParent(canvasTransform);
        temp.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        temp.GetComponent<TestDamage>().Initialize(3f, direction);
        temp.GetComponent<Text>().text = text;
    }

    public void CreateText(Vector3 pos, string text)
    {
        GameObject temp = Instantiate(textPrefab);
        temp.transform.position = pos;
        temp.transform.rotation = Quaternion.identity;
        temp.transform.SetParent(canvasTransform);
        temp.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        temp.GetComponent<TestDamage>().Initialize(3f, new Vector3(0, 1, 0));
        temp.GetComponent<Text>().text = text;
    }
}
