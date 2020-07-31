using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class commandMenus : MonoBehaviour, IReceiver
{

    //Player Menus
    SortedDictionary<JobSystem, BattleMIface> Menus;

    // Action Menus (Item, Skills, Magic)
    SortedDictionary<int, BattleMIface> SubMenus;

    List<GameObject> Widgets;

    Queue<Inputs> InputData;

    Gauge gauge;

    int WidgetIndex = 0;
    int Index = 0;

    Creature Opened;

    GameObject Commands;
    GameObject PlayerStatus;
    GameObject Panel;


    GameObject PlayerStatSlot1;
    GameObject PlayerStatSlot2;
    GameObject PlayerStatSlot3;

    GameObject SelectionArrow;
    GameObject Arrow;

    List<Baddies> BadParty;

    bool Init;
    public bool IsOpened;
    bool Targeting;
    bool isSubscribed;
    public bool CommandSelected;

    Messaging message;

    JobSystem Jobs;

    SkillData SkillToTarget;

    // Start is called before the first frame update
    void Start()
    {
        /*PlayerStatus = GameObject.Find("menu");
        Commands = GameObject.Find("Command Screen");
        Commands.GetComponent<Image>().enabled = false;

        Widgets = new List<Widget>();
        message = FindObjectOfType<Messaging>();
        Subscribe();
        InputData = new Queue<Inputs>();
        Menus = new SortedDictionary<JobSystem, BattleMIface>();
        SubMenus = new SortedDictionary<int, BattleMIface>();*/
    }

    void IncreaseIndex()
    {
        //This lasts a frame you fool! Thunder Cross Splito Attack!
        if (WidgetIndex <= Widgets.Count)
        {
            WidgetIndex += 1;
            Arrow.transform.SetParent(Widgets[WidgetIndex].transform);

        }

        else
        {
            return;
        }
    }

    public void CreateArrow()
    {
        Arrow = new GameObject("Arrow");
        Arrow.AddComponent<RectTransform>();
        Arrow.AddComponent<Image>();

        Arrow.GetComponent<Image>().sprite = Resources.Load<Sprite>("Triangle");
        Arrow.GetComponent<RectTransform>().sizeDelta = new Vector2(5, 10);
        Arrow.transform.SetParent(Widgets[WidgetIndex].transform);
        Arrow.transform.localPosition = new Vector2(-100, 0);

    }

    void DecreaseIndex()
    {
        if (WidgetIndex > 0)
        {
            WidgetIndex -= 1;
            Arrow.transform.SetParent(Widgets[WidgetIndex].transform);
            return;
        }
    }

    public void Initlaize()
    {
        PlayerStatus = GameObject.Find("menu");
        message = FindObjectOfType<Messaging>();

        Subscribe();

        Widgets = new List<GameObject>();
        InputData = new Queue<Inputs>();
        Menus = new SortedDictionary<JobSystem, BattleMIface>();
        SubMenus = new SortedDictionary<int, BattleMIface>();
    }


    public void SetIsOpened(bool open)
    {
        IsOpened = open;
    }

    public void Subscribe()
    {
        isSubscribed = true;
        message.Subscribe(MessageType.INPUT, this);
    }

    public void Unsubscribe()
    {
        isSubscribed = false;
        message.Unsubscribe(MessageType.INPUT, this);
    }

    public void Receive(object message)
    {
        InputData.Enqueue((Inputs)message);
    }

    IEnumerator DetectInput()
    {
        while (InputData.Count == 0)
        {
            yield return null;
        }


        if (InputData.Count > 0)
        {
            Inputs CurrentInput = InputData.Dequeue();


            switch (CurrentInput)
            {
                case Inputs.UP:
                    DecreaseIndex();
                    Debug.Log("UP");
                    Debug.Log("Current Index: " + WidgetIndex);
                    CurrentInput = Inputs.NULL;
                    break;

                case Inputs.DOWN:
                    IncreaseIndex();
                    Debug.Log("Current Index: " + WidgetIndex);
                    CurrentInput = Inputs.NULL;
                    break;

                case Inputs.A:
                    Debug.Log("A press");
                    Widgets[WidgetIndex].GetComponent<Widget>().Execute();
                    CurrentInput = Inputs.NULL;
                    InputData.Clear();
                    Targeting = true;
                    break;
            }
        }
        yield return WidgetIndex;
    }


    public void AddWidget(GameObject w)
    {
        int Y = 0;

        Panel = Commands.transform.GetChild(0).gameObject;

        if (Widgets == null)
        {
            Widgets = new List<GameObject>();
        }

        w.transform.SetParent(Panel.transform);
        w.transform.localPosition = new Vector2(0, Y);
        Widgets.Add(w);

        Y -= 5;
    }

    public void Open(Creature Opener, List<Baddies> Villan)
    {
        if (!IsOpened)
        {
            Commands = (GameObject)Instantiate(Resources.Load("BattlePrefabs/BattleMenu"));
            Commands.GetComponentInChildren<Image>().enabled = true;
            Opened = Opener;
            Menus[Opened.Job].Open(Opened);
            BadParty = Villan;
            IsOpened = true;
        }

        Debug.Log("Menu is Opened");
    }

    public void Open(int index)
    {
        SubMenus[index].Open();
    }

    public void AddMenu(JobSystem J, BattleMIface M)
    {
        if (Menus == null)
        {
            Initlaize();
        }

        if (!Menus.ContainsKey(J))
        {
            Menus.Add(J, M);
        }
    }

    public void AddSubMenu(int I, BattleMIface M)
    {
        SubMenus.Add(I, M);
    }

    public void DrawStats(List<Creature> Battlers)
    {
        if (!Init)
        {
            PlayerStatSlot1 = GameObject.Find("Stat1");
            PlayerStatSlot2 = GameObject.Find("Stat2");
            PlayerStatSlot3 = GameObject.Find("Stat3");
        }

        PlayerStatSlot1.GetComponent<TextMeshProUGUI>().SetText(Battlers[0].CreatureName + ':' + " Health: " + Battlers[0].Stats.StatList[(int)StatType.HEALTH].Stat.ToString());

        PlayerStatSlot2.GetComponent<TextMeshProUGUI>().SetText(Battlers[1].CreatureName + ':' + " Health: " + Battlers[1].Stats.StatList[(int)StatType.HEALTH].Stat.ToString());

        //PlayerStatSlot3.GetComponent<TextMeshProUGUI>().SetText(Battlers[2].CreatureName + ':' + " Health: " + Battlers[2].Stats.StatList[(int)StatType.HEALTH].Stat.ToString());
    }

    public void CreateTargetArrow()
    {
        SelectionArrow = new GameObject("Arrow");
        SelectionArrow.AddComponent<RectTransform>();
        SelectionArrow.AddComponent<SpriteRenderer>();

        SelectionArrow.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Triangle");
        SelectionArrow.GetComponent<RectTransform>().sizeDelta = new Vector2(5, 10);
        SelectionArrow.transform.SetParent(BadParty[Index].Battler.transform);
        SelectionArrow.transform.localPosition = new Vector2(-100, 0);

    }

    public void ResetInput()
    {
        InputData.Clear();
    }

    public IEnumerator Target(SkillData SkillToEnqueue)
    {
        // Select a Target for an attack in here?
        // Implicitably we have an enqueued skill that were trying to 
        // Aim or rather are currently enqueuing? don't forget amount of targets

        SkillToTarget = SkillToEnqueue;

        //Debug.Log("Targeting");

        Inputs CurrentInput = new Inputs();
        if (SelectionArrow == null)
        {
            CreateTargetArrow();
        }

        while (InputData.Count == 0)
        {
            yield return null;
        }

        if (InputData.Count > 0)
        {

            CurrentInput = (Inputs)InputData.Dequeue();

            switch (CurrentInput)
            {
                case Inputs.UP:
                    if (Index != BadParty.Count)
                    {
                        Index += 1;
                        SelectionArrow.transform.SetParent(BadParty[Index].Battler.transform);
                        Debug.Log("Bad Index: " + Index);
                    }
                    break;

                case Inputs.DOWN:
                    if (Index > 0)
                    {
                        Index -= 1;
                        SelectionArrow.transform.SetParent(BadParty[Index].Battler.transform);
                        Debug.Log("Bad Index: " + Index);
                    }
                    break;

                case Inputs.A:
                    SkillToEnqueue.Enqueue(Opened, BadParty[Index]);
                    SkillToEnqueue = null;
                    SkillToTarget = null;
                    CurrentInput = Inputs.NULL;
                    Close();

                    Debug.Log("A press");
                    break;
            }
        }

        yield return Index;

    }


    public void Close()
    {
        InputData.Clear();
        Targeting = false;
        CommandSelected = true;
        StopAllCoroutines();

        for (int i = 0; i < Widgets.Count; i++)
        {
            Widgets[i] = null;
        }

        Widgets.Clear();
        Destroy(Commands);
        Destroy(Panel);

        IsOpened = false;

        Debug.Log("Menu Closed");

    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("IsOpened: " + IsOpened);
        if (IsOpened)
        {
            if (Widgets.Count == 0)
            {
                Debug.Log("No Widget");
            }

            if (!Targeting)
            {
                if (InputData.Count > 0)
                {
                    StartCoroutine(DetectInput());
                }
            }

            else if (Targeting)
            {
                if (InputData.Count > 0)
                {
                    StartCoroutine(Target(SkillToTarget));
                }
            }
        }
    }
}