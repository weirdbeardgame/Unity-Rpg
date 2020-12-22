using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace menu
{

    public class MenuManager : MonoBehaviour, IReceiver // This is to handle functionality. To handle Inputs  
    {
        Messaging message;

        PScreen Screen;
        AppData CApp;
        List<Widget> CurrentWidgets;
        public List<GameObject> Apps;

        Queue<InputData> CurrentInputs;

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
            Screen = new PScreen();
            CurrentInputs = new Queue<InputData>();

            Subscribe();
        }

        public void Subscribe()
        {
            if (!IsSubscribed)
            {
                IsSubscribed = true;
                message.Subscribe(MessageType.INPUT, this);
            }
        }

        public void Unsubscribe()
        {
            IsSubscribed = false;
            message.Unsubscribe(MessageType.INPUT, this);
        }

        public void Receive(object message)
        {
            CurrentInputs.Enqueue((InputData)message);
        }

        void Update()
        {
            if (CurrentInputs.Count > 0)
            {
                if (CurrentInputs.Peek().CurrentInput == Inputs.START && !IsOpened)
                {
                    Debug.Log("START PRESSED!");
                    CurrentInputs.Dequeue();
                    Open(0);
                }
                else if (IsOpened && CurrentInputs.Peek().CurrentInput != Inputs.START)
                {
                    Screen.CurrentScreen.GetComponent<AppData>().Input(CurrentInputs.Dequeue().CurrentInput);
                }
                if (CurrentInputs.Peek().CurrentInput == Inputs.START && IsOpened)
                {
                    CurrentInputs.Dequeue();
                    Close();
                }
            }
            if (Screen)
            {
                Screen.Draw(); // Run all screen and subscreen logic
            }
        }

        public void Open(int index)
        {

            Debug.Log("OPEN");

            Destroy(Arrow);
            CurrentWidgets = null;
            CurrentWidgets = new List<Widget>();

            Screen.Open(Apps[index]);
            CApp = Apps[index].GetComponent<AppData>();

            CurrentWidgets = Screen.CurrentScreen.GetComponent<AppData>().Widgets;
            Arrow = Instantiate(InstantiateArrow);

            IsOpened = true;
        }

        public void Close()
        {
            Screen.Close();
        }
    }
}