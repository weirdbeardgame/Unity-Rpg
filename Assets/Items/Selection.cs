using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace menu
{
    public class Selection : MonoBehaviour, IMenu
    {
        InventoryMessage currentMessage;
        MenuManager Manager;
        Party selection;
        ItemData current;
        GameObject slot;
        menuType type;

        Material shader;

        int index = 0;

        public void Open()
        {
            // This is for using items. Who useth the ITEM!!!            
            for (int i = 0; i < 2; i++)
            {
                Instantiate(slot);
                //slot.CreateRectangle(apply, selection.PartyMembers[i].CreatureName);
            }
        }

        public void SetShader(Material s)
        {
            shader = s;
        }

        public Material GetShader()
        {
            return shader;
        }

        public Selection()
        {
        }

        public void initalize(ItemData Current)
        {
            selection = FindObjectOfType<Party>();
            Manager = FindObjectOfType<MenuManager>();
            type = menuType.SUB;
            current = Current;
        }

        public void AddMenu()
        {
            return;
        }

        void apply()
        {
            //Use(Manager.WidgetIndex);
        }

        public void Use(int i)
        {
            currentMessage = new InventoryMessage();

            currentMessage.construct(current, itemState.USED);

            //current.Use(selection.PartyMembers[i]);
        }
    }
}