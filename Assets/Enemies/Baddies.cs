using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System;

public class Baddies : Creature
{

    string enemyName;
    string type;
    public string spritePath;
    int eID;

    int _Level;

    [System.NonSerialized]
    public Gauge gauge;

    [System.NonSerialized]
    bool isBattle;

    public string EnemyName
    {
        get
        {
            return enemyName;
        }

        set
        {
            enemyName = value;
        }
    }

    public string Type
    {
        get
        {
            return type;
        }

        set
        {
            type = value;
        }
    }

    public int ID
    {
        get
        {
            return eID;
        }

        set
        {
            eID = value;
        }
    }

    public int Level
    {

        get
        {
            return _Level;
        }

        set
        {
            _Level = value;
        }

    }

    /*public GameObject Battler
    {
        get
        {
            return battler;
        }
    }*/

    public Baddies createBattler(float x, float y)
    {
        Battler = new GameObject(EnemyName);

        Battler.AddComponent<SpriteRenderer>();
        Battler.AddComponent<BoxCollider2D>();
        Battler.AddComponent<RectTransform>();
        Battler.AddComponent<Rigidbody2D>();
        gauge = Battler.AddComponent<Gauge>();

        Battler.GetComponent<Rigidbody2D>().gravityScale = 0;
        Battler.GetComponent<RectTransform>().sizeDelta = new Vector2(5, 10);
        Battler.GetComponent<RectTransform>().position = new Vector2(x, y);
        Battler.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(spritePath);
        Battler.GetComponent<SpriteRenderer>().sortingOrder = 1;
        Battler.tag = "Enemy";

        tag = BattleTag.ENEMY;

        MonoBehaviour.DontDestroyOnLoad(Battler);

        createWeaponSlots(); // We need a separate constrcut function to give weapons to Baddies?
        //setJob((jobSystem)Enum.Parse(typeof(jobSystem), token.Value<string>("job")));
        return this;
    }

    public bool checkHealth()
    {
        if (Stats.statList[(int)StatType.HEALTH].stat <= 0)
        {
            // Increase Kill Counter
            return false;
        }

        else
        {
            return true;
        }
    }
}



