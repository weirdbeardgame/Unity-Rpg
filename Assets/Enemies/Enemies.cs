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

    public List<Baddies> enemyData
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
        enemyData = new List<Baddies>();
        if (File.Exists(filePath))
        {
            parsedData = File.ReadAllText(filePath);
            enemyData = JsonConvert.DeserializeObject<List<Baddies>>(parsedData);
        }

        return this;
    }

    public Baddies RandomSelectEnemy()
    {
        // Given an index or scale of enemy based on area. IE. What's allowed to load generate a random selection
        return enemyData[0];
    }

}
