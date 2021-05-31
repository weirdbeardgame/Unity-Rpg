using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using menu;
public class PauseApp : AppData
{
    public override void Init()
    {
        appID = 1;
        appName = "Pause";
        properties = new List<MenuProperties>();
        properties.Add(MenuProperties.APP);
        properties.Add(MenuProperties.INPUT);
        for (int i = 0; i < widgetsToAdd.Count; i++)
        {
            AddWidget(widgetsToAdd[i]);
        }
    }
}
