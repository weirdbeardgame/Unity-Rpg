using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : ScreenData
{
    public GameObject ItemWidget;
    public GameObject WeaponWidget;

    public override void Init()
    {
        ScreenID = 0;
        ScreenName = "Pause";
        ItemWidget.GetComponent<Widget>().Instantiate();
        AddWidget(ItemWidget.GetComponent<Widget>());
        WeaponWidget.GetComponent<Widget>().Instantiate();
        AddWidget(WeaponWidget.GetComponent<Widget>());
    }
}
