 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using menu;
public class PScreen : MonoBehaviour
{  

    public GameObject Screen;
    public List<GameObject> SubScreens;
    public GameObject PlayerObject;
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
        foreach(var SubScreen in SubScreens)
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
    }
}
