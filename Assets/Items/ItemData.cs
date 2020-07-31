using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { HEALING, DAMAGING } // For later if I have other types like Key Item 
public enum AreaOfEffect { HEALTH, STRENGTH, MAGIC, SPEED }

public class ItemBuffer
{

    public int Buff;
    public ItemType Type;
    public AreaOfEffect Effect;

    public ItemBuffer()
    {

    }

    ~ItemBuffer()
    {

    }
}

public class ItemData : ScriptableObject
{
    private string _ItemName;
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
            return _ItemName;
        }

        set
        {
            _ItemName = value;
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
        switch (_Effect.Type)
        {

            case ItemType.HEALING:

                switch (_Effect.Effect)
                {

                    case AreaOfEffect.HEALTH:
                        Creature.Stats.StatList[(int)StatType.HEALTH].Stat += _Effect.Buff;
                        break;

                    case AreaOfEffect.MAGIC:
                        Creature.Stats.StatList[(int)StatType.MAGIC].Stat += _Effect.Buff;
                        break;

                    case AreaOfEffect.SPEED:
                        Creature.Stats.StatList[(int)StatType.SPEED].Stat += _Effect.Buff;
                        break;

                    case AreaOfEffect.STRENGTH:
                        Creature.Stats.StatList[(int)StatType.STRENGTH].Stat += _Effect.Buff;
                        break;

                }
                break;

            case ItemType.DAMAGING:
                switch (_Effect.Effect)
                {
                    case AreaOfEffect.HEALTH:
                        Creature.Stats.StatList[(int)StatType.HEALTH].Stat -= _Effect.Buff;
                        break;

                    case AreaOfEffect.MAGIC:
                        Creature.Stats.StatList[(int)StatType.MAGIC].Stat -= _Effect.Buff;
                        break;

                    case AreaOfEffect.SPEED:
                        Creature.Stats.StatList[(int)StatType.SPEED].Stat -= _Effect.Buff;
                        break;

                    case AreaOfEffect.STRENGTH:
                        Creature.Stats.StatList[(int)StatType.STRENGTH].Stat -= _Effect.Buff;
                        break;
                }
                break;
        }

    }
}
