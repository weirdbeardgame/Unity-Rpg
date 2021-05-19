 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using menu;
public class PScreen : MonoBehaviour
{
    [SerializeField]
    GameObject Screen;
    public List<GameObject> SubScreens;
    [SerializeField]
    GameObject PlayerObject;
    [SerializeField]
    public GameObject CurrentScreen;
    Shader ScreenShader;
    bool isOpened;

    public void Open(GameObject CScreen) // Instantiates the GameObject Screen. This is meant only to display what needs to be drawn in the current design.
    {     
        if (CurrentScreen)            
        {
            Close();
        }

        if (CScreen)
        {
            Vector2 Pos = new Vector2(PlayerObject.transform.localPosition.x + 5, PlayerObject.transform.localPosition.y);
            CurrentScreen = Instantiate(CScreen, Pos, Quaternion.identity, Screen.transform) as GameObject; // Instantiate app prefab
            CurrentScreen.transform.SetParent(Screen.transform);
            CurrentScreen.GetComponent<AppData>().Init();
        }
    }

    public GameObject GetScreen
    {     
        get
        {
            return CurrentScreen;
        }
    }

    public void Close()
    {
        foreach(Transform Widget in CurrentScreen.transform)
        {
            Destroy(Widget.gameObject);
        }

        Destroy(CurrentScreen);    
    }
}
