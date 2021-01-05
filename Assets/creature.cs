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
    private weaponSlots[] _Slots;

    BattleTag _Tag;

    public GameObject Battler;

    JobSystem _Job;

    BattleState _State;

    float Buff;

    Buffers OneTime;
    float _ActualDamage = 0;


    public weaponSlots[] Slots
    {
        get
        {
            return _Slots;
        }

        set
        {
            _Slots = value;
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

    public BattleState State
    {
        get
        {
            return _State;
        }

        set
        {
            _State = value;
        }
    }

    public string CreatureName;

    Appendage _Appendage;

    public void createWeaponSlots()
    {
        Slots = new weaponSlots[5];

        for (int i = 0; i < 5; i++)
        {
            Slots[i] = new weaponSlots((Appendage)i, _Job);
        }
    }

    // We need to know strength of attack, of Enemy of defense and if it has any special buffs.
    public float TakeDamage(Creature Attacker, float BaseDMG, Appendage WeaponSlot)
    {
        OneTime = new Buffers();
        Buff = Attacker.Stats.StatList[(int)StatType.STRENGTH].Stat + BaseDMG; // Note that base damage is basic attack skills damage
        _ActualDamage = Buff - Stats.StatList[(int)StatType.DEFENSE].Stat;
        OneTime = OneTime.CreateBuffer(-_ActualDamage, TimeAmount.ONCE, BufferEffect.NORMAL, BuffType.FLAT);
        OneTime.ApplyBuffer(Stats, (int)StatType.HEALTH);
        return _ActualDamage;
    }


    public float ActualDamage
    {
        get
        {
            return _ActualDamage;
        }
    }

    /*public void ApplyBuffs(Buffers Buff)
    {

    }

    public void ApplyDebuffs(Buffers Buff)
    {

    }Potental later design */

    public weaponSlots GetSlot(Appendage app)
    {
        return Slots[(int)app];
    }

    public void send(object message)
    {
        // I could maybe see this being used?
    }

    public Creature getCreature()
    {
        return this;
    }

}
