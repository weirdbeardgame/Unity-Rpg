using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : ScreenData
{
    ItemWidget ItemWidget;
    WeaponWidget WeaponWidget;

    public override void Init()
    {
        ScreenID = 0;
        ScreenName = "Pause";
        ItemWidget.Instantiate();
        AddWidget(ItemWidget);
        WeaponWidget.Instantiate();
        AddWidget(WeaponWidget);
    }
}
