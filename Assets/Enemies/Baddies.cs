﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
#endif
using System.IO;
using System;

[System.Serializable]
public class Baddies : ScriptableObject, IAsset
{
    public int id;
    public int level;

    [System.NonSerialized]
    public Gauge gauge;

    // I only want the saved path not the object itself
    [System.NonSerialized]
    public GameObject prefab;
    private Creature data;
    public string prefabPath;
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

    public IAsset CreateAsset()
    {
        var bInst = Resources.Load(prefabPath, typeof(GameObject)) as GameObject;
        if (!prefab)
        {
            prefab = Instantiate(bInst);
            prefab.SetActive(false);
        }
        return this;
    }

    public Baddies createBattler(BattleSlots slot)
    {
        prefab = MonoBehaviour.Instantiate(prefab);
        MonoBehaviour.DontDestroyOnLoad(prefab);
        gauge = prefab.GetComponent<Gauge>();
        prefab.tag = "Enemy";
        //tag = data.BattleTag.ENEMY;
        data.createWeaponSlots();
        return this;
    }

    public bool checkHealth()
    {
        if (data.Stats.statList[(int)StatType.HEALTH].stat <= 0)
        {
            data.Die();
            return false;
        }

        else
        {
            return true;
        }
    }
}


