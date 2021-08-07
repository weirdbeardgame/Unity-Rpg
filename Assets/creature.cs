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
    public bool isAlive;
    public StatManager Stats;
    public weaponSlots[] slots
    {
        get
        {
            return slots;
        }
        private set
        {
            slots = value;
        }
    }

    public BattleTag tag;
    // ToDo. Figure out best method for prefab serialization
    public GameObject BattlePrefab;
    public JobSystem job;

    public BattleState state
    {
        get
        {
            return state;
        }
        protected set
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
        protected set
        {
            actualDamage = value;
        }
    }

    public string creatureName;

    public void createWeaponSlots()
    {
        Debug.Log("Attempting to create Slots");
        slots = new weaponSlots[5]; // Need to figure out why this is throwing an exception. Oddly nothing on the surface appears "null"
        Debug.Log("SLOTS: " + slots);

        for (int i = 0; i < 5; i++)
        {
            slots[i] = new weaponSlots((Appendage)i, job);
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
        // There's no more health. Be gone... THOT!
        MonoBehaviour.Destroy(BattlePrefab);
        slots = null; // Doth we needeth this?
        isAlive = false;
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
