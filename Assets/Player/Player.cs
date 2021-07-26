using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(PlayerMovement))]
public class Player : Creature
{
    public string SpritePath;
    public int level;
    public void Kill()
    {
        // A later to be implemented Death function. I'm still betting on an afterlife mechanic.
    }

    public void LevelUp()
    {
        // Increase level Int. Increase Stats 
    }

    public void Equip(Appendage append, Weapon WeaponToEquip)
    {
        WeaponToEquip.Equip((int)append, this);
    }

    public void Dequip(Appendage Slot, WeaponData WeaponToRemove)
    {
        WeaponToRemove.Dequip(slots[(int)Slot]);
    }

    void ApplyWeaponBuffs()
    {
        // Things like. Str +5 or Spd +2 or whatever.
    }

}
