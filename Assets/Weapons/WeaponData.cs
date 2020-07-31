using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using menu;

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
    private string _WeaponName;
    private string _Description;
    string iconPath;

    public Buffers Buffer;

    private JobSystem _Job;
    private Appendage _Appendage;

    Inventory inv;

    public int WeaponID;

    public List<SkillData> Skills; // the all important skill system, note weapons can contain multiple skills.
    bool isEquipped;
    GameObject icon;


    public string WeaponName
    {
        get
        {
            return _WeaponName;
        }

        set
        {
            _WeaponName = value;
        }
    }

    public string Description
    {
        get
        {
            return _Description;
        }

        set
        {
            _Description = value;
        }
    }


    public JobSystem Job
    {
        get
        {
            return _Job;
        }

        set
        {
            _Job = value;
        }
    }

    public Appendage Appendage
    {
        get
        {
            return _Appendage;
        }

        set
        {
            _Appendage = value;
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
