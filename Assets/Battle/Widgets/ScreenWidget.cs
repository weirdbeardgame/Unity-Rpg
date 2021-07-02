using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWidget : Widget
{
    [SerializeField]
    private int menuID = 0;
    commandMenus menu;
    void Start()
    {
        menu = GetComponent<commandMenus>();
    }

    public override void Execute()
    {
        base.Execute();
        // Depends on what index non related to job. Should I have a different struct for that?
        menu.Open(menuID);
    }
    void Update()
    {
        
    }
}
