using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TimeAmount { TIMED, ONCE, PERMA }
public enum BufferEffect { NORMAL, BURN, POISON, ABSORB, FROST } // Not sure if ABSORB should be a buffer or it's own thing
public enum BuffType { FLAT, PERCENTAGE }

public class Buffers : ScriptableObject
{
    public float buff;
    public float effectTimer;

    [System.NonSerialized]
    float timeRemaining;
    public BufferEffect effect;
    public TimeAmount timer;
    public BuffType type;
    bool IsActive = false;

    public void AddBuffer(Creature Target)
    {
        timeRemaining = new float();
        timeRemaining = effectTimer;
        //Target.Stats.AddBuffer(this);
    }

    public Buffers CreateBuffer(float Buffer, TimeAmount Time, BufferEffect Eff, BuffType T)
    {
        this.buff = Buffer;
        this.timer = Time;
        this.effect = Eff;
        this.type = T;

        return this;
    }

    public float ApplyOnce() // Item, Basic Skill, Attack
    {
        return buff;
    }

    public float ApplyPermaBuffers() // Weapon, Absorbtion
    {
        if (!IsActive)
            IsActive = true;

        return buff;
    }

    public float ApplyTimedBuffer() // Temp Lasting effects Burn, Frost etc. Though those could have a condition rather then a timer.
    {
        // If timer == isActive apply buffer to stats!
        if (effectTimer > 0)
        {
            effectTimer -= 1;
            return buff;
        }

        else
        {
            return 0;
        }
    }

    public void ApplyBuffer(StatManager Stat, int Stat1, int Stat2 = 0, int Stat3 = 0) // Should I assume this is damage?
    {
        switch (type)
        {
            case BuffType.FLAT:

                switch (timer)
                {
                    case TimeAmount.TIMED:
                        Stat.statList[Stat1].stat += ApplyTimedBuffer();

                        if (Stat2 > 0)
                        {
                            Stat.statList[Stat2].stat += ApplyTimedBuffer();
                        }

                        if (Stat3 > 0)
                        {
                            Stat.statList[Stat3].stat += ApplyTimedBuffer();
                        }
                        break;

                    case TimeAmount.ONCE:
                        Stat.statList[Stat1].stat += ApplyOnce();

                        if (Stat2 > 0)
                        {
                            Stat.statList[Stat2].stat += ApplyOnce();
                        }

                        if (Stat3 > 0)
                        {
                            Stat.statList[Stat3].stat += ApplyOnce();
                        }
                        break;

                    case TimeAmount.PERMA:
                        Stat.statList[Stat1].stat += ApplyPermaBuffers();

                        if (Stat2 > 0)
                        {
                            Stat.statList[Stat2].stat += ApplyPermaBuffers();
                        }

                        if (Stat3 > 0)
                        {
                            Stat.statList[Stat3].stat += ApplyPermaBuffers();
                        }
                        break;
                }
                break;
        }
    }

}
