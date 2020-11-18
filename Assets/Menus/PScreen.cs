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

    List<Widget> Widgets;
    List<ScreenData> Screens;

    ScreenData CurrentScreen;

    // Start is called before the first frame update
    void Start()
    {        
        Screen = GameObject.Find("Menu");
        Screen.AddComponent<RectTransform>();
        Screen.AddComponent<Image>();
    }
    public void Open()
    {
        // Play animation of screen opening.
        ChangeScreen(0).Widgets = Widgets;
        Screen.SetActive(true);
    }

    public void Close()
    {
        // Play animation of Screen closing
        Screen.SetActive(false);

    }

    public ScreenData ChangeScreen(int ScreenID)
    {
        // Load Other Screens
        return Screens[ScreenID];
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
