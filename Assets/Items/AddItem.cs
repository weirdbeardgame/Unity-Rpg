using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItem : MonoBehaviour
{
    Item ItemIndex;

    public int ID;

    bool IsTrigger = false;
    bool ItemGiven;

    InventoryMessage Message;

    void GiveItem()
    {

        Message = new InventoryMessage();
        ItemIndex = FindObjectOfType<Item>();
        ItemGiven = true;

        Message.construct(ItemIndex.GetItem(ID), itemState.RECIEVED);

        Debug.Log("An Item Added! " + ItemIndex.GetItem(ID).name);
        ItemIndex.AddToInventory(ID);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !ItemGiven)
        {
            IsTrigger = true;
        }

        else
        {
            IsTrigger = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (IsTrigger && !ItemGiven)
        {
            GiveItem();
        }

        else
        {
            return;
        }

    }
}
