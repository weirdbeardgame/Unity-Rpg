using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemsApp : AppData
{
    Inventory Inv;
    public GameObject ItemWidget;
    GameObject Sub;
    GameObject Rect;

    ItemData SelectedItem;

    // Something about SubScreens should be here.

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

    void SelectItem()
    {
        if(!SelectedItem)
        {
            SelectedItem = Widgets[WidgetIndex].GetComponent<InvetoryWidget>().item;
        }
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

                break;

            case Inputs.B:

                break;

            case Inputs.UP:

                break;

            case Inputs.DOWN:

                break;

            case Inputs.LEFT:

                break;

            case Inputs.RIGHT:

                break;

            case Inputs.START:

                break;
        }
    }

}
