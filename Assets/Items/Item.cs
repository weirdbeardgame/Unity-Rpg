using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using System.IO;

public class Item : MonoBehaviour
{
    private Dictionary<int, ItemData> items;
    Creature creature;
    Inventory inventory;
    string filePath = "Assets/Items.json";
    public Dictionary<int, ItemData> Items;
    string jsonData;

    void Start()
    {
        if (File.Exists(filePath))
        {
            jsonData = File.ReadAllText(filePath);
            items = JsonConvert.DeserializeObject<Dictionary<int, ItemData>>(jsonData);
        }
        for (int i = 0; i < items.Count; i++)
        {
            //Debug.Log("Item: " + items[i].itemName);
        }
    }

    public void Use(Creature creature, int itemID)
    {
        if (items != null)
        {
            items[itemID].Use(creature);

            Debug.Log("Health" + creature.Stats.statList[(int)StatType.HEALTH].stat.ToString());
        }
    }

    public void Use(Creature creature, ItemData item)
    {
            item.Use(creature); // Then why ItemList
            Debug.Log("Health" + creature.Stats.statList[(int)StatType.HEALTH].stat.ToString());
    }


    public ItemData GetItem(int itemID)
    {
        return items[itemID];
    }

    public void AddToInventory(int itemID)
    {
        inventory = FindObjectOfType<Inventory>();

        if (items[itemID].amount < items[itemID].maxAmount)
        {
            inventory.Add(items[itemID]);
            items[itemID].amount++;
        }
    }

}
