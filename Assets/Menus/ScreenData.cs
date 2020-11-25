using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenData : MonoBehaviour
{
    protected int ScreenID;
    public List <Widget> Widgets;
    protected string ScreenName;

    public virtual void Init()
    {
        ScreenID = -1;
        Widgets = null;
        ScreenName = "Boo!";
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
