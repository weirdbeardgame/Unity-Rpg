using System.Collections;
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
        // Handle Subscreen Logic in here. Always 0 will be details and character select. 1 might be use or go back like a list of buttons
        Sub.GetComponentInChildren<TextMeshProUGUI>().text = Widgets[0].GetComponent<InvetoryWidget>().item.ItemDescription; // This assumes SubScreen 0 for now.
    }

    public override void Input(Inputs In)
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

}
