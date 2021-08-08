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

    public Dictionary<int, Baddies> enemyData
    {
        get
        {
            return enemyData;
        }
        private set
        {
            enemyData = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Initalize();
    }

    public Enemies Initalize() // Just incase
    {
        enemyData = new Dictionary<int, Baddies>();
        if (File.Exists(filePath))
        {
            parsedData = File.ReadAllText(filePath);
            enemyData = JsonConvert.DeserializeObject<Dictionary<int, Baddies>>(parsedData);
        }

        return this;
    }
}
