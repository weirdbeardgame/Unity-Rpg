using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
#endif
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(PlayerMovement))]

// Either this hold an instance of Creature data and GameObject. Or I use Creature as a common type and these hold specific functions?
public class Player : Asset
{
    public int level;

    /*********************************************************************************
    * This isn't seralizable in Json because unity won't allow it!
    * We need to figure out a logic that either reads from folder or
    * We need to figure a different way to store this data. Perhaps a custom format?
    * Even better idea. LET THE ASSET MANAGER DO THIS!!!
    * We're just returning a cast of the toSerialize object
    ***********************************************************************************/
    [JsonIgnore]
    public GameObject prefab
    {
        get
        {
            return (GameObject)toSerialize;
        }
        #if UNITY_EDITOR
        set
        {
            toSerialize = value;
        }
        #else
        private set
        {
            toSerialize = value;
        }
        #endif

    }
    private Creature data;

    public Creature Data
    {
        get
        {
            return data;
        }
        #if UNITY_EDITOR
        set
        {
            data = value;
        }

        #else
        private set
        {
            data = value;
        }
        #endif
    }

    public override Asset CreateAsset()
    {
       var bInst = Resources.Load(path, typeof(GameObject)) as GameObject;
       if (!prefab)
       {
            prefab = MonoBehaviour.Instantiate(bInst);
            prefab.SetActive(false);
       }
        return this;
    }

    public override Asset DestroyAsset()
    {
        MonoBehaviour.Destroy(prefab);
        data = null;
        return null;
    }

    public void Kill()
    {
        // A later to be implemented death function. I'm still betting on an afterlife mechanic.
    }

    public void LevelUp()
    {
        // Increase level Int. Increase Stats 
    }

    public void Equip(Appendage append, Weapon WeaponToEquip)
    {
        WeaponToEquip.Equip((int)append, data);
    }

    public void Dequip(Appendage Slot, WeaponData WeaponToRemove)
    {
        WeaponToRemove.Dequip(data.slots[(int)Slot]);
    }

    void ApplyWeaponBuffs()
    {
        // Things like. Str +5 or Spd +2 or whatever.
    }

}
