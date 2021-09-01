using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Dictionary<int, ItemData> itemList;
    private Dictionary<int, WeaponData> equipables;

    int maxSize = 99;
    private int currentSize = 0;

    public int CurrentSize
    {
        get
        {
            return currentSize;
        }
    }

    public Dictionary<int, ItemData> ItemList
    {
        get
        {
            return itemList;
        }
    }

    public Dictionary<int, WeaponData> Equipables
    {
        get
        {
            return equipables;
        }
    }

    InventoryMessage currentMessage;

    // Start is called before the first frame update
    void Start()
    {
        itemList = new Dictionary<int, ItemData>(); // to initalize the structure
        equipables = new Dictionary<int, WeaponData>();
    }

    public void Add(ItemData I)
    {
        currentMessage = new InventoryMessage();
        currentMessage.construct(I, itemState.RECIEVED);

        if (currentSize < maxSize)
        {
            itemList.Add(currentSize, I);
            currentSize++;
        }

        else
        {
            Debug.Log("Inventory FULL!");
        }
    }

    public ItemData Remove(ItemData Item)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i] == Item)
            {
                itemList.Remove(i);
            }
        }
        return Item;
    }

    public void Add(WeaponData I)
    {

        if (equipables == null)
        {
            equipables = new Dictionary<int, WeaponData>();
        }

        if (currentSize < maxSize)
        {
            equipables.Add(currentSize, I);
            currentSize++;
        }

        else
        {
            Debug.Log("Inventory FULL!");
        }
    }

    public WeaponData getEquipables(int i)
    {
        return equipables[i];
    }

    // Update is called once per frame
    void Update()
    {

    }
}
