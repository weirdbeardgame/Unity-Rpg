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
        ScreenData CScreen;
        List<Widget> CurrentWidgets;
        public List<GameObject> Screens;

        Queue<Inputs> InputData;

        gameStateMessage StateMessage;

        // Amount of Buttons in Menu and navagating it
        private int _WidgetIndex = 0;
        private int _MaxWidgets = 0;

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
            InputData = new Queue<Inputs>();

            Subscribe();
        }

        void IncreaseIndex()
        {
            if (_WidgetIndex != _MaxWidgets)
            {
                _WidgetIndex += 1;
                Arrow.transform.SetParent(CurrentWidgets[_WidgetIndex].transform);
            }
            return;
        }

        void DecreaseIndex()
        {
            // This lasts a frame stupid!
            if (_WidgetIndex > 0)
            {
                _WidgetIndex -= 1;
                Arrow.transform.SetParent(CurrentWidgets[_WidgetIndex].transform);
                return;
            }
        }

        IEnumerator DetectInput()
        {
            while (InputData.Count == 0)
            {
                yield return null;
            }

            Inputs CurrentInput = InputData.Dequeue();

            if (IsOpened)
            {
                if (CurrentInput != Inputs.NULL)
                {
                    switch (CurrentInput)
                    {
                        case Inputs.UP:
                            // For future Grid Movement
                            break;

                        case Inputs.DOWN:
                            // For future Grid Movement
                            break;

                        case Inputs.RIGHT:
                            IncreaseIndex();
                            Debug.Log("Current Index: " + _WidgetIndex);
                            CurrentInput = Inputs.NULL;
                            break;

                        case Inputs.LEFT:
                            DecreaseIndex();
                            Debug.Log("Current Index: " + _WidgetIndex);
                            CurrentInput = Inputs.NULL;
                            break;

                        case Inputs.A:
                            CurrentWidgets[_WidgetIndex].GetComponent<Widget>().Execute();
                            CurrentInput = Inputs.NULL;
                            break;
                    }
                }
            }

            if (CurrentInput == Inputs.START)
            {
                StateMessage = ScriptableObject.CreateInstance<gameStateMessage>();

                switch (state.State)
                {
                    case States.MAIN:
                        StateMessage.construct(States.PAUSE, state.CurrrentFlag);
                        Open(0);     
                        _WidgetIndex = 0;
                        break;

                    case States.PAUSE:
                        StateMessage.construct(States.MAIN, state.CurrrentFlag);
                        Close();
                        break;
                }
            }
            yield return _WidgetIndex;
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
            InputData.Enqueue((Inputs)message);
        }

        void Update()
        {
            if (InputData.Count > 0)
            {
                StartCoroutine(DetectInput());
            }
            if (Screen)
            {
                Screen.Draw(); // Run all screen and subscreen logic
            }
        }

        public void Open(int index)
        {
            Destroy(Arrow);
            CurrentWidgets = null;
            CurrentWidgets = new List<Widget>();

            Screen.Open(Screens[index]);
            CScreen = Screens[index].GetComponent<ScreenData>();

            CurrentWidgets = Screen.CurrentScreen.GetComponent<ScreenData>().Widgets;
            _MaxWidgets = CurrentWidgets.Count;

            Arrow = Instantiate(InstantiateArrow);

            IsOpened = true;
        }

        public void Close()
        {
            Screen.Close();
        }

        public void ResetIndex()
        {
            _WidgetIndex = 0;
        }

    }
}