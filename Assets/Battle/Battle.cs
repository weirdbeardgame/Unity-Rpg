﻿using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;

public enum BattleStateM { START, ACTIVE, END };

public enum BattleState { COMMAND, SELECTION, ACTION, WAIT };

public class Battle : MonoBehaviour
{
    BattlePlayers Players;
    BattleEnemies enemies;
    BattlerFloors slots;
    CommandQueue queue;
    commandMenus menus;

    GameObject BattleObject;
    GameObject SelectionArrow;

    SceneInfo SceneToReturnTo;

    Creature Caster;
    Creature Receiver;

    BattleStateM state = BattleStateM.START;

    public void StartBattle(SceneInfo PreviousScene, GameObject bObject)
    {
        // HAX Section
        state = BattleStateM.START;
        menus = FindObjectOfType<commandMenus>();

        if (state == BattleStateM.START)
        {
            SceneToReturnTo = PreviousScene;
            BattleObject = bObject;

            queue = BattleObject.GetComponent<CommandQueue>();
            slots = FindObjectOfType<BattlerFloors>();

            Players = BattleObject.GetComponent<BattlePlayers>();
            enemies = BattleObject.GetComponent<BattleEnemies>();

            for (int i = 0; i < BattleObject.GetComponent<BattleEnemies>().BadParty.Count; i++)
            {
                slots.createSlots(enemies.BadParty[i].Data, enemies.BadParty[i].prefab);
            }

            for (int i = 0; i < 2; i++)
            {
                slots.createSlots(Players.GetPlayer(i), Players.battleParty[i].prefab);
            }

            menus.Init();

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

    void Update()
    {
        switch (state)
        {
            case BattleStateM.ACTIVE:
                // Run Battle Logic in here
                for (int i = 0; i < Players.battleParty.Count; i++)
                {
                    // Handles fill of Guage and enquement of commands into Global queue
                    queue.enqueue(Players.Battle(i));
                    queue.enqueue(enemies.Battle(i));
                    BattleObject.GetComponent<commandMenus>().DrawStats(Players.battleParty);

                    // Run enqueued actions after a certain point. Need to add a time delimiter in here.
                    // Something like actions wait for a few seconds before executing.
                    queue.dequeue().Execute();


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
