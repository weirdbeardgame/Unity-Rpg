using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum StatType { HEALTH, SPEED, STRENGTH, MAGIC, DEFENSE }

public class Stats
{
    int ID;
    public StatType currentStat;
    public float stat;

    public void CreateStats(StatType Stat, float Stats)
    {
        currentStat = Stat;
        stat = Stats;
    }

    public override string ToString() 
    {
        // Return name and Value
        return currentStat.ToString() + ": " +  stat.ToString();
    }
}

public class StatManager
{
    public List<Stats> statList;
    private Stats statToCreate;
    [System.NonSerialized]
    Dictionary<BufferEffect, Buffers> appliedBuffer; // perma lasting effects Weapons equipped or any perma spell effects

    Buffers OneTimeEffect;

    float buff;

    public void Initalize()
    {
        statList = new List<Stats>();
        appliedBuffer = new Dictionary<BufferEffect, Buffers>();
        for (int i = 0; i < 5; i++)
        {
            statToCreate = new Stats();
            statToCreate.CreateStats((StatType)i, buff);
            statList.Add(statToCreate);
        }
    }

    public void AddBuffer(BufferEffect Effect, Buffers BuffToApply)
    {
        appliedBuffer.Add(Effect, BuffToApply);
    }
}
