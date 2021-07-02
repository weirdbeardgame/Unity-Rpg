using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using menu;

public class OpenMenuWidget : Widget
{
    [SerializeField]
    private int menuID = 0;
    MenuManager manager;

    public override void Execute()
    {
        manager = FindObjectOfType<MenuManager>();
        manager.Open(menuID);
    }
}
