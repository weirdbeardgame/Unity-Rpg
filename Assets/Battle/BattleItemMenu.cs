using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleItemMenu : MonoBehaviour, BattleMIface
{

    commandMenus _Menu;
    Inventory _Inventory;
    SkillMessage ItemMessage;

    public GameObject Temp;

    // Start is called before the first frame update
    void Start()
    {
        _Menu = FindObjectOfType<commandMenus>();
        _Inventory = FindObjectOfType<Inventory>();
        Add();
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
