using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponWidget : Widget
{
    int MenuID = 2;
    PScreen Screen;

    public override void Execute()
    {
        Screen.ChangeScreen(MenuID);
    }
}
