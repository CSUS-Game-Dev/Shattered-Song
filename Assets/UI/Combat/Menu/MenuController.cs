using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour, IControllable
{
    public List<GameObject> menuButtons;
    public GameObject menuCursorPrefab;
    private MenuCursor activeCursor = null;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && activeCursor == null)
        {
            GameObject temp = Instantiate(menuCursorPrefab, transform.parent);
            activeCursor = temp.GetComponent<MenuCursor>();
            activeCursor.setup(menuButtons);
            Controller.instance.addListener(activeCursor);
        }
    }

    public void setActiveCursor(ICursor cursor)
    {

    }
}
