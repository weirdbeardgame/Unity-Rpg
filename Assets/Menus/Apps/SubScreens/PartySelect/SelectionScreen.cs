using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using menu;
using TMPro;
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
        properties = new List<MenuProperties>();
        properties.Add(MenuProperties.SUBAPP);
        properties.Add(MenuProperties.INPUT);

        SelectionParty = FindObjectOfType<Party>();

        foreach (Creature Member in SelectionParty.PartyMembers )
        {
            // Create Widget and fill with character data that's relevant. I need to know what Item or Weapon and the stats they're affecting
            CharacterSelect = Instantiate(CharacterSelect, ToParent.transform, false) as GameObject;
            CharacterSelect.GetComponent<SelectorWidget>().Init(Member, Item);
            AddWidget(CharacterSelect.GetComponent<SelectorWidget>(), 0);
            CharacterSelect.transform.SetParent(ToParent.transform);
            CharacterSelect.transform.localPosition = ToParent.transform.localPosition;
            CharacterSelect.GetComponentInChildren<TextMeshProUGUI>().text = Member.Stats.statList[(int)Item.Effect.Effect].ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var Widget in widgets)
        {
            //Widget.Draw(); // This seems mildly inefficent
        }
    }
}
