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
    public StatType affectedStat;

    private string skillName;

    private float effect;
    private float actionWeight;

    private int skillID = 0;
    public int TargetAmount;

    [System.NonSerialized]
    public int timer;

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

    public override void Execute()
    {
        Debug.Log(target.creatureName + " BEFORE: ");
        Debug.Log("Bad Health: " + target.Stats.statList[(int)StatType.HEALTH].stat.ToString());
        Debug.Log("Bad Magic: " + target.Stats.statList[(int)StatType.MAGIC].stat.ToString());

        // Need to apply use of Buffer system
        switch (skillType)
        {
            case SkillTypes.HEALING:
                target.Stats.statList[(int)affectedStat].stat += effect;
                break;

            case SkillTypes.DAMAGING:
                target.TakeDamage(this.caster, effect, 0);
                break;
        }

        Debug.Log(target.creatureName + " AFTER: ");
        Debug.Log("Bad Health: " + target.Stats.statList[(int)StatType.HEALTH].stat.ToString());
        Debug.Log("Bad Magic: " + target.Stats.statList[(int)StatType.MAGIC].stat.ToString());
    }
}
