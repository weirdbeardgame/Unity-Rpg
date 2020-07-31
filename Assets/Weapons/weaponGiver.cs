using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponGiver : MonoBehaviour
{

    Weapon data;

    bool IsTrigger;
    bool ItemGiven;

    public int id;


    // Start is called before the first frame update
    void Start()
    {

    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            IsTrigger = true;
        }

        else
        {
            IsTrigger = false;
        }

    }

    void GiveWeapon()
    {
        data = FindObjectOfType<Weapon>();
        Debug.Log("A new Weapon: " + data.GetWeaponData(id).WeaponName);
        data.GetWeaponData(id).AddToInventory();
        ItemGiven = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (IsTrigger && !ItemGiven && Input.GetButtonDown("Submit"))
        {
            GiveWeapon();
        }
    }
}
