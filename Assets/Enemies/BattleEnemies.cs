using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEnemies : MonoBehaviour
{
    // Just the enemies spawned in scene
    private List<Baddies> badParty;
    public List<Baddies> BadParty
    {
        get
        {
            return badParty;
        }
        private set
        {
            badParty = value;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Insert(Baddies bad)
    {
        if (badParty == null)
        {
            badParty = new List<Baddies>();
        }
        badParty.Add(bad);
    }

    public void Remove(Baddies bad)
    {
        if (badParty == null)
        {
            return;
        }
        badParty.Remove(bad);
    }

    public void Battle(int i)
    {
        switch (badParty[i].Data.state)
        {
            case BattleState.WAIT:
            // Fill Guage
            break;

            case BattleState.COMMAND:
            // Run AI and select a command
            break;

            case BattleState.SELECTION:
            // Run AI and select a target
            break;

            case BattleState.ACTION:
            // Take action against thine enemy the player!
            break;
        }
    }

    void Kill(int id)
    {
        // Doesn't that mean this'll destroy itself?
        badParty[id].Data.Die();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
