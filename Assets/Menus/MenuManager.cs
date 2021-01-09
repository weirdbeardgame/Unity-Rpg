using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace menu
{
    // General Menu Properties, Type of screen. Needs input? Drawing modes
    public enum MenuProperties { INPUT, APP, SUBAPP, GRID, LIST};

    public class MenuManager : MonoBehaviour, IReceiver // This is to handle functionality. To handle Inputs  
    {
        Messaging message;

        PScreen Screen;
        AppData CApp;
        List<Widget> CurrentWidgets;
        public List<GameObject> Apps; // List of Screens

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
            if (state.State != States.PAUSE)
            {
                InputData temp = (InputData)message;

                if (temp.CurrentInput == Inputs.START)
                {
                    CurrentInputs.Enqueue(temp);
                }
            }

            if (state.State == States.PAUSE)
            {
                CurrentInputs.Enqueue((InputData)message);
            }
        }
 
        void FixedUpdate()        
        {
            if (CurrentInputs.Count > 0)
            {
                switch (state.State)
                {
                    case States.PAUSE:

                        switch (CurrentInputs.Peek().CurrentInput)
                        {
                            case Inputs.START:
                                CurrentInputs.Dequeue();
                                Close();
                                break;
                            default:
                                Screen.CurrentScreen.GetComponent<AppData>().Input(CurrentInputs.Dequeue().CurrentInput);
                                break;
                        }
                        if (Screen) 
                        { 
                            Screen.Draw(); // Run all screen and subscreen logic 
                        }
                        break;

                    case States.MAIN:
                        if (CurrentInputs.Peek().CurrentInput == Inputs.START)
                        {
                            CurrentInputs.Dequeue();
                            Open(0);
                        }
                        else
                        {
                            break;
                        }
                        break;
                }
            }
            else
            {
                return;
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
            state.State = States.PAUSE;

            CurrentWidgets = Screen.CurrentScreen.GetComponent<AppData>().Widgets;
            Arrow = Instantiate(InstantiateArrow);

            IsOpened = true;
        }

        public void Close()
        {
            Screen.Close();
            state.State = States.MAIN;
            IsOpened = false;
        }
    }
}