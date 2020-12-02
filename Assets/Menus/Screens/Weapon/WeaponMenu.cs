using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace menu
{
    public class WeaponMenu : ScreenData
    {
        Party players;
        PScreen Manager;
        Inventory inventory;
        GameObject Slot;

        Material shader;

        Weapon Weapon;
        WeaponWidgetData WeaponWidget;

        int PlayerIndex;
        int WeaponIndex;

        // Start is called before the first frame update
        void Start()
        {
            Manager = FindObjectOfType<PScreen>();
            inventory = FindObjectOfType<Inventory>();
            players = FindObjectOfType<Party>(); // For the player select screen.
            Weapon = FindObjectOfType<Weapon>();
        }

        public override void Init()
        {
            for (int i = 0; i < inventory.Equipables.Count; i++)
            {
                WeaponWidget = new WeaponWidgetData(inventory.getEquipables(i));
            }
        }
    }
}