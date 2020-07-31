using TMPro;
using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace menu
{
    public class ItemMenu : MonoBehaviour, IMenu
    {
        [SerializeField]
        int context = 1; // this is editable

        int index = 0;

        public GameObject slot;

        Inventory _Inventory;
        InventoryMessage currentMessage;

        MenuManager Manager;
        Selection select;

        Material shader;

        bool keyPress;
        bool noItem;

        RectTransform pos;

        // Start is called before the first frame update
        void Start()
        {
            Manager = FindObjectOfType<MenuManager>();
            select = FindObjectOfType<Selection>();
            _Inventory = FindObjectOfType<Inventory>();

            Manager.MaxWidgets = _Inventory.CurrentSize;
            Manager.AddSubMenu(this, select);
            AddMenu();
        }

        public void Open()
        {

            Manager.MenuContext = context;

            if (_Inventory.ItemList.Count > 0)
            {
                noItem = false;

                Debug.Log("Open");

                for (int i = 0; i < _Inventory.CurrentSize; i++)
                {


                    Manager.newWidget(slot);
                }
            }

            else
            {
                noItem = true;
                Instantiate(slot);
                //slot.CreateRectangle(null, "No Items");
                Manager.newWidget(slot);
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


        public void Execute()
        {
            select.initalize(_Inventory.ItemList[Manager.WidgetIndex]);
            Manager.OpenSubMenu(this);
        }

        public void AddMenu()
        {
            Manager.AddMenu(context, this);
        }
    }
}