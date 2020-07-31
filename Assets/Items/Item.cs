using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using System.IO;

public class Item : MonoBehaviour
{
    private Dictionary<int, ItemData> _Items;
    GameObject Items;
    Creature creature;

    Inventory _Inventory;

    string filePath = "Assets/Items.json";
    public Dictionary<int, ItemData> items;
    string jsonData;


    void Start()
    {
        if (File.Exists(filePath))
        {
            jsonData = File.ReadAllText(filePath);
            _Items = JsonConvert.DeserializeObject<Dictionary<int, ItemData>>(jsonData);
        }
        for (int i = 0; i < _Items.Count; i++)
        {
            Debug.Log("Item: " + _Items[i].ItemName);
        }
    }

    public void Initalize()
    {
        if (File.Exists(filePath))
        {
            jsonData = File.ReadAllText(filePath);
            _Items = JsonConvert.DeserializeObject<Dictionary<int, ItemData>>(jsonData);
        }
        for (int i = 0; i < _Items.Count; i++)
        {
            Debug.Log("Item: " + _Items[i].ItemName);
        }
    }


    public string[] GetNames()
    {
        List<string> TempItemNames = new List<string>();
        for (int i = 0; i < _Items.Count; i++)
        {
            TempItemNames.Add(_Items[i].ItemName);
        }

        return TempItemNames.ToArray();
    }



    public void Use(Creature creature, int ItemID)
    {
        if (_Items != null)
        {
            _Items[ItemID].Use(creature);

            Debug.Log("Health" + creature.Stats.StatList[(int)StatType.HEALTH].Stat.ToString());
        }
    }


    public ItemData GetItem(int ItemID)
    {
        return _Items[ItemID];
    }

    public void AddToInventory(int ItemID)
    {
        _Inventory = FindObjectOfType<Inventory>();

        if (_Items[ItemID].Amount < _Items[ItemID].MaxAmount)
        {
            _Inventory.Add(_Items[ItemID]);
            _Items[ItemID].Amount++;
        }
    }

}
