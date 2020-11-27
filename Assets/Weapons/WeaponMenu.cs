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

        int PlayerIndex;
        int WeaponIndex;

        // Start is called before the first frame update
        void Start()
        {
            Manager = FindObjectOfType<PScreen>();
            inventory = FindObjectOfType<Inventory>();
            players = FindObjectOfType<Party>();
            Weapon = FindObjectOfType<Weapon>();
        }

        public override void Init()
        {
            base.Init();
        }
    }
}