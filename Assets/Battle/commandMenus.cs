using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class commandMenus : MonoBehaviour
{
    int index = 0;
    Player opener;
    bool isOpened;

    BattleMIface cMenu;

    // Action Menus (Item, Skills, Magic)
    SortedDictionary<int, BattleMIface> SubMenus;
    SortedDictionary<JobSystem, BattleMIface> Menus;

    GameObject prefab;

    [SerializeField]
    GameObject commandMenu;

    List<GameObject> playerStats;

    [SerializeField]
    BattlePlayers turn;

    // Start is called before the first frame update
    void Start()
    {
        // Dis happen too fast bitch!
    }

    // Hax
    public void Init()
    {
        turn = FindObjectOfType<BattlePlayers>();
        turn.playerTurnEvent += (opener) => { return Open(opener); };
        Menus = new SortedDictionary<JobSystem, BattleMIface>();
        SubMenus = new SortedDictionary<int, BattleMIface>();
    }


    public bool Open(Player opener)
    {
        if (!isOpened)
        {
            // This is Null on Open because there's not an assigned GameObject in the scene!
            // For now I stick with a list of prefab?
            // Think about, https://www.youtube.com/watch?v=4fkTbbxktpc
            commandMenu = Instantiate(prefab);
            commandMenu.GetComponentInChildren<Image>().enabled = true;
            cMenu = Menus[opener.Data.job];
            if (!cMenu)
            {
                return isOpened = false;
            }

            Debug.Log(opener.Data.creatureName + " opened the menu");
            return isOpened = true;
        }
        // Assume it was "already opened" in error.
        return false;
    }

    public void DrawStats(Dictionary<int, Player> Battlers)
    {
        for (int i = 0; i < Battlers.Count; i++)
        {
            if (playerStats.Count < Battlers.Count)
            {
                playerStats.Add(prefab);
            }
            playerStats[i].GetComponent<TextMeshProUGUI>().SetText(Battlers[i].Data.creatureName + ':' + " Health: " + Battlers[0].Data.Stats.statList[(int)StatType.HEALTH].stat.ToString());
        }
        // ToDo. ReWrite with Depenency injection more in mind. Rather then grabbing the stat from the character. I should be handing it the stat
        //PlayerStatSlot1.GetComponent<TextMeshProUGUI>().SetText(Battlers[0].Data.creatureName + ':' + " Health: " + Battlers[0].Data.Stats.statList[(int)StatType.HEALTH].stat.ToString());
        //PlayerStatSlot2.GetComponent<TextMeshProUGUI>().SetText(Battlers[1].Data.creatureName + ':' + " Health: " + Battlers[1].Data.Stats.statList[(int)StatType.HEALTH].stat.ToString());
        //PlayerStatSlot3.GetComponent<TextMeshProUGUI>().SetText(Battlers[2].creatureName + ':' + " Health: " + Battlers[2].Stats.StatList[(int)StatType.HEALTH].Stat.ToString());
    }

    public void Close()
    {
        StopAllCoroutines();

        Destroy(commandMenu);

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