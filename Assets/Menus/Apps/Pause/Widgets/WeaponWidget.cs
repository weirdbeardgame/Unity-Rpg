using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using menu;

public class WeaponWidget : Widget
{
    int MenuID = 2;
    MenuManager Manager;

    public override void Execute()
    {
        Manager = FindObjectOfType<MenuManager>();
        Manager.Open(MenuID);
    }
}
