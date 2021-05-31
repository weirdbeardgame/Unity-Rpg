using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using menu;

public class MainScreen : AppData
{
    // Start is called before the first frame update
    public override void Init()
    {
        appID = 0;
        appName = "Main";
        properties = new List<MenuProperties>();
        properties.Add(MenuProperties.APP);
        properties.Add(MenuProperties.INPUT);
        //properties.Add(MenuProperties.HORIZONTAL);

        for (int i = 0; i < widgetsToAdd.Count; i++)
        {
            AddWidget(widgetsToAdd[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
