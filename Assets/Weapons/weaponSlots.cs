using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponSlots : ScriptableObject
{
    bool IsEquiped;
    JobSystem Jobs;
    Appendage Appendage;
    public WeaponData Weapon;

    public weaponSlots()
    {
        Appendage = 0;
        Jobs = 0;
        Weapon = null;
        IsEquiped = false;
    }

    public weaponSlots(Appendage ap, JobSystem job)
    {
        Jobs = job;
        Appendage = ap;
        IsEquiped = false;
        Weapon = new WeaponData(); // to initalize the slot itself
    }

    public void insert(WeaponData Weap)
    {
        if (Weap.Appendage == Appendage && Weap.Job == Jobs)
        {
            Weapon = Weap;
            IsEquiped = true;
        }
    }

    public void remove()
    {
        Weapon = null;
        IsEquiped = false;
    }

}
