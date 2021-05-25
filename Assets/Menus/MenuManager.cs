﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace menu
{
    // General Menu Properties, Type of screen. Needs input? Drawing modes
    public enum MenuProperties { INPUT, APP, SUBAPP };

    public class MenuManager : MonoBehaviour
    {
        Messaging message;
        PScreen screen;
        public AppData cApp;
        // Widgets
        List<Widget> listWidgets;
        Vector2Int origPos, tarPos;
        Widget[,] gridWidgets;
        Widget selectedWidget;
        int widgetIndex = 0;

        public List<GameObject> apps; // List of Screens
        gameStateMessage stateMessage;

        // What Menu are we looking at
        int menuContext = 0;
        PlayerCamera pCam;

        // The sate of game (PAUSED)
        StateMachine state;
        private bool isSubscribed;
        private bool isOpened;

        // The Arrow itself
        public GameObject instantiateArrow;
        GameObject arrow;
        Vector2 position;

        // Start is called before the first frame update
        void Start()
        {
            state = FindObjectOfType<StateMachine>();
            message = FindObjectOfType<Messaging>();
            screen = FindObjectOfType<PScreen>();
        }

        public GameObject GetScreen()
        {
            return screen.GetScreen;
        }

        public GameObject GetSubScreen(int i)
        {
            if (screen.SubScreens != null)
            {
                return screen.SubScreens[i];
            }
            else
            {
                return null;
            }
        }

        public void Open(int index)
        {
            Debug.Log("OPEN");
            RectTransform transform = null;
            Destroy(arrow);
            Debug.Log("Apps: " + apps[index].GetComponent<AppData>().name);
            cApp = screen.Open(apps[index]);
            state.State = States.PAUSE;
            arrow = Instantiate(instantiateArrow);

            switch (cApp.GetComponent<AppData>().display)
            {
                case MenuDisplay.LIST:
                    listWidgets = cApp.GetList;

                    if (listWidgets.Count > 0)
                    {
                        selectedWidget = listWidgets[0];
                    }
                    break;
                case MenuDisplay.GRID:
                    transform = (RectTransform)screen.CurrentScreen.transform;
                    gridWidgets = new Widget[(int)transform.rect.width, (int)transform.rect.height];
                    gridWidgets = cApp.GetGrid;
                    if (gridWidgets != null)
                    {
                        selectedWidget = gridWidgets[(int)position.x, (int)position.y]; // Position [0,0] I need to do an inital search along X to find first element
                    }
                    break;
            }
            isOpened = true;
        }

        public void Accept()
        {
            if (selectedWidget)
            {
                selectedWidget.Execute();
            }
        }

        Widget FindNext(Vector2Int dir)
        {
            Widget toFind = null;
            switch (cApp.display)
            {
                case MenuDisplay.GRID:
                    for (int x = 0; x < screen.CurrentScreen.GetComponent<RectTransform>().rect.width; x += dir.x)
                    {
                        for (int y = 0; y < screen.CurrentScreen.GetComponent<RectTransform>().rect.height; y += dir.y)
                        {
                            // Do a search in here through current grid
                        }
                    }
                    break;
                case MenuDisplay.LIST:
                    // return next index in here
                    widgetIndex += dir.y;
                    toFind = listWidgets[widgetIndex];
                    break;
            }
            return toFind;
        }

        public void Move(Vector2Int pos)
        {
            switch (cApp.GetComponent<AppData>().display)
            {
                case MenuDisplay.LIST:
                    Debug.Log("POS: " + position);
                    arrow.transform.Translate(position);
                    widgetIndex += (int)Mathf.Sign(position.y);
                    // Grab each widget's position and go from there.
                    selectedWidget = listWidgets[widgetIndex];
                    Debug.Log("Widget Name: " + selectedWidget.GetComponent<Widget>().name);

                    break;

                case MenuDisplay.GRID:
                    int movementX = 0;
                    int movementY = 0;
                    // Adjust position in grid and check for widget
                    if (pos.x > 0 || pos.x < 0)
                    {
                        movementX = (int)Mathf.Sign(pos.x);
                    }
                    if (pos.y > 0 || pos.y < 0)
                    {
                        movementY = (int)Mathf.Sign(pos.y);
                    }
                    for (int x = 0; x < gridWidgets.GetLength(0); x += movementX)
                    {
                        for (int y = 0; y < gridWidgets.GetLength(1); y += movementY)
                        {
                            if (gridWidgets[x, y])
                            {
                                selectedWidget = gridWidgets[x, y];
                            }
                        }
                    }
                    break;
            }
        }

        public void Close()
        {
            Destroy(arrow);
            screen.Close();
            state.State = States.MAIN;
            isOpened = false;
        }
    }
}