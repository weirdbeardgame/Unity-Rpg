using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[System.Serializable]
public class CharacterInfo
{
    public int ID;
    public GameObject prefab;
    public Creature Player;
    public CommandQueue playerQueue;

    public void init(Creature c, GameObject pre, int i)
    {
        ID = i;
        prefab = pre;
        prefab.SetActive(true);
        Player = c;
    }

    public void enque(ActionIface i)
    {
        playerQueue.enqueue(i);
    }

    public ActionIface deque()
    {
        return playerQueue.dequeue();
    }
}

public class BattlePlayers : MonoBehaviour
{
    Party Players;
    public Dictionary<int, CharacterInfo> battleParty;
    CharacterInfo Temp;

    public bool isInitalized = false;

    int killCount = 0;
    int i = 0;

    List<Baddies> BadParty;

    commandMenus Menu;

    GameObject BattleObject;

    public Dictionary<int, CharacterInfo> Initialize(GameObject BattleO, List<Baddies> Bad, int i)
    {
        if (!isInitalized)
        {
            Players = FindObjectOfType<Party>();
            battleParty = new Dictionary<int, CharacterInfo>();
            // Grab the top 3 members of the party
            for (int j = 0; j < 2; j++)
            {
                CharacterInfo c = new CharacterInfo();
                c.playerQueue = new CommandQueue();
                c.init(Players.PartyMembers[j].Data, Players.PartyMembers[j].prefab, j);
                battleParty.Add(j, c);

                // allCharacters[j].playerQueue = FindObjectOfType<CommandQueue>(); // Incorrect! 
                // Each player could have their own instance! Not look for the one in scene. The point is the players and baddies are each running their own logic.
                // DontDestroyOnLoad(battleParty[j].Prefab);
            }

            BattleObject = BattleO;
            isInitalized = true;
            Menu = FindObjectOfType<commandMenus>();
        }
        return battleParty;
    }

    public GameObject CreateCharacterById(int ID)
    {
        return (GameObject)Instantiate(battleParty[ID].prefab);
    }

    public void OpenWindow(int i)
    {
        if (battleParty[i].prefab.GetComponent<Gauge>().getFilled())
        {
            if (battleParty[i].Player.state == BattleState.WAIT)
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
        switch (battleParty[i].Player.state)
        {
            case BattleState.WAIT:
                battleParty[i].prefab.GetComponentInChildren<Animator>().SetBool("Is_Idle", true);
                battleParty[i].prefab.GetComponent<Gauge>().fill(battleParty[i].Player.Stats.statList[(int)StatType.SPEED].stat);
                if (battleParty[i].prefab.GetComponent<Gauge>().getFilled())
                {
                    battleParty[i].Player.state = BattleState.COMMAND;
                }
                break;
            case BattleState.COMMAND:

                if (!Menu && BadParty != null)
                {
                    Menu.Open(battleParty[i].Player);
                }
                else
                {
                    return;
                }

                if (Menu.CommandSelected)
                {
                    battleParty[i].Player.state = BattleState.SELECTION;
                }
                break;
            case BattleState.SELECTION:
                Target(BadParty);
                break;
            case BattleState.ACTION:

                Menu.Close();
                if (battleParty[i].playerQueue.Peek() != null && battleParty[i].playerQueue.Peek().caster.tag == BattleTag.PLAYER)
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
                }
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
        if (battleParty[i].prefab.GetComponent<Gauge>().getFilled() && battleParty[i].Player.state == BattleState.WAIT)
        {
            battleParty[i].prefab.GetComponent<Gauge>().Reset();
            Menu.CommandSelected = false;
        }
    }

    public Creature GetPlayer(int i)
    {
        return battleParty[i].Player;
    }

    public void CheckHealth(int i)
    {
        // In here goeth the holy calls for Death upon the characters which can behave differently depending on circumstance.
        if (battleParty[i].Player.Stats.statList[(int)StatType.HEALTH].stat <= 0)
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
