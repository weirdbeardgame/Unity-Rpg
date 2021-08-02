using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEnemies : MonoBehaviour
{
    List<Baddies> badParty;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Insert(Baddies bad)
    {
        if (badParty == null)
        {
            badParty = new List<Baddies>();
        }
        badParty.Add(bad);
    }

    void Remove(Baddies bad)
    {
        if (badParty == null)
        {
            return;
        }
        badParty.Remove(bad);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
