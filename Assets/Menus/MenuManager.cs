using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace menu
{
    // General Menu Properties, Type of screen. Needs input? Drawing modes
    public enum MenuProperties { INPUT, APP, SUBAPP, GRID, LIST};

    public class MenuManager : MonoBehaviour
    {
        Messaging message;
        [SerializeField]
        InputActionMap menuInput;
        PScreen Screen;
        public AppData CApp;
        List<Widget> CurrentWidgets;
        List<Widget> SubWidgets;
        Widget selectedWidget;
        public List<GameObject> Apps; // List of Screens
        gameStateMessage StateMessage;

        // What Menu are we looking at
        int _MenuContext = 0;

        PlayerCamera Pcam;

        // The sate of game (PAUSED)
        StateMachine state;
        private bool IsSubscribed;
        private bool IsOpened;

        // The Arrow itself
        public GameObject InstantiateArrow;
        GameObject Arrow;
        public int WidgetIndex;

        // Start is called before the first frame update
        void Start()
        {
            state = FindObjectOfType<StateMachine>();
            message = FindObjectOfType<Messaging>();
            Screen = FindObjectOfType<PScreen>();
        }

        public GameObject GetScreen()
        {
            return Screen.GetScreen;
        }

        public GameObject GetSubScreen(int i)
        {
            if (Screen.SubScreens != null)
            {
                return Screen.SubScreens[i];
            }
            else
            {
                return null;
            }
        }

        public void Open(int index)
        {
            Debug.Log("OPEN");
            menuInput.Enable();

            Destroy(Arrow);
            CurrentWidgets = null;
            CurrentWidgets = new List<Widget>();

            Screen.Open(Apps[index]);
            CApp = Apps[index].GetComponent<AppData>();
            state.State = States.PAUSE;

            CurrentWidgets = Screen.CurrentScreen.GetComponent<AppData>().Widgets;
            Arrow = Instantiate(InstantiateArrow);

            IsOpened = true;
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
            Debug.Log("POS: " + pos);
            Arrow.transform.Translate(pos);
            // Grab each widget's position and go from there.
            for (int i = 0; i < CurrentWidgets.Count; i++)
            {
                if (pos == (Vector2)CurrentWidgets[i].transform.position )
                {
                    selectedWidget = CurrentWidgets[i];
                    Debug.Log("Widget: " + CurrentWidgets[i].name);
                }
                if (pos != (Vector2)CurrentWidgets[i].transform.position)
                {
                    // Get em back on track
                }
            }
        }

        public void Close()
        {
            Screen.Close();
            state.State = States.MAIN;
            IsOpened = false;
        }
    }
}