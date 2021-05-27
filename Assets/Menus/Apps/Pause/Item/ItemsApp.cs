using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using menu;
using TMPro;

public class ItemsApp : AppData
{
    Inventory Inv;
    public GameObject ItemWidget;
    public GameObject ItemLayout;
    public GameObject Sub;
    GameObject Rect;

    ItemData SelectedItem;

    public override void Init()
    {   
        properties = new List<MenuProperties>();
        properties.Add(MenuProperties.APP);
        properties.Add(MenuProperties.INPUT);

        menu = FindObjectOfType<MenuManager>();

        appID = 1;
        appName = "Items";
        Inv = FindObjectOfType<Inventory>();
        Rect = GameObject.Find("SubScreen");

        for (int i = 0; i < Inv.ItemList.Count; i++)
        {
            Instantiate(ItemWidget);
            ItemWidget.GetComponent<InvetoryWidget>().SetItem(Inv.ItemList[i]); // Instantiate as an Item.
            ItemWidget.transform.SetParent(ItemLayout.transform);
            widgets.Add(ItemWidget.GetComponent<InvetoryWidget>());
        }

        Sub = Instantiate(subScreens[0], Rect.transform, false);
        Sub.transform.SetParent(Rect.transform);
        base.Init();
    }

    public override void Draw() 
    {
        // Handle Subscreen Logic in here?
        Sub.GetComponent<AppData>().Draw();
    }
}
