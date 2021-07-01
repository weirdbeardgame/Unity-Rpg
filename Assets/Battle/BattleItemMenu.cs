using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleItemMenu : BattleMIface
{

    commandMenus menu;
    Inventory inventory;
    SkillMessage itemMessage;

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
        menu.AddSubMenu(0, this);
    }

    void use()
    {
        itemMessage = new SkillMessage();
        itemMessage.construct(null, MessageType.BATTLE, CommandType.ITEM, 0);
    }

    public void Open(Creature Opener)
    {

    }


    public void Open()
    {
        //menu.Close();

        for (int i = 0; i < inventory.ItemList.Count; i++)
        {


            Instantiate(Temp);
            menu.AddWidget(Temp);
        }
    }

    public override void Close()
    {

    }

}
