using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InvetoryMenu : ScreenData
{
    Inventory Inv;
    public GameObject ItemWidget;
    GameObject Sub;

    ItemData SelectedItem;

    // Something about SubScreens should be here.

    public override void Init()
    {
        Inv = FindObjectOfType<Inventory>();
        Sub = GameObject.Find("Rectangle");
        for (int i = 0; i < Inv.ItemList.Count; i++)
        {
            ItemWidget = Instantiate(ItemWidget);
            ItemWidget.GetComponent<InvetoryWidget>().SetItem(Inv.ItemList[i]); // Instantiate as an Item.
            Widgets.Add(ItemWidget.GetComponent<InvetoryWidget>());
        }

        Sub = Instantiate(SubScreens[0]);
    }

    public override void Draw()
    {
        // Handle Subscreen Logic in here. Always 0 will be details and character select. 1 might be use or go back like a list of buttons
        Sub.GetComponentInChildren<TextMeshProUGUI>().text = Widgets[0].GetComponent<InvetoryWidget>().item.ItemDescription; // This assumes SubScreen 0 

    }
    private void Update()
    {
        
    }

}
