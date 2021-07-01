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
        _Menu = FindObjectOfType<commandMenus>();
        _Menu.AddSubMenu(0, this);
    }

    void use()
    {
        ItemMessage = new SkillMessage();
        ItemMessage.construct(null, MessageType.BATTLE, CommandType.ITEM, 0);
    }

    public void Open(Creature Opener)
    {

    }


    public void Open()
    {
        _Menu.Close();

        for (int i = 0; i < _Inventory.ItemList.Count; i++)
        {


            Instantiate(Temp);
            _Menu.AddWidget(Temp);
        }
    }

    public void Close()
    {

    }

}
