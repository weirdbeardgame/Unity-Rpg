using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PScreen : MonoBehaviour
{
    int Width, Height;

    Shader ScreenShader;

    GameObject Screen;

    public List<ScreenData> Screens;

    ScreenData CurrentScreen;

    bool isOpened;

    // Start is called before the first frame update
    void Start()
    {      
        Screen = GameObject.Find("Menu");
        Screen.AddComponent<RectTransform>();
        Screen.AddComponent<Image>();
        Screen.SetActive(false);
    }
    public void Open(ScreenData CScreen)
    {
        if (CScreen)
        {
            CurrentScreen = CScreen;       
        }

        // Play animation of screen opening.
        for (int i = 0; i < CurrentScreen.Widgets.Count; i++)
        {
            CurrentScreen.Widgets[i].Instantiate(); // Something like that to Start
        }

        Screen.SetActive(true);
    }

    public void Close()
    {
        // Play animation of Screen closing
        for (int i = 0; i < CurrentScreen.Widgets.Count; i++)
        {
            Destroy(CurrentScreen.Widgets[i]);
        }
     
        Screen.SetActive(false);
    }

    public void ChangeScreen(int ScreenID)
    {
        // Load Other Screens
        Open(Screens[ScreenID]);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel") && !isOpened)
        {
            Open(Screens[0]);
        }
        else if(isOpened && Input.GetButtonDown("Cancel"))
        {
            Close();
        }
    }
}
