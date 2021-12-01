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
