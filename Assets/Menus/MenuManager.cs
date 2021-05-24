﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace menu
{
    // General Menu Properties, Type of screen. Needs input? Drawing modes
    public enum MenuProperties { INPUT, APP, SUBAPP};

    public class MenuManager : MonoBehaviour
    {
        Messaging message;
        PScreen screen;
        public AppData cApp;
        // Widgets
        List<Widget> listWidgets;
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
            screen.Open(apps[index]);
            cApp = apps[index].GetComponent<AppData>();
            state.State = States.PAUSE;

            switch (cApp.GetComponent<AppData>().display)
            {
            case MenuDisplay.LIST:
                listWidgets = new List<Widget>();
                listWidgets = cApp.widgets;

                if (listWidgets.Count > 0)
                {
                    selectedWidget = listWidgets[0];
                }
            break;
            case MenuDisplay.GRID:
                transform = (RectTransform)screen.CurrentScreen.transform;
                gridWidgets = new Widget[(int)transform.rect.width, (int)transform.rect.height];
                gridWidgets = cApp.gridWidgets;
                if (gridWidgets != null)
                {
                    selectedWidget = gridWidgets[(int)position.x, (int)position.y];
                }
            break;
            }

            arrow = Instantiate(instantiateArrow);

            isOpened = true;
        }

        public void Accept()
        {
            if (selectedWidget)
            {
                selectedWidget.Execute();
            }
        }

        public void Move(Vector2 pos)
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