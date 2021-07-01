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
}

public class BattlePlayers : MonoBehaviour
{
    Party Players;
    public List<CharacterInfo> AllCharacters;

    public Dictionary<int, CharacterInfo> BattleParty;

    CharacterInfo Temp;


    public bool isInitalized = false;

    int maxIndex = 0;
    int killCount = 0;

    List<Baddies> BadParty;

    CommandQueue Queue;
    commandMenus Menu;

    GameObject BattleObject;

    public Dictionary<int, CharacterInfo> Initialize(GameObject Battle, GameObject BattleO, List<Baddies> Bad, int i)
    {
        if (!isInitalized)
        {
            Players = FindObjectOfType<Party>();

            Queue = FindObjectOfType<CommandQueue>();

            BattleParty = new Dictionary<int, CharacterInfo>();

            BattleObject = BattleO;

            isInitalized = true;

            Menu = FindObjectOfType<commandMenus>();
        

        if (Players != null)
        {
            BadParty = Bad;

            AllCharacters[i].Player = Players.PartyMembers[i];

            BattleParty.Add(i, AllCharacters[i]);

            DontDestroyOnLoad(BattleParty[i].Prefab);
        }
}

        return BattleParty;

    }
    // In here goes the logic for Battle! If God is love then you can call me cupid! ooh rah
    // Calculate the magical equations for Attack any status effects etc here. 


    public GameObject CreateCharacterById(int ID)
    {
        return (GameObject)Instantiate(BattleParty[ID].Prefab);
    }

    public void OpenWindow(int i)
    {
        if (BattleParty[i].Prefab.GetComponent<Gauge>().getFilled())
        {
            if (BattleParty[i].Player.State == BattleState.WAIT)
            {
                BattleParty[i].Player.State = BattleState.COMMAND;
            }

            else
            {
                return;
            }
        }
    }

    public void Battle(int i)
    {
        switch (BattleParty[i].Player.State)
        {

            case BattleState.WAIT:
                BattleParty[i].Prefab.GetComponentInChildren<Animator>().SetBool("Is_Idle", true);
                BattleParty[i].Prefab.GetComponent<Gauge>().fill(BattleParty[i].Player.Stats.StatList[(int)StatType.SPEED].Stat);
                if (BattleParty[i].Prefab.GetComponent<Gauge>().getFilled())
                {
                    BattleParty[i].Player.State = BattleState.COMMAND;
                }
                break;

            case BattleState.COMMAND:

                if (!Menu && BadParty != null)
                {
                    //Menu.Open(BattleParty[i].Player, BadParty);
                }
                else
                {
                    return;
                }

                if (Menu.CommandSelected)
                {
                    BattleParty[i].Player.State = BattleState.ACTION;
                }
                break;

            case BattleState.ACTION:
                // Execute command
                Menu.Close();
                if (Queue.Peek() != null && Queue.Peek().GetCaster().Tag == BattleTag.PLAYER)
                {
                    ActionIface Temp = Queue.Dequeue();
                    BattleParty[i].Prefab.GetComponentInChildren<Animator>().SetBool("Is_Attack", true);
                    Temp.Execute();
                    BattleObject.GetComponent<DamageRecieved>().Create(Temp.GetTarget().Battler.transform.localPosition, Temp.GetTarget().ActualDamage);
                }

                if (Queue.Peek() == null)
                {
                    BattleParty[i].Player.State = BattleState.WAIT;
                    BattleParty[i].Prefab.GetComponentInChildren<Animator>().SetBool("Is_Idle", true);
                    Reset(i);
                }
                break;
        }
    }

    public void Reset(int i)
    {
        if (BattleParty[i].Prefab.GetComponent<Gauge>().getFilled() && BattleParty[i].Player.State == BattleState.WAIT)
        {
            BattleParty[i].Prefab.GetComponent<Gauge>().Reset();
            Menu.CommandSelected = false;
        }
    }

    public Creature GetPlayer(int i)
    {
        return BattleParty[i].Player;
    }

    public void CheckHealth(int i)
    {
        // In here goeth the holy calls for Death upon the characters which can behave differently depending on circumstance.
        if (BattleParty[i].Player.Stats.StatList[(int)StatType.HEALTH].Stat <= 0)
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
