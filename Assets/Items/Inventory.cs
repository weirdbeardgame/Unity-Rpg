using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    private Dictionary<int, ItemData> _ItemList;
    private Dictionary<int, WeaponData> _Equipables;

    int maxSize = 99;
    private int _CurrentSize = 0;

    public int CurrentSize
    {
        get
        {
            return _CurrentSize;
        }
    }

    public Dictionary<int, ItemData> ItemList
    {
        get
        {
            return _ItemList;
        }
    }

    public Dictionary<int, WeaponData> Equipables
    {
        get
        {
            return _Equipables;
        }
    }

    InventoryMessage currentMessage;


    // Start is called before the first frame update
    void Start()
    {
        _ItemList = new Dictionary<int, ItemData>(); // to initalize the structure
        _Equipables = new Dictionary<int, WeaponData>();
    }

    public void Add(ItemData I)
    {
        currentMessage = new InventoryMessage();

        currentMessage.construct(I, itemState.RECIEVED);

        if (_CurrentSize < maxSize)
        {
            _ItemList.Add(_CurrentSize, I);
            _CurrentSize++;
        }

        else
        {
            Debug.Log("Inventory FULL!");
        }
    }

    public void Add(WeaponData I)
    {

        if (_Equipables == null)
        {
            _Equipables = new Dictionary<int, WeaponData>();
        }

        if (_CurrentSize < maxSize)
        {
            _Equipables.Add(_CurrentSize, I);
            _CurrentSize++;
        }

        else
        {
            Debug.Log("Inventory FULL!");
        }
    }

    public WeaponData getEquipables(int i)
    {
        return _Equipables[i];
    }

    // Update is called once per frame
    void Update()
    {

    }
}
