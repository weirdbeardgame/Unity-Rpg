using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleItemMenu : BattleMIface
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

    public void Open(Creature Opener)
    {

    }

    public override void Open()
    {
        //menu.Close();

        for (int i = 0; i < inventory.ItemList.Count; i++)
        {
            Instantiate(Temp);
        }
    }

    public override void Close()
    {

    }

}
