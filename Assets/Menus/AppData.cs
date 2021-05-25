using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace menu
{
    public enum MenuDisplay { GRID, LIST };

    public class AppData : MonoBehaviour
    {
        protected int appID;
        protected string appName;
        PScreen screen;
        public static List<Widget> widgets;
        public static Widget[,] gridWidgets;
        public List<GameObject> subScreens;
        public MenuManager menu;
        public MenuDisplay display;
        protected int x, y;
        protected int widgetIndex;
        protected List<MenuProperties> properties;

        public MenuProperties GetProperties(int i)
        {
            return properties[i];
        }

        public List<Widget> GetList
        {
            get
            {
                return widgets;
            }
        }

        public Widget[,] GetGrid
        {
            get
            {
                return gridWidgets;
            } 
        }

        public int PropertiesCount()
        {
            return properties.Count;
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
            menu = FindObjectOfType<MenuManager>();
            screen = FindObjectOfType<PScreen>();

            foreach (var Prop in properties)
            {
                switch (Prop)
                {
                    case MenuProperties.APP:
                        switch (display)
                        {
                            case MenuDisplay.LIST:
                                if (widgets == null)
                                {
                                    widgets = new List<Widget>();
                                }
                                widget.transform.SetParent(menu.GetScreen().transform);

                                widgets.Add(widget);
                                break;
                            case MenuDisplay.GRID:
                                RectTransform transform = (RectTransform)screen.CurrentScreen.transform;
                                if (gridWidgets == null)
                                {
                                    gridWidgets = new Widget[(int)transform.rect.width, (int)transform.rect.height];
                                }
                                gridWidgets[(int)widget.transform.position.x, (int)widget.transform.position.y] = widget;
                                break;
                        }
                        break;
                }
            }
        }
    }
}