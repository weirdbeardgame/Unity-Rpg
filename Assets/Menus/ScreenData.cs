using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenData : MonoBehaviour
{
    protected int ScreenID;
    protected string ScreenName;
    public List<Widget> Widgets;

    public List<GameObject> SubScreens;

    public virtual void Init()
    {
        ScreenID = -1;
        Widgets = null;
        ScreenName = "Boo!";
    }

    public virtual void Draw()
    {
        // All Updates to screens and widgets happen in here.
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
