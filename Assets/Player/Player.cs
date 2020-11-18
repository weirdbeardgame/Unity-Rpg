using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Creature
{
    public string SpritePath;
    public int Level;

    public void Kill()
    {
        // A later to be implemented Death function
    }

    public void LevelUp()
    {
        // Increase level Int. Increase Stats 
    }

    public void Equip(Appendage Index, Weapon WeaponToEquip)
    {
        WeaponToEquip.Equip((int)Index, this);
    }

    public void Dequip(Appendage Slot, WeaponData WeaponToRemove)
    {
        WeaponToRemove.Dequip(Slots[(int)Slot]);
    }

    void ApplyWeaponBuffs()
    {

    }

}
