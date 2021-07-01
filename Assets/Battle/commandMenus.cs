using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class commandMenus : MonoBehaviour, IReceiver
{

    //Player Menus
    SortedDictionary<JobSystem, BattleMIface> Menus;

    GameObject cMenu;

    // Action Menus (Item, Skills, Magic)
    SortedDictionary<int, BattleMIface> SubMenus;

    MenuControls controls;

    List<GameObject> widgets;
    GameObject selectedWidget;

    Gauge gauge;

    int widgetIndex = 0;
    int index = 0;

    Creature Opened;

    GameObject Commands;
    GameObject PlayerStatus;
    GameObject Panel;

    GameObject PlayerStatSlot1;
    GameObject PlayerStatSlot2;
    GameObject PlayerStatSlot3;

    GameObject selectionArrow;
    GameObject arrow;

    List<Baddies> BadParty;

    bool init;
    bool isOpened;
    // The issue with this is the enemy targeting system is in here instead of directly attached to the battle system like it should be.
    // This should be a state the characters can be in ABS style!
    bool targeting;
    bool isSubscribed;
    public bool CommandSelected;

    Messaging message;

    JobSystem Jobs;

    SkillData SkillToTarget;

    // Start is called before the first frame update
    void Start()
    {
        PlayerStatus = GameObject.Find("menu");
        Commands = GameObject.Find("Command Screen");
        Commands.GetComponent<Image>().enabled = false;

        controls = GetComponent<MenuControls>();

        // Widgets = new List<Widget>();
        message = FindObjectOfType<Messaging>();
        Menus = new SortedDictionary<JobSystem, BattleMIface>();
        SubMenus = new SortedDictionary<int, BattleMIface>();
    }

    void IncreaseIndex()
    {
        //This lasts a frame you fool! Thunder Cross Splito Attack!
        if (widgetIndex <= widgets.Count)
        {
            widgetIndex += 1;
            arrow.transform.SetParent(widgets[widgetIndex].transform);

        }

        else
        {
            return;
        }
    }

    public void CreateArrow()
    {
        arrow = new GameObject("Arrow");
        arrow.AddComponent<RectTransform>();
        arrow.AddComponent<Image>();

        arrow.GetComponent<Image>().sprite = Resources.Load<Sprite>("Triangle");
        arrow.GetComponent<RectTransform>().sizeDelta = new Vector2(5, 10);
        arrow.transform.SetParent(widgets[widgetIndex].transform);
        arrow.transform.localPosition = new Vector2(-100, 0);
    }

    void DecreaseIndex()
    {
        if (widgetIndex > 0)
        {
            widgetIndex -= 1;
            arrow.transform.SetParent(widgets[widgetIndex].transform);
            return;
        }
    }

    public void Initlaize()
    {
        PlayerStatus = GameObject.Find("menu");
        message = FindObjectOfType<Messaging>();

        Subscribe();

        widgets = new List<GameObject>();
        Menus = new SortedDictionary<JobSystem, BattleMIface>();
        SubMenus = new SortedDictionary<int, BattleMIface>();
    }

    public void Move(Vector2 position)
    {
        Debug.Log("POS: " + position);
        selectionArrow.transform.Translate(position);
        widgetIndex += (int)Mathf.Sign(position.y);
        // Grab each widget's position and go from there.
        selectedWidget = widgets[widgetIndex];
        Debug.Log("Widget Name: " + selectedWidget.GetComponent<Widget>().name);
    }


    public void SetIsOpened(bool open)
    {
        isOpened = open;
    }

    public void Subscribe()
    {
        isSubscribed = true;
    }

    public void Unsubscribe()
    {
        isSubscribed = false;
    }

    public void Receive(object message)
    {
    }

    public void AddWidget(GameObject w)
    {
        int y = 0;

        Panel = Commands.transform.GetChild(0).gameObject;

        if (widgets == null)
        {
            widgets = new List<GameObject>();
        }

        w.transform.SetParent(Panel.transform);
        w.transform.localPosition = new Vector2(0, y);
        widgets.Add(w);

        y -= 5;
    }

    public void Open(Creature Opener, List<Baddies> Villan)
    {
        if (!isOpened)
        {
            Commands = (GameObject)Instantiate(Resources.Load("BattlePrefabs/BattleMenu"));
            Commands.GetComponentInChildren<Image>().enabled = true;
            Opened = Opener;
            //Menus[Opened.Job].Open(Opened);
            BadParty = Villan;
            // I need to add error checking in here
            isOpened = true;
        }

        Debug.Log("Menu is Opened");
    }

    public void Open(int index)
    {
        //SubMenus[index].Open();
    }

    public void AddMenu(JobSystem j, BattleMIface m)
    {
        if (Menus == null)
        {
            Initlaize();
        }

        if (!Menus.ContainsKey(j))
        {
            Menus.Add(j, m);
        }
    }

    public void AddSubMenu(int i, BattleMIface m)
    {
        SubMenus.Add(i, m);
    }

    public void DrawStats(List<CharacterInfo> Battlers)
    {
        if (!init)
        {
            PlayerStatSlot1 = GameObject.Find("Stat1");
            PlayerStatSlot2 = GameObject.Find("Stat2");
            PlayerStatSlot3 = GameObject.Find("Stat3");
        }

        PlayerStatSlot1.GetComponent<TextMeshProUGUI>().SetText(Battlers[0].Player.CreatureName + ':' + " Health: " + Battlers[0].Player.Stats.StatList[(int)StatType.HEALTH].Stat.ToString());
        PlayerStatSlot2.GetComponent<TextMeshProUGUI>().SetText(Battlers[1].Player.CreatureName + ':' + " Health: " + Battlers[1].Player.Stats.StatList[(int)StatType.HEALTH].Stat.ToString());
        //PlayerStatSlot3.GetComponent<TextMeshProUGUI>().SetText(Battlers[2].CreatureName + ':' + " Health: " + Battlers[2].Stats.StatList[(int)StatType.HEALTH].Stat.ToString());
    }

    public void CreateTargetArrow()
    {
        selectionArrow = new GameObject("Arrow");
        selectionArrow.AddComponent<RectTransform>();
        selectionArrow.AddComponent<SpriteRenderer>();

        selectionArrow.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Triangle");
        selectionArrow.GetComponent<RectTransform>().sizeDelta = new Vector2(5, 10);
        selectionArrow.transform.SetParent(BadParty[index].Battler.transform);
        selectionArrow.transform.localPosition = new Vector2(-100, 0);

    }

    public void Target(SkillData SkillToEnqueue)
    {
        // Select a Target for an attack in here?
        // Implicitably we have an enqueued skill that were trying to 
        // Aim or rather are currently enqueuing? don't forget amount of targets

        SkillToTarget = SkillToEnqueue;

        //Debug.Log("Targeting");

        if (selectionArrow == null)
        {
            CreateTargetArrow();
        }
    }


    public void Close()
    {
        targeting = false;
        CommandSelected = true;
        StopAllCoroutines();

        for (int i = 0; i < widgets.Count; i++)
        {
            widgets[i] = null;
        }

        widgets.Clear();
        Destroy(Commands);
        Destroy(Panel);

        isOpened = false;
        Debug.Log("Menu Closed");
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("IsOpened: " + IsOpened);
        if (isOpened)
        {
            if (widgets.Count == 0)
            {
                Debug.Log("No Widget");
            }

            if (!targeting)
            {

            }

            else if (targeting)
            {

            }
        }
    }
}