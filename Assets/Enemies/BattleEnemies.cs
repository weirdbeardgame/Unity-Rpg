using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEnemies : MonoBehaviour
{
    // Just the enemies spawned in scene
    public List<GameObject> badParty
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

    public void Insert(GameObject bad)
    {
        if (badParty == null)
        {
            badParty = new List<GameObject>();
        }
        badParty.Add(bad);
    }

    public void Remove(GameObject bad)
    {
        if (badParty == null)
        {
            return;
        }
        badParty.Remove(bad);
    }

    void Kill(int id)
    {
        // Doesn't that mean this'll destroy itself?
        //badParty[id].GetComponent<Baddies>().Die();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
