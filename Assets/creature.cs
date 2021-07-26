using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { COMMAND, SELECTION, ACTION, WAIT };
public enum JobSystem { MAGE, WARRIOR, FIGHTER, DRAGOON, THIEF, SAMURAI }; // Does Musungo fit in the warrior box?

public enum BattleTag { PLAYER, ENEMY }

public enum Appendage { LLEG, RLEG, LHAND, RHAND, HEAD };

public class Creature : senderInterface
{
    public StatManager Stats;
    private weaponSlots[] slots;

    BattleTag _Tag;

    public GameObject Battler;

    JobSystem _Job;

    public BattleState state
    {
        get
        {
            return state;
        }
        private set
        {
            state = value;
        }
    }

    float Buff;

    Buffers OneTime;
    public float actualDamage
    {
        get
        {
            return actualDamage;
        }
        private set
        {
            actualDamage = value;
        }
    }

    public BattleTag Tag
    {
        get
        {
            return _Tag;
        }

        set
        {
            _Tag = value;
        }
    }

    public JobSystem Job
    {
        get
        {
            return _Job;
        }

        set
        {
            _Job = value;
        }
    }

    public string CreatureName;

    public void createWeaponSlots()
    {
        slots = new weaponSlots[5];

        for (int i = 0; i < 5; i++)
        {
            slots[i] = new weaponSlots((Appendage)i, _Job);
        }
    }

    // We need to know strength of attack, of Enemy of defense and if it has any special buffs.
    public float TakeDamage(Creature Attacker, float BaseDMG, Appendage WeaponSlot)
    {
        OneTime = new Buffers();
        Buff = Attacker.Stats.StatList[(int)StatType.STRENGTH].Stat + BaseDMG; // Note that base damage is basic attack skills damage
        actualDamage = Buff - Stats.StatList[(int)StatType.DEFENSE].Stat;
        OneTime = OneTime.CreateBuffer(-actualDamage, TimeAmount.ONCE, BufferEffect.NORMAL, BuffType.FLAT);
        OneTime.ApplyBuffer(Stats, (int)StatType.HEALTH);
        return actualDamage;
    }

    public void ApplyBuffs(Buffers Buff)
    {

    }

    public void ApplyDebuffs(Buffers Buff)
    {

    }
    public weaponSlots GetSlot(Appendage app)
    {
        return slots[(int)app];
    }

    public void send(object message)
    {
        
    }

    public Creature getCreature()
    {
        return this;
    }
}
