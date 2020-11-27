using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvetoryWidget : Widget
{
    ItemData item;
    Inventory inventory;

    void SetItem(ItemData data)
    {
        item = data;
    }

    public override void Execute()
    {
        //item.Use(); // This is where we would add the sub screen. Who do you want to use it on!
    }

}
