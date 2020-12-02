using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using menu;

public class ItemWidget : Widget
{
    int MenuID = 1; // Menu 1 will always be Item
    MenuManager Manager;

    public override void Execute()
    {
        Manager = FindObjectOfType<MenuManager>();
        Manager.Open(MenuID);
    }
}
