using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace menu
{
    public enum MenuDisplay {GRID, LIST};

    public class AppData : MonoBehaviour
    {
        protected int AppID;
        protected string AppName;
        public List<Widget> Widgets;
        public List<Widget> SubWidgets;
        public List<GameObject> SubScreens;
        public MenuManager Menu;
        public MenuDisplay display;
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

        public void AddWidget(Widget widget, int i = 0)
        {
            Menu = FindObjectOfType<MenuManager>();

            foreach(var Prop in Properties)
            {
                switch (Prop)
                {
                    case MenuProperties.APP:
                        if (Widgets == null)
                        {
                            Widgets = new List<Widget>();
                        }
                        widget.transform.SetParent(Menu.GetScreen().transform);

                        Widgets.Add(widget);
                        break;
                    case MenuProperties.SUBAPP:
                        if (SubWidgets == null)
                        {
                            SubWidgets = new List<Widget>();
                        }
                        widget.transform.SetParent(Menu.GetSubScreen(i).transform);
                        SubWidgets.Add(widget);
                        break;
                }
            }
        }
    }
}