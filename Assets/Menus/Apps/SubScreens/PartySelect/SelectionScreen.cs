using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionScreen : SubScreen
{
    // Who we usin this shit on?
    Party SelectionParty;

    // Start is called before the first frame update
    public override void Init()
    {
        foreach (Creature Member in SelectionParty.PartyMembers )
        {
            // Create Widget and fill with character data that's relevant. I need to know what Item or Weapon and the stats they're affecting
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
