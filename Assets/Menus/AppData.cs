using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***************************************************************
* All Screens must be a scale of 1, 1, 1 and a size of
* Width: 176 Height: 258
***************************************************************/
namespace menu
{
    public enum MenuDisplay { GRID, LIST };

    public class AppData : MonoBehaviour
    {
        protected int appID;
        protected string appName;
        PScreen screen;
        public List<Widget> widgetsToAdd;
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

        public void AddWidget(Widget widget, int i = 0)
        {
            menu = FindObjectOfType<MenuManager>();
            screen = FindObjectOfType<PScreen>();
            RectTransform transform = (RectTransform)screen.CurrentScreen.transform;

            int row = (int)transform.rect.width, col = (int)transform.rect.height;
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
                                if (gridWidgets == null)
                                {
                                    gridWidgets = new Widget[row , col];
                                }
                                if (x < row && y < col)
                                {
                                    gridWidgets[x, y] = widget;
                                    x += 5;
                                    if ((x % 3) == 0)
                                    {
                                        y += 5;
                                        x = 0;
                                    }
                                }
                                break;
                        }
                        break;
                }
            }
        }
    }
}