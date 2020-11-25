using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using menu;

public class ItemWidget : Widget
{
    int MenuID = 1; // Menu 1 will always be Item
    PScreen Screen;

    public override void Execute()
    {
        Screen = FindObjectOfType<PScreen>();
        Screen.ChangeScreen(MenuID);
    }
}
