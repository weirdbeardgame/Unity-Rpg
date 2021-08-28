using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using System.IO;
using System;

public enum AssetType {ENEMY, PLAYER, ITEM, WEAPON, AUDIO, SPRITE}

// Seems a bit overkill?
public struct Asset
{
    public AssetType indexedType;
    public object Data;
    int id;

    public Asset(object asset, AssetType type)
    {
        id = 0;
        Data = asset;
        indexedType = type;
    }

    public void CreateAsset(object asset, AssetType type)
    {
        Data = asset;
        indexedType = type;
        id += 1;
    }
}

/*********************************************************************************
* Store paths to prefabs as well as other data serialized in json.
* Data such as player or enemy stat data. Loop times and length of clips for audio
* This will also be used for TileMap and scene management?
**********************************************************************************/
//[CreateAssetMenu(menuName = "Assets")]
public sealed class GameAssetManager : MonoBehaviour
{
    // The overall path that EVERYTHING will serialize to
    string filePath = "Assets/Assets.json";
    string jsonData;
    int itemID;
    string key;

    // Is this the best way to store this data? Each system will call for data from manager
    [SerializeField]
    private Dictionary<string, Asset> data;
    private Dictionary<string, Asset> tempContainer;

    public Dictionary<string, Asset> Data
    {
        get
        {
            return data;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public void Init()
    {
        data = new Dictionary<string, Asset>();
        tempContainer = new Dictionary<string, Asset>();
        // In here or a seperate initalize function to parse all data's!
        if (File.Exists(filePath))
        {
            jsonData = File.ReadAllText(filePath);
            tempContainer = JsonConvert.DeserializeObject<Dictionary<string, Asset>>(jsonData, settings);
            foreach(var item in tempContainer)
            {
                switch (item.Value.indexedType)
                {
                    case AssetType.ENEMY:
                        Asset temp = item.Value;
                        Baddies bad = (Baddies)temp.Data;
                        GameObject bInst = Resources.Load(bad.prefabPath, typeof(GameObject)) as GameObject;
                        bad.Prefab = Instantiate(bInst);
                        temp.Data = bad;
                        data.Add(item.Key, temp);
                        break;
                    case AssetType.PLAYER:
                        Asset pTemp = item.Value;
                        Player play = (Player)pTemp.Data;
                        GameObject pInst = Resources.Load(play.prefabPath, typeof(GameObject)) as GameObject;
                        play.prefab = Instantiate(pInst);
                        pTemp.Data = play;
                        data.Add(item.Key, pTemp);
                        break;
                }
            }
            tempContainer.Clear();
        }
    }

    static JsonSerializerSettings settings = new JsonSerializerSettings
    {
        TypeNameHandling = TypeNameHandling.All,
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
    };

    /**************************************************************************************************************************
    * This has several jobs or tasks to accomplish.
    * A. It needs to have a method to sort data into one of the lists and store it.
    * B. It needs to be able to extract data from assetData and serialize it.
    * C. It needs to be able to deserialize and store data listed in Json and be able to hand it to requesters
    ***************************************************************************************************************************/
    public int AddAsset(Asset assetData, string key)
    {
        // Do this first to recreate the inital structure and save item position proper
        // Then save all items in their proper order

        if (File.Exists(filePath) && data == null)
        {
            jsonData = File.ReadAllText(filePath);
            data = JsonConvert.DeserializeObject<Dictionary<string, Asset>>(jsonData, settings);
        }

        else if (!File.Exists(filePath) && data == null)
        {
            data = new Dictionary<string, Asset>();
            data.Add(key, assetData);
        }
        else if (!data.ContainsKey(key))
        {
            data.Add(key, assetData);
        }
        Debug.Log(data.ToString());
        string serialize = JsonConvert.SerializeObject(data, settings);
        File.WriteAllText(filePath, serialize);
        return 0;
    }

    void removeAsset(string key)
    {
        data.Remove(key);
    }

    public object Get(string key, AssetType type)
    {
        // Need a way to grab enmasse
        foreach (var asset in data)
        {
            if (asset.Equals(key))
            {
                return asset;
            }
        }
        return default(Asset);
    }

    public int isFilled()
    {
        if (data == null)
        {
            Init();
        }
        return data.Count;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
