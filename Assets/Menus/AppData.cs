using System.Collections;
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
            AppID = -1;
            Widgets = null;
            AppName = "Boo!";
        }

        public virtual void Draw()
        {
            // All Updates to screens and widgets happen in here.
            foreach(var Prop in Properties)
            {
               switch (Prop)
                {
                    case MenuProperties.LIST:
                        // Draw Verticle widgets
                        break;
                    case MenuProperties.GRID:
                        // Draw grid of widgets
                        break;
                }
            }
        }

        public virtual void Input(Inputs In)
        {
            // Handling of Input per app happens here
        }

        public void AddWidget(Widget widget)
        {
            if (Widgets == null)
            {
                Widgets = new List<Widget>();
            }

            Widgets.Add(widget);
        }
    }
}