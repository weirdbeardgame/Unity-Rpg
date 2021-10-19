using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScreen : MonoBehaviour
{
    Inventory inventory;
    public GameObject itemInit;
    List<GameObject> listOfItem;
    // Awake is called before the first frame update

    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
        foreach(var item in inventory.ItemList)
        {
            itemInit = Instantiate(item.Value.prefab);
            listOfItem.Add(itemInit);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
