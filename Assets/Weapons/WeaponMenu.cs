using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace menu
{
    public class WeaponMenu : MonoBehaviour, IMenu
    {
        Party players;
        MenuManager Manager;
        Inventory inventory;
        GameObject Slot;

        Material shader;

        Weapon Weapon;

        int PlayerIndex;
        int WeaponIndex;

        // Start is called before the first frame update
        void Start()
        {
            Manager = FindObjectOfType<MenuManager>();
            inventory = FindObjectOfType<Inventory>();
            players = FindObjectOfType<Party>();
            Weapon = FindObjectOfType<Weapon>();
            AddMenu();
        }

        public void AddMenu()
        {
            Manager.AddMenu(2, this);
        }

        public void Open()
        {
            Manager.MaxWidgets = players.PartyMembers.Count;

            Debug.Log("Open Weapons");

            for (int i = 0; i < players.PartyMembers.Count; i++)
            {
                Instantiate(Slot);
                Manager.newWidget(Slot);
            }
        }

        public void Use(int i)
        {

        }

        public void SetShader(Material s)
        {
            shader = s;
        }

        public Material GetShader()
        {
            return shader;
        }


        public void ShowItems()
        {
            PlayerIndex = Manager.WidgetIndex;
            Manager.ClearWidgets();
            for (int i = 0; i < Weapon.Weapons.Count; i++)
            {
                if (Weapon.Weapons[i].Job == players.PartyMembers[PlayerIndex].Job)
                {
                    Instantiate(Slot);
                    //slot.CreateSquare(() => Equip(i), Weapon.Weapons[i].WeaponName);
                    Manager.newWidget(Slot);
                }
            }
        }

        public void Equip(int i)
        {
            players.PartyMembers[PlayerIndex].Equip((Appendage)i, Weapon);
        }

        public void Dequip(int i)
        {
            players.PartyMembers[PlayerIndex].Dequip((Appendage)i, Weapon.GetWeaponData(i));
        }


        // Update is called once per frame
        void Update()
        {

        }
    }
}