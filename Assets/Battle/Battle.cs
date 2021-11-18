using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;

public enum CommandType { SKILL, ITEM };
public enum BattleStateM { START, ACTIVE, END };

public class Battle : MonoBehaviour
{
    BattlePlayers Players;
    BattleEnemies enemies;
    BattleSlots slots;
    CommandQueue Queue;
    GameObject BattleObject;
    GameObject SelectionArrow;
    SceneInfo SceneToReturnTo;
    Creature Caster;
    Creature Receiver;
    Animator BattleAnimations;
    int Index = 0;
    int PlayerIndex;
    int x, y = 3;
    int EnemyKilled;
    int PlayerKilled;
    BattleStateM state = BattleStateM.START;

    public void StartBattle(SceneInfo PreviousScene, GameObject bObject)
    {
        // HAX
        state = BattleStateM.START;
        if (state == BattleStateM.START)
        {
            SceneToReturnTo = PreviousScene;
            BattleObject = bObject;

            slots = BattleObject.GetComponent<BattleSlots>();

            Queue = BattleObject.GetComponent<CommandQueue>();
            Players = BattleObject.GetComponent<BattlePlayers>();
            enemies = BattleObject.GetComponent<BattleEnemies>();
            BattleAnimations = BattleObject.GetComponent<Animator>();

            //Players.Init(BattleObject, enemies.BadParty);

            for (int i = 0; i < BattleObject.GetComponent<BattleEnemies>().BadParty.Count; i++)
            {
                slots.createSlots(SlotPosition.FRONT, BattleTag.ENEMY, enemies.BadParty[i].Data, i);
                x += 1;
            }

            for (int i = 0; i < 2; i++)
            {
                slots.createSlots(SlotPosition.FRONT, BattleTag.PLAYER, Players.GetPlayer(i), i);
            }
            state = BattleStateM.ACTIVE;
        }
    }

    IEnumerator ChangeScene()
    {
        AsyncOperation unload = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        while (!unload.isDone)
        {
            Destroy(BattleObject);
            Destroy(Players);
            yield return null;
        }

        AsyncOperation async = SceneManager.LoadSceneAsync(SceneToReturnTo.sceneName);

        while (!async.isDone)
        {
            yield return null;
        }
    }

    void Target()
    {
        // Run skill targeter in here? Or should that belong to battle players given this is ABS styled
    }

    void Update()
    {
        switch (state)
        {
            case BattleStateM.ACTIVE:
                // Run Battle Logic in here
                for (int i = 0; i < Players.battleParty.Count; i++)
                {
                    Players.Battle(i);
                    enemies.Battle(i);
                    BattleObject.GetComponent<commandMenus>().DrawStats(Players.battleParty);
                    // Listen for death and action. Check for enemy or player death
                }
                break;

            case BattleStateM.END:
                EndBattle();
                break;
        }

    }


    public void EndBattle()
    {
        // Add Exp, Items and Levels. Show screen to do so
    }

}
