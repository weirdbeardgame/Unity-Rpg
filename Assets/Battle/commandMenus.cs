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

    Gauge gauge;

    int widgetIndex = 0;
    int index = 0;
    Creature opened;

    [SerializeField]
    GameObject Commands;
 
    [SerializeField]
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

    JobSystem Jobs;

    SkillData SkillToTarget;

    // Start is called before the first frame update
    void Start()
    {
        PlayerStatus = GameObject.Find("menu");
        Commands = GameObject.Find("Command Screen");
        Commands.GetComponent<Image>().enabled = false;

        // Widgets = new List<Widget>();
        Menus = new SortedDictionary<JobSystem, BattleMIface>();
        SubMenus = new SortedDictionary<int, BattleMIface>();
    }

    public void CreateArrow()
    {
        arrow = new GameObject("Arrow");
        arrow.AddComponent<RectTransform>();
        arrow.AddComponent<Image>();

        arrow.GetComponent<Image>().sprite = Resources.Load<Sprite>("Triangle");
        arrow.GetComponent<RectTransform>().sizeDelta = new Vector2(5, 10);
    }

    public void Initlaize()
    {
        Menus = new SortedDictionary<JobSystem, BattleMIface>();
        SubMenus = new SortedDictionary<int, BattleMIface>();
    }

    public void Move(Vector2 position)
    {
        Debug.Log("POS: " + position);
        selectionArrow.transform.Translate(position);
    }

    public bool Open(Creature opener)
    {
        if (!isOpened)
        {
            Commands = Instantiate(Commands);
            Commands.GetComponentInChildren<Image>().enabled = true;
            cMenu = Menus[opener.job];
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

    public void DrawStats(Dictionary<int, Player> Battlers)
    {
        // ToDo. ReWrite with Depenency injection more in mind. Rather then grabbing the stat from the character. I should be handing it the stat
        PlayerStatSlot1.GetComponent<TextMeshProUGUI>().SetText(Battlers[0].Data.creatureName + ':' + " Health: " + Battlers[0].Data.Stats.statList[(int)StatType.HEALTH].stat.ToString());
        PlayerStatSlot2.GetComponent<TextMeshProUGUI>().SetText(Battlers[1].Data.creatureName + ':' + " Health: " + Battlers[1].Data.Stats.statList[(int)StatType.HEALTH].stat.ToString());
        //PlayerStatSlot3.GetComponent<TextMeshProUGUI>().SetText(Battlers[2].creatureName + ':' + " Health: " + Battlers[2].Stats.StatList[(int)StatType.HEALTH].Stat.ToString());
    }

    public void Close()
    {
        targeting = false;
        CommandSelected = true;
        StopAllCoroutines();

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
            // Let Unity process UI events in here
        }
    }
}