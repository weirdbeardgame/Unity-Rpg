using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using menu;
public class PauseApp : AppData
{
    public GameObject ItemWidget;
    public GameObject WeaponMenuWidget;

    public override void Init()
    {
        appID = 0;
        appName = "Pause";
        properties = new List<MenuProperties>();
        properties.Add(MenuProperties.APP);
        properties.Add(MenuProperties.INPUT);

        WeaponMenuWidget.GetComponent<Widget>().Instantiate();
        AddWidget(WeaponMenuWidget.GetComponent<Widget>());
        ItemWidget.GetComponent<Widget>().Instantiate();
        AddWidget(ItemWidget.GetComponent<Widget>());

    }

    public override void Input(Inputs In)
    {
        switch(In)
        {
            case Inputs.A:
            //if (position == gridWidgets[x , y])
                break;

            case Inputs.B:
                // Step back
                break;

            case Inputs.UP:
                // Grid Plus 3
                break;

            case Inputs.DOWN:
                // Grid Minus 3
                break;

            case Inputs.LEFT:
                // Grid Minus 1
                break;

            case Inputs.RIGHT:
                // Grid Plus 1
                break;
        }
    }
}
