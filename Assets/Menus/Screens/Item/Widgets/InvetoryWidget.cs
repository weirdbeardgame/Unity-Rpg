using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InvetoryWidget : Widget
{
    ItemData item;
    Inventory inventory;

    public void SetItem(ItemData data)
    {
        item = data;
        Text.GetComponent<TextMeshProUGUI>().text = item.ItemName;
        Icon.GetComponent<SpriteRenderer>().sprite = item.GetSprite();
    }

    public override void Execute()
    {
        //item.Use(); // This is where we would add the sub screen. Who do you want to use it on!
    }

}
