﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using menu;

public class AppData : MonoBehaviour
{
    protected int AppID;
    protected string AppName;
    public List<Widget> Widgets;
    public List<GameObject> SubScreens;
    public MenuManager Menu;

    protected int WidgetIndex;

    public virtual void Init()
    {
        AppID = -1;
        Widgets = null;
        AppName = "Boo!";
    }

    public virtual void Draw()
    {
        // All Updates to screens and widgets happen in here.
    }

    public virtual void Input(Inputs In)
    {
        // Handling of Input per app happens here
    }

    public void AddWidget(Widget widget)
    {
        if (Widgets == null)
        {
            Widgets = new List<Widget>();
        }

        Widgets.Add(widget);
    }
}
