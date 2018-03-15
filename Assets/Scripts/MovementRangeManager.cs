using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementRangeManager : MonoBehaviour {

    // Use this for initialization
    private static MovementRangeManager instance;  //reference to itself

    public GameObject MovementPrefab;   //prefab for text
    public BattleMap map;

    public static MovementRangeManager Instance    //reurns reference to singleton so no multipl copies
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<MovementRangeManager>();
            }
            return instance;
        }
    }
}
