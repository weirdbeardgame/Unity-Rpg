using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PScreen : MonoBehaviour
{
    GameObject ScreenSpace;

    int Width, Height;

    Shader ScreenShader;

    List<Widget> Widgets;
    List<ScreenData> Screens;

    ScreenData CurrentScreen;

    // Start is called before the first frame update
    void Start()
    {
        ScreenSpace.AddComponent<RectTransform>();
        ScreenSpace.AddComponent<Image>();

    }
    public void Open()
    {
        // Play animation of screen opening.
        ChangeScreen(0).Widgets = Widgets;
        ScreenSpace.SetActive(true);
    }

    public void Close()
    {
        // Play animation of Screen closing
        ScreenSpace.SetActive(false);
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
