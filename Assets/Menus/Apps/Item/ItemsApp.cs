﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using menu;
using TMPro;

public class ItemsApp : AppData
{
    Inventory Inv;
    public GameObject ItemWidget;
    public GameObject Sub;
    GameObject Rect;

    ItemData SelectedItem;

    public override void Init()
    {
        AppID = 1;
        AppName = "Items";
        Inv = FindObjectOfType<Inventory>();
        Rect = GameObject.Find("Rectangle");
        Properties = new List<MenuProperties>();
        Properties.Add(MenuProperties.APP);
        Properties.Add(MenuProperties.INPUT);

        for (int i = 0; i < Inv.ItemList.Count; i++)
        {
            ItemWidget = Instantiate(ItemWidget);
            ItemWidget.GetComponent<InvetoryWidget>().SetItem(Inv.ItemList[i]); // Instantiate as an Item.
            Widgets.Add(ItemWidget.GetComponent<InvetoryWidget>());
        }

        Sub = Instantiate(SubScreens[0]);
        Sub.transform.SetParent(Rect.transform);
        Sub.transform.localPosition = new Vector3(0, 0, 0);
    }

    public override void Draw() 
    {
        // Handle Subscreen Logic in here?
        Sub.GetComponent<AppData>().Draw();
    }

    public override void Input(Inputs In)
    {

        if (!Sub.GetComponent<AppData>())
        {
            switch (In)
            {
                case Inputs.A:
                    if (!SelectedItem)
                    {
                        SelectedItem = Widgets[WidgetIndex].GetComponent<InvetoryWidget>().item;
                        // Open character selection screen.
                        SubScreens[1].GetComponent<SelectionScreen>().Item = SelectedItem;
                        Sub = Instantiate(SubScreens[1]);
                        Sub.GetComponent<AppData>().Init();
                    }
                    break;

                case Inputs.B:
                    if (SelectedItem)
                    {
                        SelectedItem = null; // Item Select Buisness. Go back to your drinks
                    }
                    else
                    {
                        // Swap back to Pause screen.
                        Menu = FindObjectOfType<MenuManager>();
                        Menu.Open(0);
                    }
                    break;

                case Inputs.UP:
                    WidgetIndex += 1; // Vertical list so +1 in list
                    break;

                case Inputs.DOWN:
                    WidgetIndex -= 1; // Vertical list so - 1 in list
                    break;

                case Inputs.LEFT:
                    // Other screen functionality like selecting Use vs something else button wise. Assuming I add that functionality
                    break;

                case Inputs.RIGHT:
                    // Other screen functionality like selecting Use vs something else button wise. Assuming I add that functionality
                    break;
            }
        }
        else
        { 
            for (int i = 0; i < Sub.GetComponent<AppData>().PropertiesCount(); i++)
            {
                if (Sub.GetComponent<AppData>().GetProperties(i) != MenuProperties.INPUT)
                {
                    return;
                }
                else
                {
                    Sub.GetComponent<AppData>().Input(In); // Pass that shit through to SubScreeens My dude. This should be in the virtual function me thinks
                }
            }
        }
    }
}
