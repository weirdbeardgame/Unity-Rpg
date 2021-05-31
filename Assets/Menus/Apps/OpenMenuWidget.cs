using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using menu;

public class OpenMenuWidget : Widget
{
    [SerializeField]
    int MenuID = 0; // Menu 1 will always be Item
    MenuManager Manager;

    public override void Execute()
    {
        Manager = FindObjectOfType<MenuManager>();
        Manager.Open(MenuID);
    }
}
