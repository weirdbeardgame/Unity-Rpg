using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleItemMenu : BattleMenu
{

    commandMenus menu;
    Inventory inventory;

    public GameObject Temp;

    // Start is called before the first frame update
    void Start()
    {
        menu = FindObjectOfType<commandMenus>();
        inventory = FindObjectOfType<Inventory>();
    }

    public void Add()
    {
        menu = FindObjectOfType<commandMenus>();
    }

    void use()
    {

    }
}
