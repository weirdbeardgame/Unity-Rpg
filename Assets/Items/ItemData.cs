using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { HEALING, DAMAGING } // For later if I have other types like Key Item 
public enum AreaOfEffect { HEALTH, STRENGTH, MAGIC, SPEED }

public class ItemBuffer
{
    public Stats Buff;
    public ItemType Type;
    public AreaOfEffect Effect;
}

public class ItemData : ScriptableObject
{
    private string IName;
    private string IDescrip;

    private int _ItemID;
    private int _Cost;

    private ItemBuffer _Effect;

    [System.NonSerialized]
    public int MaxAmount = 99;

    [System.NonSerialized]
    public int Amount = 0;

    Sprite Icon;
    Inventory _Inventory;


    public string ItemName
    {
        get
        {
            return IName;
        }

        set
        {
            IName = value;
        }
    }

    public string ItemDescription
    {
        get
        {
            return IDescrip;
        }

        set
        {
            IDescrip = value;
        }
    }


    public int ItemID
    {
        get
        {
            return _ItemID;
        }

        set
        {
            _ItemID = value;
        }
    }

    public Sprite GetSprite()
    {
        return Icon;
    }

    public int Cost
    {
        get
        {
            return _Cost;
        }

        set
        {
            _Cost = value;
        }
    }

    public ItemBuffer Effect
    {
        get
        {
            return _Effect;
        }

        set
        {
            _Effect = value;
        }
    }

    public void Use(Creature Creature)
    {
        Creature.Stats.StatList[(int)Effect.Effect].Stat += Effect.Buff.Stat;
    }
}