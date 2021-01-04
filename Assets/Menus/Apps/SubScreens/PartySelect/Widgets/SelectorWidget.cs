using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorWidget : Widget
{
    Creature CharacterData;
    ItemData Item; // Assume somewhere along the line this is constructed from Item / ItemList that it holds

    void Init(Creature C, ItemData I)
    {
        CharacterData = C;
        Item = I;
    }

    void Draw()
    {
        // Draw Character info in here.
    }

    public override void Execute()
    {
        // Apply Item?
        Item.Use(CharacterData);

    }
}
