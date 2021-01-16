using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using menu;
public class PScreen : MonoBehaviour
{  
    public GameObject Screen;
    Shader ScreenShader;
    public GameObject CurrentScreen;
    public GameObject SubScreen;
    bool isOpened;

    public void Open(GameObject CScreen) // Instantiates the GameObject Screen. This is meant only to display what needs to be drawn in the current design.
    {
        if (CScreen)
        {
            if (CurrentScreen)
            {
                Destroy(CurrentScreen);
            }
            
            CurrentScreen = (GameObject)Instantiate(CScreen); // Instantiate app prefab
            CurrentScreen.GetComponent<AppData>().Init();
            CurrentScreen.transform.SetParent(Screen.transform);
            CurrentScreen.transform.localPosition = new Vector2(0, 0);
        }
        //Screen.SetActive(true);
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

    }

    void DrawScreen()
    {
        // This is more like an update function? OR is this what spawns the screen from the get go.
        // Should I use shaders on the screen to make it look more lcd like?
        if (CurrentScreen)
        {
            CurrentScreen.GetComponent<AppData>().Draw();
        }
    }

    void DrawSubScreen()
    {
        if (SubScreen)
        {
            // This needs to be aware of Other screen?
            SubScreen.GetComponent<AppData>().Draw();
        }
    }

    public void Close()
    {
        foreach(Transform Widget in CurrentScreen.transform)
        {
            Destroy(Widget.gameObject);
        }

        Destroy(CurrentScreen);    
        //Screen.SetActive(false);
    }
}
