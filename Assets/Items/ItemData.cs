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

public class ItemData : Asset
{
    // Meant for In UI use
    [System.NonSerialized]
    public GameObject prefab;

    public string name;
    public string description;
    public string prefabPath;

    public int itemID;
    public int cost;
    public int amount = 0;
    public int maxAmount = 99;

    public ItemBuffer effect;

    public override Asset CreateAsset()
    {
        var bInst = Resources.Load(prefabPath, typeof(GameObject)) as GameObject;
        if (!prefab)
        {
            prefab = bInst;
        }
        return this;
    }

    public override Asset DestroyAsset()
    {
        MonoBehaviour.Destroy(prefab);
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