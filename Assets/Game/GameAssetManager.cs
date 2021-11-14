using System.Text.RegularExpressions;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using System.Linq;
using System.IO;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif


public abstract class Asset : ISerializationCallbackReceiver
{
    // I don't know why this serializes correctly. But only sometimes?
    // Like seriously. This is pure magic as far as Newtonsoft is concerned it seems
    public Guid assetGuid;
    public string path;

    // Though this will change depening on type.
    [System.NonSerialized]
    public UnityEngine.Object toSerialize;

    long refID;
    public Asset()
    {
        assetGuid = new Guid();
        assetGuid = Guid.NewGuid();
    }

    // If it aint obvious. This shit should only happen when serializing in Editor my guy
    #if UNITY_EDITOR
    static public string GetAssetPath(UnityEngine.Object toSerialize)
    {
        string assetPath = string.Empty;
        string finalString = string.Empty;
        string [] pathToken;

        assetPath = AssetDatabase.GetAssetPath(toSerialize);
        if (assetPath != string.Empty)
        {
            // Check and tokenize string in here to make sure it's a valid path.
            pathToken = assetPath.Split('/').Select(x => x.Trim()).ToArray();
            finalString = string.Join("/", pathToken, 0, pathToken.Count() - 1);
        }
        return finalString;
    }

    // Similar to Create Asset but a different point to it.
    public void OnBeforeSerialize()
    {
        if (assetGuid == null || assetGuid == Guid.Empty)
        {
            assetGuid = Guid.NewGuid();
        }

        // I wonder if I should assume this belongs in resources somehow
        path = GetAssetPath(toSerialize);

        if (assetGuid == Guid.Empty)
        {
            assetGuid = Guid.NewGuid();
        }
    }

    public void OnAfterDeserialize()
    {
        // If it's an instantiated object in the scene. We don't want nonna that!
        // MonoBehaviour.Destroy(toSerialize);
    }
    #endif

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

    private static GameAssetManager instance;

    public static GameAssetManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameAssetManager();
                instance.Init();
            }
            return instance;
        }
    }

    // Is this the best way to store this data? Each system will call for data from manager
    [SerializeReference]
    private Dictionary<string, Asset> data;
    private Dictionary<string, Asset> tempContainer;
    public Dictionary<string, Asset> Data
    {
        get
        {
            return data;
        }
    }

    private void Init()
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
                if (item.Value.assetGuid == null)
                {
                    item.Value.assetGuid = System.Guid.NewGuid();
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
                if (asset.Value.assetGuid == null)
                {
                    asset.Value.assetGuid = System.Guid.NewGuid();
                }
                if (asset.Value.path == string.Empty)
                {
                    Asset.GetAssetPath(asset.Value.toSerialize);
                }
            }

            assetData.assetGuid = System.Guid.NewGuid();
            data.Add(key, assetData);
        }

        else if (!File.Exists(filePath) && data == null)
        {
            data = new Dictionary<string, Asset>();
            assetData.assetGuid = System.Guid.NewGuid();
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
