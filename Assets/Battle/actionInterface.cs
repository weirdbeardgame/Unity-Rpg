using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ActionIface
{
    void Enqueue(Creature caster, Creature target);
    void Execute();
    float GetWeight();
    float GetBuff();
    Creature GetCaster();
    Creature GetTarget();
}