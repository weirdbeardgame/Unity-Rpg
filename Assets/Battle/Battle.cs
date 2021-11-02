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

public class Battle : MonoBehaviour
{
    BattlePlayers Players;
    BattleEnemies enemies;
    BattleSlots slots;
    CommandQueue Queue;
    GameObject BattleObject;
    GameObject SelectionArrow;
    Scene SceneToReturnTo;
    Creature Caster;
    Creature Receiver;
    Animator BattleAnimations;
    int Index = 0;
    int PlayerIndex;
    int x, y = 3;
    int EnemyKilled;
    int PlayerKilled;

    public void StartBattle(Scene PreviousScene, GameObject BattleObject, int Scene)
    {
        SceneToReturnTo = PreviousScene;
        this.BattleObject = BattleObject;

        slots = BattleObject.GetComponent<BattleSlots>();

        Queue = BattleObject.GetComponent<CommandQueue>();
        Players = BattleObject.GetComponent<BattlePlayers>();
        enemies = BattleObject.GetComponent<BattleEnemies>();
        BattleAnimations = BattleObject.GetComponent<Animator>();

        for (int i = 0; i < BattleObject.GetComponent<BattleEnemies>().badParty.Count; i++)
        {
            slots.createSlots(SlotPosition.FRONT, BattleTag.ENEMY, enemies.badParty[i].Data, i);
            x += 1;
        }

        for (int i = 0; i < 2; i++)
        {
            slots.createSlots(SlotPosition.FRONT, BattleTag.PLAYER, Players.GetPlayer(i), i);
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

        AsyncOperation async = SceneManager.LoadSceneAsync(SceneToReturnTo.name);

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
        // Run Battle Logic in here
        for (int i = 0; i < Players.battleParty.Count; i++)
        {
            Players.Battle(i);
            enemies.Battle(i);
            BattleObject.GetComponent<commandMenus>().DrawStats(Players.battleParty);
            // Listen for death and action. Check for enemy or player death
        }
        EndBattle();
    }


    public void EndBattle()
    {
        // Add Exp, Items and Levels. Show screen to do so
    }

}
