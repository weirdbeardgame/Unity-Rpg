using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TimeAmount { TIMED, ONCE, PERMA }
public enum BufferEffect { NORMAL, BURN, POISON, ABSORB, FROST } // Not sure if ABSORB should be a buffer or it's own thing
public enum BuffType { FLAT, PERCENTAGE }

public class Buffers : ScriptableObject
{

    public float Buff;
    public float EffectTimer;

    [System.NonSerialized]
    float TimeRemaining;

    public BufferEffect Effect;
    public TimeAmount Timer;
    public BuffType Type;


    bool IsActive = false;

    public void AddBuffer(Creature Target)
    {
        TimeRemaining = new float();
        TimeRemaining = EffectTimer;
        //Target.Stats.
        //AddBuffer(this);
    }


    public Buffers CreateBuffer(float Buffer, TimeAmount Time, BufferEffect Eff, BuffType T)
    {
        this.Buff = Buffer;
        this.Timer = Time;
        this.Effect = Eff;
        this.Type = T;

        return this;
    }



    public float ApplyOnce() // Item, Basic Skill, Attack
    {
        return Buff;
    }


    public float ApplyPermaBuffers() // Weapon, Absorbtion
    {
        if (!IsActive)
            IsActive = true;

        return Buff;
    }

    public float ApplyTimedBuffer() // Temp Lasting effects Burn, Frost etc. Though those could have a condition rather then a timer.
    {
        // If timer == isActive apply buffer to stats!
        if (EffectTimer > 0)
        {
            EffectTimer -= 1;
            return Buff;
        }

        else
        {
            return 0;
        }
    }

    public void ApplyBuffer(StatManager Stat, int Stat1, int Stat2 = 0, int Stat3 = 0) // Should I assume this is damage?
    {
        switch (Type)
        {
            case BuffType.FLAT:

                switch (Timer)
                {
                    case TimeAmount.TIMED:
                        Stat.StatList[Stat1].Stat += ApplyTimedBuffer();

                        if (Stat2 > 0)
                        {
                            Stat.StatList[Stat2].Stat += ApplyTimedBuffer();
                        }

                        if (Stat3 > 0)
                        {
                            Stat.StatList[Stat3].Stat += ApplyTimedBuffer();
                        }
                        break;

                    case TimeAmount.ONCE:
                        Stat.StatList[Stat1].Stat += ApplyOnce();

                        if (Stat2 > 0)
                        {
                            Stat.StatList[Stat2].Stat += ApplyOnce();
                        }

                        if (Stat3 > 0)
                        {
                            Stat.StatList[Stat3].Stat += ApplyOnce();
                        }
                        break;

                    case TimeAmount.PERMA:
                        Stat.StatList[Stat1].Stat += ApplyPermaBuffers();

                        if (Stat2 > 0)
                        {
                            Stat.StatList[Stat2].Stat += ApplyPermaBuffers();
                        }

                        if (Stat3 > 0)
                        {
                            Stat.StatList[Stat3].Stat += ApplyPermaBuffers();
                        }
                        break;
                }
                break;
        }
    }

}
