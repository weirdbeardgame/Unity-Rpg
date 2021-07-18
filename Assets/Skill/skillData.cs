using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public enum AttributeType { PHYSICAL, MAGIC };
public enum SkillTypes { HEALING, DAMAGING };

public class SkillData : ActionIface
{
    public SkillTypes skillType;
    public AttributeType attribute;
    private string skillName;
    private int skillID = 0;
    private float effect;
    public StatType affectedStat;
    public int TargetAmount;

    [System.NonSerialized]
    public int timer;

    [System.NonSerialized]
    CommandQueue Queue;

    private float actionWeight;

    public float GetWeight()
    {
        return actionWeight;
    }

    public float GetBuff()
    {
        return effect;
    }

    public float Effect
    {

        get
        {
            return effect;
        }

        set
        {
            effect = value;
        }
    }

    public int SkillID
    {
        get
        {
            return skillID;
        }

        set
        {
            skillID = value;
        }
    }

    public string SkillName
    {
        get
        {
            return skillName;
        }

        set
        {
            skillName = value;
        }
    }

    public override void Enqueue(Creature c, Creature t)
    {
        Queue = ScriptableObject.FindObjectOfType<CommandQueue>();

        caster = c;
        target = t;

        //This is to determine location of Action in queue not the time to action but I guess it could be that?
        //ActionWeight = C.Stats.Speed * C.Stats.Agility / MaxTicks;

        if (Queue.Commands.Count > 0)
        {
            for (int i = 1; i < Queue.Commands.Count; i++)
            {
                if (actionWeight > Queue.Commands[i].weight)
                {
                    ActionIface temp = Queue.Commands[i];
                    Queue.enqueue(this, false);
                    Queue.enqueue(temp, true);
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
            Queue.enqueue(this);
        }
    }

    /*public override void Execute() // Because Caster will be important for the absorption system
    {
        Debug.Log(this.target.CreatureName + " BEFORE: ");
        Debug.Log("Bad Health: " + this.target.Stats.StatList[(int)StatType.HEALTH].Stat.ToString());
        Debug.Log("Bad Magic: " + this.target.Stats.StatList[(int)StatType.MAGIC].Stat.ToString());

        switch (skillType)
        {
            case SkillTypes.HEALING:
                this.target.Stats.StatList[(int)affectedStat].Stat += effect;
                break;

            case SkillTypes.DAMAGING:
                this.target.TakeDamage(this.caster, effect, 0);
                break;
        }

        Debug.Log(this.target.CreatureName + " AFTER: ");
        Debug.Log("Bad Health: " + this.target.Stats.StatList[(int)StatType.HEALTH].Stat.ToString());
        Debug.Log("Bad Magic: " + this.target.Stats.StatList[(int)StatType.MAGIC].Stat.ToString());
    }*/ // The Menu's should never preform an action in the battle system itself! Let the skill system or the battle system itself handle these interactions!
}
