using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class BattlePlayers : MonoBehaviour
{
    Party Players;
    public Dictionary<int, Player> battleParty;
    CharacterInfo Temp;

    bool isInitalized = false;

    int killCount = 0;
    int i = 0;

    List<Creature> BadParty;

    commandMenus Menu;

    GameObject BattleObject;

    public void Init(GameObject BattleO)
    {
        Players = FindObjectOfType<Party>();
        battleParty = new Dictionary<int, Player>();

        // Grab the top 3 members of the party
        for (int j = 0; j < 2; j++)
        {
            DontDestroyOnLoad(Players.PartyMembers[j].prefab);
            Players.PartyMembers[j].prefab.SetActive(true);
            battleParty.Add(j, Players.PartyMembers[j]);
        }

        BattleObject = BattleO;
        isInitalized = true;
        Menu = FindObjectOfType<commandMenus>();
    }

    public GameObject CreateCharacterById(int ID)
    {
        return (GameObject)Instantiate(battleParty[ID].prefab);
    }

    public void OpenWindow(int i)
    {
        if (battleParty[i].prefab.GetComponent<Gauge>().getFilled())
        {
            if (battleParty[i].Data.state == BattleState.WAIT)
            {
                battleParty[i].Data.state = BattleState.COMMAND;
            }

            else
            {
                return;
            }
        }
    }

    // In here goes the logic for Battle! If God is love then you can call me cupid! ooh rah
    // Calculate the magical equations for Attack any status effects etc here. 
    public ActionIface Battle(int i)
    {
        ActionIface action = new ActionIface();
        switch (battleParty[i].Data.state)
        {
            case BattleState.WAIT:

                battleParty[i].prefab.GetComponentInChildren<Animator>().SetBool("Is_Idle", true);
                battleParty[i].prefab.GetComponent<Gauge>().fill(battleParty[i].Data.Stats.statList[(int)StatType.SPEED].stat);

                if (battleParty[i].prefab.GetComponent<Gauge>().getFilled())
                {
                    battleParty[i].Data.state = BattleState.COMMAND;
                }
                break;
            case BattleState.COMMAND:
                // Use a delegate in here! Send event out that player is ready to act and menu should open from there

                /*****************************************************
                * if (!Menu && BadParty != null)
                * {
                *    Menu.Open(battleParty[i].Data);
                * }

                * if (Menu.CommandSelected)
                * {
                *     battleParty[i].Data.state = BattleState.SELECTION;
                * }
                *******************************************************/
                break;
            case BattleState.SELECTION:
                Target(BadParty, action);
                break;
            case BattleState.ACTION:
                Menu.Close();
                break;
        }
        return action;
    }

    // Just make it generic for now
    void Target(List<Creature> targets, ActionIface action)
    {
        // Assume we have an enqueued skill that needs to be constructed 
        if ((i += ((int)Input.GetAxisRaw("Horizontal"))) < targets.Count || (i += ((int)Input.GetAxisRaw("Horizontal"))) > targets.Count)
        {
            action.target = targets[i];
        }
        return;
    }

    public void Reset(int i)
    {
        if (battleParty[i].prefab.GetComponent<Gauge>().getFilled() && battleParty[i].Data.state == BattleState.WAIT)
        {
            battleParty[i].prefab.GetComponent<Gauge>().Reset();
            Menu.CommandSelected = false;
        }
    }

    public Creature GetPlayer(int i)
    {
        return battleParty[i].Data;
    }

    public void CheckHealth(int i)
    {
        // In here goeth the holy calls for Death upon the characters which can behave differently depending on circumstance.
        if (battleParty[i].Data.Stats.statList[(int)StatType.HEALTH].stat <= 0)
        {
            killCount += 1;
            Debug.Log("Kill Count:" + killCount);
        }
    }

    public bool checkDeath()
    {
        if (killCount == battleParty.Count)
        {
            return true;
        }

        else
        {
            return false;
        }
    }
}
