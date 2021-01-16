﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace menu
{
    public class AppData : MonoBehaviour
    {
        protected int AppID;
        protected string AppName;
        public List<Widget> Widgets;
        public List<GameObject> SubScreens;
        public MenuManager Menu;

        int X, Y;

        protected int WidgetIndex;
        protected List<MenuProperties> Properties;

        public MenuProperties GetProperties(int i)
        {
            return Properties[i];
        }

        public int PropertiesCount()
        {
            return Properties.Count;
        }

        public virtual void Init()
        {
        }

        public virtual void Draw()
        {
        }

        public virtual void Input(Inputs In)
        {
            // Handling of Input per app happens here
        }

        public void AddWidget(Widget widget)
        {
            Menu = FindObjectOfType<MenuManager>();

            if (Widgets == null)
            {
                Widgets = new List<Widget>();
            }
            widget.transform.SetParent(Menu.GetScreen().transform);

            Widgets.Add(widget);
        }
    }
}