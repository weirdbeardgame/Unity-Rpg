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

    public WeaponData GetWeaponData(int Weapon)
    {
        return Weapons[Weapon];
    }

    public void Equip(int Index, Creature equipTO)
    {

        int Weapon = Index - 1;

        Debug.Log("Weapon Index: " + Weapon.ToString());
        Debug.Log("Weapon Name: " + Weapons[Weapon].WeaponName);

        Weapons[Weapon].Equip(Weapons[Weapon], equipTO.getCreature().GetSlot(Weapons[Weapon].Appendage));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
