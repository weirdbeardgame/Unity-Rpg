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

[System.Serializable]
public class Baddies : ScriptableObject
{
    public int id;
    public int level;

    [System.NonSerialized]
    public Gauge gauge;

    // I only want the saved path not the object itself
    [System.NonSerialized]
    public GameObject Prefab;
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

    public Baddies createBattler(BattleSlots slot)
    {
        Prefab = MonoBehaviour.Instantiate(Prefab);
        MonoBehaviour.DontDestroyOnLoad(Prefab);
        gauge = Prefab.GetComponent<Gauge>();
        Prefab.tag = "Enemy";
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

    public Baddies serialize()
    {
        #if UNITY_EDITOR
        if (Prefab)
        {
            prefabPath = AssetDatabase.GetAssetPath(Prefab);
        }
        return this;
        #endif
        return null;
    }
}

public class BaddiesConverter : JsonConverter
{
    const string ValuePropertyName = "Value";// nameof(LikeType<object>.Value); // in C#6+

    private readonly Type[] types;

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        Baddies temp = (Baddies)value;
        JToken t = JToken.FromObject(temp.serialize());
        serializer.Serialize(writer, t);
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType.IsGenericType && objectType.GetGenericTypeDefinition() == typeof(Baddies);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        // var value = serializer.Deserialize(reader, valueType);
        // I could instantiate gameObject in here

        return null;
    }
}


