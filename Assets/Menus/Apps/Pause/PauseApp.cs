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

                break;

            case Inputs.B:

                break;

            case Inputs.UP:

                break;

            case Inputs.DOWN:

                break;

            case Inputs.LEFT:

                break;

            case Inputs.RIGHT:

                break;

            case Inputs.START:

                break;
        }
    }
}
