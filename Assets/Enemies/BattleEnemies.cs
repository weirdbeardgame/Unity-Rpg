using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEnemies : MonoBehaviour
{
    public List<Baddies> badParty
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

    void Kill(int id)
    {
        badParty[id].Die();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
