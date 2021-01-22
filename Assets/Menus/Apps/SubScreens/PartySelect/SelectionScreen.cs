using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using menu;
public class SelectionScreen : AppData
{
    // Who we usin this shit on?
    Party SelectionParty;
    public GameObject CharacterSelect;
    public GameObject ToParent;
    public ItemData Item;

    // Start is called before the first frame update
    public override void Init()
    {
        Properties = new List<MenuProperties>();
        Properties.Add(MenuProperties.SUBAPP);
        Properties.Add(MenuProperties.INPUT);

        SelectionParty = FindObjectOfType<Party>();

        foreach (Creature Member in SelectionParty.PartyMembers )
        {
            // Create Widget and fill with character data that's relevant. I need to know what Item or Weapon and the stats they're affecting
            CharacterSelect = Instantiate(CharacterSelect, ToParent.transform, false) as GameObject;
            CharacterSelect.GetComponent<SelectorWidget>().Init(Member, Item);
            CharacterSelect.transform.SetParent(ToParent.transform);
            CharacterSelect.transform.localPosition = ToParent.transform.localPosition;
            AddWidget(CharacterSelect.GetComponent<SelectorWidget>());
        }
    }

    public override void Input(Inputs In)
    {
        base.Input(In); // Use to Capture from Current Menu. This is just a screen that can be opened.
        
        switch(In)     
        {

            case Inputs.DOWN:
                // Assume Verticle list?
                WidgetIndex += 1;
                break;

            case Inputs.UP:
                WidgetIndex -= 1;
                break;

            case Inputs.A:
                Widgets[WidgetIndex].Execute();
                break;

            case Inputs.B:
                //Close();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var Widget in Widgets)
        {
            Widget.Draw(); // This seems mildly inefficent
        }
    }
}
