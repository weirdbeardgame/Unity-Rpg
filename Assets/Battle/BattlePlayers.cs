using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

using menu;

[System.Serializable]
public class CharacterInfo
{
    public int ID;
    public GameObject Prefab;
    public Creature Player;
    public CommandQueue playerQueue;

    public void init(Creature c, GameObject pre, int i)
    {
        ID = i;
        Prefab = pre;
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

    int maxIndex = 0;
    int killCount = 0;
    int i = 0;

    List<Baddies> BadParty;

    commandMenus Menu;

    GameObject BattleObject;

    public Dictionary<int, CharacterInfo> Initialize(GameObject playerPrefab, GameObject BattleO, List<Baddies> Bad, int i)
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
                c.init(Players.PartyMembers[j], playerPrefab, j);
                battleParty.Add(j, c);

                // allCharacters[j].playerQueue = FindObjectOfType<CommandQueue>(); Incorrect! 
                // Each player could have their own instance! Not look for the one in scene
//                DontDestroyOnLoad(battleParty[j].Prefab);
            }

            BattleObject = BattleO;
            isInitalized = true;
            Menu = FindObjectOfType<commandMenus>();
        }
        return battleParty;
    }
    // In here goes the logic for Battle! If God is love then you can call me cupid! ooh rah
    // Calculate the magical equations for Attack any status effects etc here. 

    public GameObject CreateCharacterById(int ID)
    {
        return (GameObject)Instantiate(battleParty[ID].Prefab);
    }

    public void OpenWindow(int i)
    {
        if (battleParty[i].Prefab.GetComponent<Gauge>().getFilled())
        {
            if (battleParty[i].Player.State == BattleState.WAIT)
            {
                battleParty[i].Player.State = BattleState.COMMAND;
            }

            else
            {
                return;
            }
        }
    }

    public void Battle(int i)
    {
        switch (battleParty[i].Player.State)
        {

            case BattleState.WAIT:
                battleParty[i].Prefab.GetComponentInChildren<Animator>().SetBool("Is_Idle", true);
                battleParty[i].Prefab.GetComponent<Gauge>().fill(battleParty[i].Player.Stats.StatList[(int)StatType.SPEED].Stat);
                if (battleParty[i].Prefab.GetComponent<Gauge>().getFilled())
                {
                    battleParty[i].Player.State = BattleState.COMMAND;
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
                    battleParty[i].Player.State = BattleState.SELECTION;
                }
                break;
            case BattleState.SELECTION:
                Target(BadParty);
                break;
            case BattleState.ACTION:

                Menu.Close();
                if (battleParty[i].playerQueue.Peek() != null && battleParty[i].playerQueue.Peek().caster.Tag == BattleTag.PLAYER)
                {
                    ActionIface Temp = battleParty[i].deque();
                    battleParty[i].Prefab.GetComponentInChildren<Animator>().SetBool("Is_Attack", true);
                    Temp.Execute();
                    BattleObject.GetComponent<DamageRecieved>().Create(Temp.target.Battler.transform.localPosition, Temp.target.ActualDamage);
                }

                if (battleParty[i].playerQueue.Peek() == null)
                {
                    battleParty[i].Player.State = BattleState.WAIT;
                    battleParty[i].Prefab.GetComponentInChildren<Animator>().SetBool("Is_Idle", true);
                    Reset(i);
                }
                break;
        }
    }

    Baddies Target(List<Baddies> targets)
    {
        // Assume we have an enqueued skill that needs to be constructed    // This is suppoosed to cover negative clause. That's not what it does tho
        if ((i += ((int)Input.GetAxisRaw("Horizontal"))) < targets.Count || (i += ((int)Input.GetAxisRaw("Horizontal"))) > targets.Count)
        {
            return targets[i];
        }
        return null;
    }

    public void Reset(int i)
    {
        if (battleParty[i].Prefab.GetComponent<Gauge>().getFilled() && battleParty[i].Player.State == BattleState.WAIT)
        {
            battleParty[i].Prefab.GetComponent<Gauge>().Reset();
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
        if (battleParty[i].Player.Stats.StatList[(int)StatType.HEALTH].Stat <= 0)
        {
            killCount += 1;
            Debug.Log("Kill Count:" + killCount);
        }
    }

    public bool checkDeath()
    {
        if (killCount == maxIndex)
        {
            return true;
        }

        else
        {
            return false;
        }
    }
}
