using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class commandMenus : MonoBehaviour
{

    //Player Menus
    SortedDictionary<JobSystem, BattleMIface> Menus;

    BattleMIface cMenu;

    // Action Menus (Item, Skills, Magic)
    SortedDictionary<int, BattleMIface> SubMenus;

    MenuControls controls;

    List<GameObject> widgets;
    GameObject selectedWidget;

    Gauge gauge;

    int widgetIndex = 0;
    int index = 0;

    Creature opened;

    GameObject Commands;
    GameObject PlayerStatus;
    GameObject Panel;

    GameObject PlayerStatSlot1;
    GameObject PlayerStatSlot2;
    GameObject PlayerStatSlot3;

    GameObject selectionArrow;
    GameObject arrow;

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

    public bool Open(Creature opener)
    {
        if (!isOpened)
        {
            Commands = Instantiate(Commands);
            Commands.GetComponentInChildren<Image>().enabled = true;
            cMenu = Menus[opener.Job];
            if (!cMenu)
            {
                return isOpened = false;
            }
            Debug.Log("Menu is Opened");
            return isOpened = true;
        }
        // Assume it was "already opened" in error.
        return false;
    }

    public void Open(int index)
    {
        SubMenus[index].Open();
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

            // Process input in here

        }
    }
}