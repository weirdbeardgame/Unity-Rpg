using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvetoryMenu : ScreenData
{
    Inventory Inv;
    public GameObject ItemWidget;

    // Something about SubScreens should be here.

    public override void Init()
    {
        Inv = FindObjectOfType<Inventory>();
        for (int i = 0; i < Inv.ItemList.Count; i++)
        {
            ItemWidget = Instantiate(ItemWidget);
            ItemWidget.GetComponent<InvetoryWidget>().SetItem(Inv.ItemList[i]); // Instantiate as an Item.
            Widgets.Add(ItemWidget.GetComponent<InvetoryWidget>());
        }
    }

    public override void Draw()
    {
        // Handle Subscreen Logic in here. Always 0 will be details and character select. 1 might be use or go back like a list of buttons

    }

}
