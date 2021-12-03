using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerTurn
{
    Player playerID;
}

public class BattlePlayers : MonoBehaviour
{
    Party Players;
    public List<Player> battleParty;
    CharacterInfo Temp;

    // Selected Player data to grab stats
    public delegate bool Turn(Player p);
    public event Turn playerTurnEvent;

    public delegate ActionIface Action(Player p, ActionIface a);
    public event Action actionSelected;

    PlayerTurn turn;

    bool isInitalized = false;

    int killCount = 0;
    int i = 0;

    List<Creature> BadParty;

    GameObject BattleObject;

    public void Init(GameObject BattleO)
    {
        Players = FindObjectOfType<Party>();
        battleParty = new List<Player>();
        turn = new PlayerTurn();

        // Grab the top 3 members of the party
        for (int j = 0; j < 2; j++)
        {
            DontDestroyOnLoad(Players.PartyMembers[j].prefab);
            Players.PartyMembers[j].prefab.SetActive(true);
            Players.PartyMembers[j].Data.state = BattleState.WAIT;
            Players.PartyMembers[j].targeter = new Target();
            battleParty.Add(Players.PartyMembers[j]);
        }

        BattleObject = BattleO;
        isInitalized = true;
    }

    public GameObject CreateCharacterById(int ID)
    {
        return (GameObject)Instantiate(battleParty[ID].prefab);
    }

    // In here goes the logic for Battle! If God is love then you can call me cupid! ooh rah
    // Calculate the magical equations for Attack any status effects etc here. 
    public ActionIface Battle(int i)
    {
        ActionIface action = new ActionIface();
        switch (battleParty[i].Data.state)
        {
            case BattleState.WAIT:
                //battleParty[i].prefab.GetComponentInChildren<Animator>().SetBool("Is_Idle", true);
                battleParty[i].prefab.GetComponent<Gauge>().fill(battleParty[i].Data.Stats.statList[(int)StatType.SPEED].stat);

                if (battleParty[i].prefab.GetComponent<Gauge>().getFilled())
                {
                    battleParty[i].Data.state = BattleState.COMMAND;
                }
                break;
            case BattleState.COMMAND:
                // Use a delegate in here! Send event out that player is ready to act and menu should open from there
                playerTurnEvent.Invoke(battleParty[i]);
                break;
            case BattleState.SELECTION:
                // This just don't go here
                //actionSelected.Invoke(battleParty[i], action);
                break;
            case BattleState.ACTION:
                break;
        }
        return action;
    }
    public void Reset(int i)
    {
        if (battleParty[i].prefab.GetComponent<Gauge>().getFilled() && battleParty[i].Data.state == BattleState.WAIT)
        {
            battleParty[i].prefab.GetComponent<Gauge>().Reset();
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
