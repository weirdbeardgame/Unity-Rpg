using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum itemState { RECIEVED, USED };

public class InventoryMessage : ScriptableObject
{

    itemState state;

    Messaging messenger;

    ItemData CurrentItem;

    public void construct(ItemData current, itemState stateI)
    {

        CurrentItem = current;

        state = stateI;

        messenger = FindObjectOfType<Messaging>();

        //messenger.Send(this, MessageType.INVENTORY);

    }

    public itemState getState()
    {
        return state;
    }

    public int GetID()
    {
        return CurrentItem.itemID;
    }

    public ItemData getItem()
    {
        return CurrentItem;
    }

}
