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
            foreach (var Prop in Properties)
            {
                switch (Prop)
                {
                    case MenuProperties.GRID:

                        break;

                    case MenuProperties.LIST:
                        for (int i = 0; i < Widgets.Count; i++)
                        {
                            Widgets[i].gameObject.transform.SetParent(Menu.GetScreen().transform);
                            Widgets[i].gameObject.transform.localPosition = new Vector2(Menu.GetScreen().transform.localPosition.x, Y);
                            Y += 5;
                        }
                        break;
                }
            }
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
            if (Widgets == null)
            {
                Widgets = new List<Widget>();
            }

            Widgets.Add(widget);
        }
    }
}