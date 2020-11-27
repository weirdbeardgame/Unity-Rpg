using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PScreen : ScriptableObject
{
    GameObject Screen;
    Shader ScreenShader;
    GameObject CurrentScreen;
    public List<GameObject> Screens;
   
    bool isOpened;

    // Start is called before the first frame update
    void Start()
    {      
        Screen = GameObject.Find("Menu");
        Screen.AddComponent<RectTransform>();
        Screen.AddComponent<Image>();
        Screen.SetActive(false);
    }
    public void Open(GameObject CScreen)
    {
        if (CScreen)
        {
            CurrentScreen = CScreen;
            CurrentScreen = Instantiate(CurrentScreen);
            CurrentScreen.transform.SetParent(Screen.transform);
            CurrentScreen.transform.localPosition = new Vector2(0, 0);
        }
        Screen.SetActive(true);
    }

    public void Close()
    {
        Destroy(CurrentScreen);    
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
            isOpened = true;
        }
        else if(isOpened && Input.GetButtonDown("Cancel"))
        {
            Close();
            isOpened = false;
        }
    }
}
