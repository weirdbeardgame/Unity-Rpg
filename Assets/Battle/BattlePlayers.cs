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

    List<Baddies> BadParty;

    commandMenus Menu;

    GameObject BattleObject;

    public void Init(GameObject BattleO, List<Baddies> Bad)
    {
        Players = FindObjectOfType<Party>();
        battleParty = new Dictionary<int, Player>();
        // Grab the top 3 members of the party
        for (int j = 0; j < 2; j++)
        {

            //c.playerQueue = new CommandQueue();
            //c.init(Players.PartyMembers[j].Data, Players.PartyMembers[j].prefab, j);
            DontDestroyOnLoad(Players.PartyMembers[j].prefab);
            Players.PartyMembers[j].prefab.SetActive(true);
            battleParty.Add(j, Players.PartyMembers[j]);

            // allCharacters[j].playerQueue = FindObjectOfType<CommandQueue>(); // Incorrect! 
            // Each player could have their own instance! Not look for the one in scene. The point is the players and baddies are each running their own logic.
            // DontDestroyOnLoad(battleParty[j].Prefab);
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
                //battleParty[i].Player.state = BattleState.COMMAND;
            }

            else
            {
                return;
            }
        }
    }

    // In here goes the logic for Battle! If God is love then you can call me cupid! ooh rah
    // Calculate the magical equations for Attack any status effects etc here. 
    public void Battle(int i)
    {
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

                if (!Menu && BadParty != null)
                {
                    Menu.Open(battleParty[i].Data);
                }
                else
                {
                    return;
                }

                if (Menu.CommandSelected)
                {
                    battleParty[i].Data.state = BattleState.SELECTION;
                }
                break;
            case BattleState.SELECTION:
                Target(BadParty);
                break;
            case BattleState.ACTION:

                Menu.Close();
                // Should this be handled by the player? Or the battle system itself? I'm working on an ABS system
                /*if (battleParty[i].playerQueue.Peek() != null && battleParty[i].playerQueue.Peek().caster.tag == BattleTag.PLAYER)
                {
                    ActionIface action = battleParty[i].deque();
                    battleParty[i].prefab.GetComponentInChildren<Animator>().SetBool("Is_Attack", true);
                    action.Execute();
                    BattleObject.GetComponent<DamageRecieved>().Create(battleParty[i].prefab, action.target.actualDamage);
                }

                if (battleParty[i].playerQueue.Peek() == null)
                {
                    //battleParty[i].Player.state = BattleState.WAIT;
                    battleParty[i].prefab.GetComponentInChildren<Animator>().SetBool("Is_Idle", true);
                    Reset(i);
                }*/
                break;
        }
    }

    Baddies Target(List<Baddies> targets)
    {
        // Assume we have an enqueued skill that needs to be constructed 
        if ((i += ((int)Input.GetAxisRaw("Horizontal"))) < targets.Count || (i += ((int)Input.GetAxisRaw("Horizontal"))) > targets.Count)
        {
            return targets[i];
        }
        return null;
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
        if (battleParty == null)
        {
            Init(BattleObject, BadParty);
        }
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
