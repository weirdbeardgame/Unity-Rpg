using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using System.IO;
using System;


[System.AttributeUsage(System.AttributeTargets.All, Inherited = true, AllowMultiple = true), Serializable]
public class IAsset : PropertyAttribute
{
    public virtual IAsset CreateAsset()
    {
        return null;
    }
    //IAsset GetAsset();
    public virtual IAsset DestroyAsset()
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
//[CreateAssetMenu(menuName = "Assets")]
public sealed class GameAssetManager : MonoBehaviour
{
    string folderName = "Resources/";

    // The Json file that EVERYTHING will serialize to
    string filePath = Application.dataPath + "/Assets.json";
    string jsonData;
    int itemID;
    string key;
    bool isInit;

    public GameAssetManager()
    {
        if (!isInit)
        {
            Init();
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
    private Dictionary<string, IAsset> data;
    private Dictionary<string, IAsset> tempContainer;
    public Dictionary<string, IAsset> Data
    {
        get
        {
            return data;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!isInit)
        {
            Init();
        }
    }

    public void Init()
    {
        data = new Dictionary<string, IAsset>();
        tempContainer = new Dictionary<string, IAsset>();
        // In here or a seperate initalize function to parse all data's!
        if (File.Exists(filePath))
        {
            jsonData = File.ReadAllText(filePath);
            tempContainer = JsonConvert.DeserializeObject<Dictionary<string, IAsset>>(jsonData, settings);
            foreach(var item in tempContainer)
            {
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
    ***************************************************************************************************************************/
    public int AddAsset(IAsset assetData, string key)
    {
        // Do this first to recreate the inital structure and save item position proper
        // Then save all items in their proper order

        if (File.Exists(filePath) && data == null)
        {
            jsonData = File.ReadAllText(filePath);
            data = JsonConvert.DeserializeObject<Dictionary<string, IAsset>>(jsonData, settings);
            data.Add(key, assetData);
        }

        else if (!File.Exists(filePath) && data == null)
        {
            data = new Dictionary<string, IAsset>();
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

    public IAsset Get(string key)
    {
        // Need a way to grab enmasse
        foreach (var asset in data)
        {
            if (asset.Equals(key))
            {
                return asset.Value;
            }
        }
        return default(IAsset);
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
