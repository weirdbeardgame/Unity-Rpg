using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { HEALING, DAMAGING } // For later if I have other types like Key Item 
public enum AreaOfEffect { HEALTH, STRENGTH, MAGIC, SPEED }

public class ItemBuffer
{
    public Stats buff;
    public ItemType type;
    public AreaOfEffect effect;
}

public class ItemData : ScriptableObject, IAsset
{
    // Meant for In UI use
    [System.NonSerialized]
    public GameObject prefab;
    public string name;
    public string description;
    public string prefabPath;
    public int itemID;
    public int cost;
    public ItemBuffer effect;
    public int maxAmount = 99;
    public int amount = 0;

    public IAsset CreateAsset()
    {
        var bInst = Resources.Load(prefabPath, typeof(GameObject)) as GameObject;
        if (!prefab)
        {
            prefab = Instantiate(bInst);
            prefab.SetActive(false);
        }
        return this;
    }

    public IAsset DestroyAsset()
    {
        Destroy(prefab);
        name = "";
        description = "";
        itemID = -1;
        amount = -1;
        return null;
    }

    public void Use(Creature creature)
    {
        creature.Stats.statList[(int)effect.effect].stat += effect.buff.stat;
    }
}