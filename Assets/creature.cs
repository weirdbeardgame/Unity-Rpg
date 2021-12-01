using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is programed to be ATB style ala ff vi - ff ix
public enum JobSystem { MAGE, WARRIOR, FIGHTER, DRAGOON, THIEF, SAMURAI }; // Does Musungo fit in the warrior box?

public class Creature
{
    public bool isAlive;
    public StatManager Stats;
    public List<weaponSlots>slots;
    public BattleTag tag;

    public JobSystem job;

    [System.NonSerialized]
    public BattleState state;

    float Buff;

    Buffers OneTime;

    [System.NonSerialized]
    public float actualDamage;

    public string creatureName;
    public string description;

    public GameObject prefab;

    public void createWeaponSlots()
    {
        slots = new List<weaponSlots>();
        for (int i = 0; i < 5; i++)
        {
            slots.Add(new weaponSlots((Appendage)i, job));
        }
    }

    // We need to know strength of attack, of Enemy of defense and if it has any special buffs.
    public float TakeDamage(Creature Attacker, float BaseDMG, Appendage WeaponSlot)
    {
        OneTime = new Buffers();
        Buff = Attacker.Stats.statList[(int)StatType.STRENGTH].stat + BaseDMG; // Note that base damage is basic attack skills damage
        actualDamage = Buff - Stats.statList[(int)StatType.DEFENSE].stat;
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

    public void Die()
    {
        slots = null;
        isAlive = false;
    }
    public weaponSlots GetSlot(Appendage app)
    {
        return slots[(int)app];
    }

    public Creature getCreature()
    {
        return this;
    }
}
