using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum StatType { HEALTH, SPEED, STRENGTH, MAGIC, DEFENSE }

public class Stats
{
    int ID;
    private StatType _CurrentStat;
    private float _Stat;

    public float Stat
    {
        get
        {
            return _Stat;
        }

        set
        {
            _Stat = value;
        }
    }

    public StatType CurrentStat
    {
        get
        {
            return _CurrentStat;
        }

        set
        {
            _CurrentStat = value;
        }
    }

    public void CreateStats(StatType Stat, float Stats)
    {
        CurrentStat = Stat;
        _Stat = Stats;
    }

    public override string ToString() 
    {
        // Return name and Value
        return CurrentStat.ToString() + ": " +  Stat.ToString();
    }
}

public class StatManager
{

    private List<Stats> _Stats;
    private Stats StatToCreate;
    [System.NonSerialized]
    Dictionary<BufferEffect, Buffers> AppliedBuffer; // perma lasting effects

    Buffers OneTimeEffect;

    float _Buff;

    public List<Stats> StatList
    {
        get
        {
            return _Stats;
        }

        set
        {
            _Stats = value;
        }
    }

    public void Initalize()
    {
        _Stats = new List<Stats>();
        AppliedBuffer = new Dictionary<BufferEffect, Buffers>();
        for (int i = 0; i < 5; i++)
        {
            StatToCreate = new Stats();
            StatToCreate.CreateStats((StatType)i, _Buff);

            _Stats.Add(StatToCreate);
        }
    }

    public void AddBuffer(BufferEffect Effect, Buffers BuffToApply)
    {
        AppliedBuffer.Add(Effect, BuffToApply);
    }


}
