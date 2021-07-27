using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

// This is the accesser 

public class Weapon : MonoBehaviour
{

    public Dictionary<int, WeaponData> Weapons;
    string FilePath = "Assets/Weapons/Weapons.json";
    string JsonData;

    // Start is called before the first frame update
    void Start()
    {
        Weapons = new Dictionary<int, WeaponData>();
        Instantiate();
    }

    void Instantiate()
    {
        if (File.Exists(FilePath))
        {
            JsonData = File.ReadAllText(FilePath);
            Weapons = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<int, WeaponData>>(JsonData);
        }
    }

    public WeaponData GetWeaponData(int Weapon) // Original intentions. Loop through Inventory with this to find weaponData
    {
        return Weapons[Weapon];
    }

    public void Equip(int index, Creature equipTO)
    {

        Debug.Log("Weapon Index: " + index.ToString());
        Debug.Log("Weapon Name: " + Weapons[index].WeaponName);
        Weapons[index].Equip(Weapons[index], equipTO.GetSlot(Weapons[index].Appendage));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
