using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using System.IO;
using System;


// TODO Add custom asset manager code to handle prefab and scriptable object creature data
public class Enemies : MonoBehaviour
{
    GameAssetManager manager;
    public Dictionary<int, Baddies> enemyData;

    // Start is called before the first frame update
    void Start()
    {
        enemyData = new Dictionary<int, Baddies>();
        manager = GetComponent<GameAssetManager>();
        if (manager.isFilled() > 0)
        {
            foreach(var asset in manager.Data)
            {
                if (asset.Value.indexedType == AssetType.ENEMY)
                {
                    Baddies bTemp = (Baddies)asset.Value.Data;
                    // Instantiate Prefab from path and then add the entire baddie to Dictionary
                    // Resources.Load is inefficent enough it shouldn't be used except at start
                    bTemp.Prefab = Resources.Load(bTemp.prefabPath) as GameObject;
                    enemyData.Add(bTemp.id, bTemp);
                }
            }
        }
    }
}
