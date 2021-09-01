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
    GameAssetManager manager;
    public Dictionary<int, ItemData> Items;

    void Start()
    {
        manager = GameAssetManager.Instance;
        if (manager.isFilled() > 0)
        {
            foreach(var asset in manager.Data)
            {
                if (asset.Value.indexedType == AssetType.ITEM)
                {
                    ItemData temp = (ItemData)asset.Value.Data;
                    items.Add(temp.itemID, temp);
                }
            }
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
