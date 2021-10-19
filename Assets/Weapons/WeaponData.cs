using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponStats
{
    public float HpBuf;
    public float MagBuf;
    public float StrBuf;
    public float SpdBuf;
    public float SpclBuf;
}

public class WeaponData
{
    private string weaponName;
    private string description;
    string iconPath;

    public Buffers Buffer;

    private JobSystem job;
    private Appendage appendage;

    Inventory inv;

    public int WeaponID;

    public List<SkillData> Skills; // the all important skill system, note weapons can contain multiple skills.
    bool isEquipped;
    GameObject icon;


    public string WeaponName
    {
        get
        {
            return weaponName;
        }

        set
        {
            weaponName = value;
        }
    }

    public string Description
    {
        get
        {
            return description;
        }

        set
        {
            description = value;
        }
    }


    public JobSystem Job
    {
        get
        {
            return job;
        }

        set
        {
            job = value;
        }
    }

    public Appendage Appendage
    {
        get
        {
            return appendage;
        }

        set
        {
            appendage = value;
        }
    }

    public void Equip(WeaponData Data, weaponSlots Slot)
    {
        Slot.insert(Data);
    }

    public void Dequip(weaponSlots Slot)
    {
        Slot.remove();
    }

    public void AddToInventory()
    {
        inv = ScriptableObject.FindObjectOfType<Inventory>();

        inv.Add(this);
    }

    public Sprite getSprite()
    {
        return null;
    }

    public int getCost()
    {
        return 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
