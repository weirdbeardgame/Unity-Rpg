using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionIface : ScriptableObject
{
    public virtual void Enqueue(Creature caster, Creature target)
    {

    }
    public virtual void Execute()
    {

    }

    public Creature target;

    public Creature caster
    {
        get
        {
            return caster;
        }

        protected set
        {
            caster = value;
        }

    }
    public Buffers buffer
    {
        get
        {
            return buffer;
        }
        set
        {
            buffer = value;
        }
    }
    public float weight
    {
        get
        {
            return weight;
        }
        protected set
        {
            weight = value;
        }
    }

    }