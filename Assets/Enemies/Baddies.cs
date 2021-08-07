using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System;

public class Baddies : Creature
{
    public string enemyName
    {
        get
        {
            return enemyName;
        }
        protected set
        {
            enemyName = value;
        }
    }
    string type;
    public string spritePath;
    public int id;

    public int level;

    [System.NonSerialized]
    public Gauge gauge;

    [System.NonSerialized]
    bool isBattle;

    public Baddies createBattler(BattleSlots slot)
    {
        BattlePrefab = new GameObject(enemyName);

        BattlePrefab.AddComponent<SpriteRenderer>();
        BattlePrefab.AddComponent<BoxCollider2D>();
        BattlePrefab.AddComponent<RectTransform>();
        BattlePrefab.AddComponent<Rigidbody2D>();
        gauge = BattlePrefab.AddComponent<Gauge>();

        BattlePrefab.GetComponent<Rigidbody2D>().gravityScale = 0;
        BattlePrefab.GetComponent<RectTransform>().sizeDelta = new Vector2(5, 10);
        BattlePrefab.GetComponent<RectTransform>().position = new Vector2(slot.gameObject.transform.localPosition.x, slot.gameObject.transform.localPosition.y);
        BattlePrefab.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(spritePath);
        BattlePrefab.GetComponent<SpriteRenderer>().sortingOrder = 1;
        BattlePrefab.tag = "Enemy";

        tag = BattleTag.ENEMY;

        MonoBehaviour.DontDestroyOnLoad(BattlePrefab);

        createWeaponSlots();
        //setJob((jobSystem)Enum.Parse(typeof(jobSystem), token.Value<string>("job")));
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



