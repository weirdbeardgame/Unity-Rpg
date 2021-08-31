﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InvetoryWidget : Widget
{
    public ItemData item;
    public Creature ToUse;
    Inventory inventory;

    public void SetItem(ItemData data)
    {
        item = data;
    }

    public override void Execute()
    {
        item.Use(ToUse); // This is where we would add the sub screen. Who do you want to use it on!
    }

}
