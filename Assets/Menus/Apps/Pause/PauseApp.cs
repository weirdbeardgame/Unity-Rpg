using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseApp : AppData
{
    public GameObject ItemWidget;
    public GameObject WeaponWidget;

    public override void Init()
    {
        AppID = 0;
        AppName = "Pause";
        ItemWidget.GetComponent<Widget>().Instantiate();
        AddWidget(ItemWidget.GetComponent<Widget>());
        WeaponWidget.GetComponent<Widget>().Instantiate();
        AddWidget(WeaponWidget.GetComponent<Widget>());
    }

    public override void Input(Inputs In)
    {
        switch(In)
        {
            case Inputs.A:
                Widgets[WidgetIndex].Execute();
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
