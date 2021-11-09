using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using System.IO;
using System;


[System.AttributeUsage(System.AttributeTargets.All, Inherited = true, AllowMultiple = true), Serializable]
public class Asset : PropertyAttribute
{
    //public string guidString;
    public Guid guid;

    public string prefabPath;

    public Asset()
    {
        guid = new Guid();
        guid = Guid.NewGuid();
    }

    public virtual Asset CreateAsset()
    {
        return null;
    }

    public virtual Asset DestroyAsset()
    {
        return null;
    }
}

/*****************************************************************************************
* Store paths to prefabs as well as other data serialized in json.
* Data such as player or enemy stat data. Loop times and length of clips for audio
* This will also be used for TileMap and scene management?
* TODO: Need to add a hash or ID. Need to add a check for valid path and if asset exists
*****************************************************************************************/
public sealed class GameAssetManager : MonoBehaviour
{
    string folderName = "Resources/";

    // The Json file that EVERYTHING will serialize to
    string filePath;
    string jsonData;
    bool isInit;

    public GameAssetManager()
    {
        if (!isInit)
        {   if (instance == null)
            {
                instance = this;
            }
            if (instance != null && instance != this)
            {
                Destroy(instance);
            }
        }
    }

    private static GameAssetManager instance;

    public static GameAssetManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameAssetManager();
            }
            return instance;
        }
    }

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

    private void Awake()
    {
        data = new Dictionary<string, Asset>();
        tempContainer = new Dictionary<string, Asset>();

        // ToDo. Add Path verify!
        filePath = "Assets/Assets.json";

        // In here or a seperate initalize function to parse all data's!
        if (File.Exists(filePath))
        {
            jsonData = File.ReadAllText(filePath);
            tempContainer = JsonConvert.DeserializeObject<Dictionary<string, Asset>>(jsonData, settings);

            foreach(var item in tempContainer)
            {
                if (item.Value.guid == null)
                {
                    item.Value.guid = System.Guid.NewGuid();
                }

                // Need to reconstruct proper paths in here
               data.Add(item.Key, item.Value.CreateAsset());
            }

            tempContainer.Clear();
            isInit = true;
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
    * Do this first to recreate the inital structure and save item position proper. Then save all items in their proper order
    ***************************************************************************************************************************/
    public int AddAsset(Asset assetData, string key)
    {
        if (File.Exists(filePath) && data == null)
        {
            jsonData = File.ReadAllText(filePath);
            data = JsonConvert.DeserializeObject<Dictionary<string, Asset>>(jsonData, settings);

            // For future GameObject ref serialization as well as general asset database use.
            foreach (var asset in data)
            {
                if (asset.Value.guid == null)
                {
                    asset.Value.guid = System.Guid.NewGuid();
                }
            }

            assetData.guid = System.Guid.NewGuid();
            data.Add(key, assetData);
        }

        else if (!File.Exists(filePath) && data == null)
        {
            data = new Dictionary<string, Asset>();
            assetData.guid = System.Guid.NewGuid();
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

    public Asset Get(string key)
    {
        // Need a way to grab enmasse
        foreach (var asset in data)
        {
            if (asset.Equals(key))
            {
                return asset.Value;
            }
        }

        return default(Asset);
    }

    public bool isFilled()
    {
        if (data == null || data.Count <= 0)
        {
            return false;
        }
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    ~GameAssetManager()
    {
        foreach(var asset in data)
        {
            asset.Value.DestroyAsset();
            data.Remove(asset.Key);
        }
    }

}
