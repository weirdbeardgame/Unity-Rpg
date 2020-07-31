using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using System.IO;
using System;

public class Enemies : MonoBehaviour
{
    [SerializeField]
    string filePath = "Assets/Enemies/Enemies.json";

    string parsedData;

    List<Baddies> _EnemyData;

    int X, Y = 3;

    // Start is called before the first frame update
    void Start()
    {
        _EnemyData = new List<Baddies>();
        if (File.Exists(filePath))
        {
            parsedData = File.ReadAllText(filePath);
            _EnemyData = JsonConvert.DeserializeObject<List<Baddies>>(parsedData);

        }
    }


    public Enemies Initalize() // Just incase
    {
        this._EnemyData = new List<Baddies>();
        if (File.Exists(filePath))
        {
            parsedData = File.ReadAllText(filePath);
            this._EnemyData = JsonConvert.DeserializeObject<List<Baddies>>(parsedData);
        }

        return this;
    }

    public List<Baddies> EnemyData
    {
        get
        {
            return _EnemyData;
        }

    }

}
