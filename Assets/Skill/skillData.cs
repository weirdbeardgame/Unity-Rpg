using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public enum AttributeType { PHYSICAL, MAGIC };
public enum SkillTypes { HEALING, DAMAGING };


public class SkillData : ScriptableObject, ActionIface
{
    public SkillTypes SkillType;
    public AttributeType Attribute;
    private string _SkillName;
    private int _SkillID = 0;
    private float _Effect;
    public StatType AffectedStat;
    public int TargetAmount;
    public Buffers Buffer;


    [System.NonSerialized]
    public int timer;
    [System.NonSerialized]
    Creature Caster;
    [System.NonSerialized]
    Creature Target;
    [System.NonSerialized]
    CommandQueue Queue;

    private float ActionWeight;

    public float GetWeight()
    {
        return ActionWeight;
    }

    public float GetBuff()
    {
        return _Effect;
    }

    public float Effect
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

    public int SkillID
    {
        get
        {
            return _SkillID;
        }

        set
        {
            _SkillID = value;
        }
    }

    public string SkillName
    {
        get
        {
            return _SkillName;
        }

        set
        {
            _SkillName = value;
        }
    }

    public void Enqueue(Creature C, Creature T)
    {
        Queue = ScriptableObject.FindObjectOfType<CommandQueue>();

        Caster = C;
        Target = T;

        //This is to determine location of Action in queue not the time to action but I guess it could be that?

        //ActionWeight = C.Stats.Speed * C.Stats.Agility / MaxTicks;

        if (Queue.Commands.Count > 0)
        {
            for (int i = 1; i < Queue.Commands.Count; i++)
            {
                if (ActionWeight > Queue.Commands[i].GetWeight())
                {
                    ActionIface temp = Queue.Commands[i];
                    Queue.Enqueue(this, i);
                    Queue.Enqueue(temp, i++);
                    break;
                }
                else
                {
                    continue;
                }
            }
        }
        else
        {
            Queue.Enqueue(this);
        }

    }

    public Creature GetCaster()
    {
        return Caster;
    }

    public Creature GetTarget()
    {
        return Target;
    }


    public void Execute() // Because Caster will be important for the absorption system
    {
        Debug.Log(this.Target.CreatureName + " BEFORE: ");
        Debug.Log("Bad Health: " + this.Target.Stats.StatList[(int)StatType.HEALTH].Stat.ToString());
        Debug.Log("Bad Magic: " + this.Target.Stats.StatList[(int)StatType.MAGIC].Stat.ToString());

        switch (SkillType)
        {
            case SkillTypes.HEALING:
                this.Target.Stats.StatList[(int)AffectedStat].Stat += _Effect;
                break;

            case SkillTypes.DAMAGING:
                this.Target.TakeDamage(this.Caster, Effect, 0);
                break;
        }

        Debug.Log(this.Target.CreatureName + " AFTER: ");
        Debug.Log("Bad Health: " + this.Target.Stats.StatList[(int)StatType.HEALTH].Stat.ToString());
        Debug.Log("Bad Magic: " + this.Target.Stats.StatList[(int)StatType.MAGIC].Stat.ToString());
    }
}
