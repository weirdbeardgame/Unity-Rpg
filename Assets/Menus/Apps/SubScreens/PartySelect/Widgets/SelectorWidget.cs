using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SelectorWidget : Widget
{
    Creature CharacterData;
    ItemData Item; // Assume somewhere along the line this is constructed from Item / ItemList that it holds

    Stats StatData; // To display the currently affected Stat. This could be considered the common Stat between Item and player or the affected Stat. 
                    // This is specifically to display the player stat? I kinda just put this here to have an ancor to my thoughts

    public void Init(Creature C, ItemData I)
    {
        CharacterData = C;
        Item = I;
    }

    public override void Draw()
    {
        // Draw Character info in here.
        // GameObject Text could be like. 
        // Health or whatever but I think I need Stat Slots?
    }

    public override void Execute()
    {
        // Apply Item?
        Item.Use(CharacterData);
    }
}
