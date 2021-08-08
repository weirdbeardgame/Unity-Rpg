using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System;

[System.Serializable]
public class Baddies : Creature
{
    string type;
    public string spritePath;
    public int id;
    public int level;
    [System.NonSerialized]
    public Gauge gauge;

    public Baddies createBattler(BattleSlots slot)
    {
        BattlePrefab = MonoBehaviour.Instantiate(BattlePrefab);
        MonoBehaviour.DontDestroyOnLoad(BattlePrefab);
        gauge = BattlePrefab.GetComponent<Gauge>();
        BattlePrefab.tag = "Enemy";
        tag = BattleTag.ENEMY;
        createWeaponSlots();
        return this;
    }

    public bool checkHealth()
    {
        if (Stats.statList[(int)StatType.HEALTH].stat <= 0)
        {
            Die();
            return false;
        }

        else
        {
            return true;
        }
    }
}



