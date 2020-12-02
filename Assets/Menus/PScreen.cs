using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PScreen : ScriptableObject
{
    GameObject Screen;
    Shader ScreenShader;
    public GameObject CurrentScreen;
    bool isOpened;

    public void Open(GameObject CScreen) // Instantiates the GameObject Screen. This is meant only to display what needs to be drawn in the current design.
    {
        if (CScreen)
        {
            if (CurrentScreen)
            {
                Destroy(CurrentScreen);
            }
            
            CurrentScreen = CScreen; 
            Screen = GameObject.Find("Menu");
            CurrentScreen = Instantiate(CurrentScreen);
            CurrentScreen.GetComponent<ScreenData>().Init();
            CurrentScreen.transform.SetParent(Screen.transform);
            CurrentScreen.transform.localPosition = new Vector2(0, 0);
        }
        Screen.SetActive(true);
    }

    public GameObject GetScreen
    {     
        get
        {
            return Screen;
        }
    }

    public void Draw()
    {
        // This is more like an update function? OR is this what spawns the screen from the get go.
        // Should I use shaders on the screen to make it look more lcd like?
    }

    public void Close()
    {
        Destroy(CurrentScreen);    
        Screen.SetActive(false);
    }
}
