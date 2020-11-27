using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace menu
{

    public class MenuManager : MonoBehaviour, IReceiver
    {
        Messaging message;

        PScreen Screen;
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
        GameObject Arrow;

        // The current screen that the player interacts with.
        GameObject Panel;

        public int WidgetIndex;

        // Start is called before the first frame update
        void Start()
        {               
            state = FindObjectOfType<StateMachine>();
            message = FindObjectOfType<Messaging>();
            Screen = FindObjectOfType<PScreen>();
            InputData = new Queue<Inputs>();

            Subscribe();
        }


        public void initialize()
        {
            InputData = new Queue<Inputs>();

            message = FindObjectOfType<Messaging>();

            Pcam = FindObjectOfType<PlayerCamera>();
            state = FindObjectOfType<StateMachine>();

        }

        void IncreaseIndex()
        {
            if (_WidgetIndex != _MaxWidgets)
            {
                _WidgetIndex += 1;
                //Arrow.transform.SetParent(_Widgets[_WidgetIndex].transform);
            }
            return;
        }

        void DecreaseIndex()
        {
            // This lasts a frame stupid!
            if (_WidgetIndex > 0)
            {
                _WidgetIndex -= 1;
                //Arrow.transform.SetParent(_Widgets[_WidgetIndex].transform);
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
                            DecreaseIndex();
                            Debug.Log("Current Index: " + _WidgetIndex);
                            CurrentInput = Inputs.NULL;
                            break;

                        case Inputs.DOWN:
                            IncreaseIndex();
                            Debug.Log("Current Index: " + _WidgetIndex);
                            CurrentInput = Inputs.NULL;
                            break;

                        case Inputs.RIGHT:
                            // For future grid movement
                            break;

                        case Inputs.LEFT:
                            // For future grid movement
                            break;

                        case Inputs.A:
                            //_Widgets[_WidgetIndex].GetComponent<Widget>().Execute();
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
                        Screen.Open(Screens[0]);
                        Panel.GetComponent<Image>().enabled = true;

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