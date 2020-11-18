using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace menu
{

    public class MenuManager : MonoBehaviour, IReceiver
    {

        // Here's what I want to accomplish with this round
        // Create a submenu functionality that makes submenus appear as the original menu.
        // Maybe change the style of menu as much as i'd like to stay with the cell phone aspect of it all. 

        SortedDictionary<int, IMenu> Menus;
        SortedDictionary<IMenu, IMenu> SubMenus;

        private List<GameObject> _Widgets;

        Messaging message;
        Animator animator;

        Queue<Inputs> InputData;

        gameStateMessage StateMessage;

        // Amount of Buttons in Menu and navagating it
        private int _WidgetIndex = 0;
        private int _MaxWidgets = 0;

        // What Menu are we looking at
        int _MenuContext = 0;

        Vector2 selectionPosition;

        private Vector2 CanvasSize;
        private Vector2 MaxCanvasSize;

        float canvasPosoitionX;
        float canvasPositionY;

        private Vector2 pos;
        private Vector2 currentPointerPosition;
        private Vector2 previousPointerPosition;

        PlayerCamera Pcam;

        // The sate of game (PAUSED)
        StateMachine state;

        private bool IsSubscribed;
        private bool IsOpened;

        // The Arrow itself
        GameObject Arrow;

        // The canvas that the player interacts with it's private in nature naturally :p
        GameObject Panel;
        GameObject Canvas;

        GameObject SecondPanel;
        GameObject SecondCanvas;

        Vector2 TrianglePos;

        public int WidgetIndex
        {
            get;
            set;
        }

        public int MaxWidgets
        {
            get
            {
                return _MaxWidgets;
            }

            set
            {
                _MaxWidgets = value;
            }

        }

        //What menu are we on?
        public int MenuContext
        {
            get;
            set;
        }

        // Start is called before the first frame update
        void Start()
        {
            _Widgets = new List<GameObject>();

            Menus = new SortedDictionary<int, IMenu>();

            message = FindObjectOfType<Messaging>();

            InputData = new Queue<Inputs>();

            Pcam = FindObjectOfType<PlayerCamera>();
            state = FindObjectOfType<StateMachine>();

            SubMenus = new SortedDictionary<IMenu, IMenu>();
            Subscribe();

            FindCanvas();
            Panel.GetComponent<Image>().enabled = false;
        }


        public void initialize()
        {
            _Widgets = new List<GameObject>();
            InputData = new Queue<Inputs>();

            Menus = new SortedDictionary<int, IMenu>();

            message = FindObjectOfType<Messaging>();

            Pcam = FindObjectOfType<PlayerCamera>();
            state = FindObjectOfType<StateMachine>();

            SubMenus = new SortedDictionary<IMenu, IMenu>();

            FindCanvas();
            Panel.AddComponent<GridLayout>();

        }

        public void CreateArrow()
        {
            Arrow = new GameObject("Arrow");
            Arrow.AddComponent<RectTransform>();
            Arrow.AddComponent<Image>();

            Arrow.GetComponent<Image>().sprite = Resources.Load<Sprite>("Triangle");
            Arrow.GetComponent<RectTransform>().sizeDelta = new Vector2(5, 10);
            //            Arrow.transform.SetParent(_Widgets[_WidgetIndex].transform);
            //          Arrow.transform.localPosition = new Vector2(-100, 0);
        }

        public void CreatePanel(GameObject NewCanvas, GameObject panel)
        {
            NewCanvas = new GameObject("Canvas");
            Canvas c = NewCanvas.AddComponent<Canvas>();

            c.renderMode = RenderMode.ScreenSpaceOverlay;

            NewCanvas.AddComponent<CanvasScaler>();
            NewCanvas.AddComponent<GraphicRaycaster>();

            Panel = new GameObject("Panel");
            Panel.AddComponent<CanvasRenderer>();
            Panel.AddComponent<RectTransform>();

            Image i = Panel.AddComponent<Image>();
            i.color = Color.gray;
            Panel.transform.SetParent(NewCanvas.transform, false);

            Panel.GetComponent<RectTransform>().sizeDelta = new Vector2(270.3f, 436.3f);

            SecondPanel = Panel;
            SecondCanvas = NewCanvas;
        }

        void IncreaseIndex()
        {
            if (_WidgetIndex != _MaxWidgets)
            {
                _WidgetIndex += 1;
                Arrow.transform.SetParent(_Widgets[_WidgetIndex].transform);
            }
            return;
        }

        void DecreaseIndex()
        {
            // This lasts a frame stupid!
            if (_WidgetIndex > 0)
            {
                _WidgetIndex -= 1;
                Arrow.transform.SetParent(_Widgets[_WidgetIndex].transform);
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
                        Panel.GetComponent<Image>().enabled = true;
                        OpenMenu(0);
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

            if (IsOpened)
            {
                if (_Widgets.Count == 0)
                {
                    Debug.Log("No Slot");
                }

                if (Arrow != null)
                {
                    Arrow.GetComponent<RectTransform>().localPosition = TrianglePos;
                }

                if (Arrow == null)
                {
                    Debug.Log("I don't see no arrow round here lad!");
                    CreateArrow();
                }
            }
        }

        // Add a menu into the dictonary
        public void AddMenu(int Context, IMenu Menu)
        {
            Menus.Add(Context, Menu);
        }

        // Open the Menu
        public void OpenMenu(int c)
        {
            if (_Widgets != null && _Widgets.Count > 0)
            {
                for (int i = 0; i < _Widgets.Count; i++)
                {
                    _Widgets[i] = null;
                }

                _Widgets.Clear();
            }

            Menus[c].Open();
            ResetIndex();
            _MenuContext = c;
            CreateArrow();
            IsOpened = true;
        }

        public void OpenSubMenu(IMenu main)
        {
            if (_Widgets != null && _Widgets.Count > 0)
            {
                for (int i = 0; i < _Widgets.Count; i++)
                {

                    _Widgets[i] = null;
                }

                _Widgets.Clear();
            }
            ResetIndex();
            SubMenus[main].Open();
        }

        public void newWidget(GameObject w)
        {
            int Y = 0;
            w.transform.SetParent(Panel.transform);
            w.transform.localPosition = new Vector2(0, Y);
            Y -= 5;
            _Widgets.Add(w);
        }

        public void CloseSubMenu()
        {
            for (int i = 0; i < _Widgets.Count; i++)
            {
                _Widgets[i] = null;

            }

            _Widgets.Clear();
            OpenMenu(_MenuContext);
        }

        public void Close()
        {
            Panel.GetComponent<Image>().enabled = false;

            Destroy(Arrow);
            for (int i = 0; i < _Widgets.Count; i++)
            {
                _Widgets[i] = null;
            }

            _Widgets.Clear();

            StopCoroutine(DetectInput());

            IsOpened = false;
            Debug.Log("Menu closed");
        }


        public void ClearWidgets()
        {
            WidgetIndex = 0;
            _Widgets.Clear();
        }

        public void FindCanvas()
        {
            Panel = GameObject.Find("Menu");
        }

        public void ResetIndex()
        {
            _WidgetIndex = 0;
        }

        public void AddSubMenu(IMenu main, IMenu sub)
        {
            SubMenus.Add(main, sub);
        }
    }
}