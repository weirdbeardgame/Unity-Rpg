using System.Collections;
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

public class Baddies : Asset
{
    public int id;
    public int level;

    [System.NonSerialized]
    public Gauge gauge;

    // I only want the saved path not the object itself
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
        data = null;
        MonoBehaviour.Destroy(prefab);
        return null;
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


