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

public class ItemData : ScriptableObject
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

    public void Use(Creature Creature)
    {
        Creature.Stats.statList[(int)effect.effect].stat += effect.buff.stat;
    }
}