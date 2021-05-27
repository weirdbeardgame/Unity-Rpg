using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using menu;

public class WeaponApp : AppData
{
    Inventory Inv;
    public GameObject WeaponWidget;
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
            Instantiate(WeaponWidget);
            //WeaponWidget.GetComponent<WeaponWidget>().ini // Instantiate as an Item.
            //WeaponWidget.transform.SetParent(ItemLayout.transform);
            //widgets.Add(ItemWidget.GetComponent<InvetoryWidget>());
        }

        Sub = Instantiate(subScreens[0], Rect.transform, false);
        Sub.transform.SetParent(Rect.transform);
        base.Init();
    }
}