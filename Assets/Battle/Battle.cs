using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
using menu;

public enum CommandType { SKILL, ITEM };

public class Battle : MonoBehaviour, IReceiver
{
    Queue<object> Inbox; // The Receiver
    Messaging Messenger;
    BattlePlayers Players;
    BattleSlots slots;
    Enemies Enemy;
    Skills Skills;
    CommandQueue Queue;
    GameObject BattleObject;
    GameObject SelectionArrow;
    GameObject[] PlayerObjects = new GameObject[3];
    Scene SceneToReturnTo;
    Creature Caster;
    Creature Receiver;
    Animator BattleAnimations;
    int Index = 0;
    int PlayerIndex;
    int x, y = 3;
    int EnemyKilled;
    int PlayerKilled;
    int PreviousSceneIndex;

    public void Receive(object Message) // Should Recieve be aware of what type of message it is and where to direct it perhaps?
    {
        if (Message != null)
        {
            Inbox.Enqueue(Message);
        }
    }

    public void Subscribe()
    {
        Messenger.Subscribe(MessageType.BATTLE, this);
    }

    public void Unsubscribe()
    {
        Messenger.Unsubscribe(MessageType.BATTLE, this);
    }

    public void StartBattle(Scene PreviousScene, GameObject BattleObject, int Scene)
    {

        SceneToReturnTo = PreviousScene;
        this.BattleObject = BattleObject;

        Enemy = BattleObject.AddComponent<Enemies>();
        Enemy = Enemy.Initalize();

        slots = BattleObject.GetComponent<BattleSlots>();

        Messenger = FindObjectOfType<Messaging>();
        Inbox = new Queue<object>();

        PreviousSceneIndex = Scene;

        Subscribe();

        Skills = BattleObject.AddComponent<Skills>();

        Players = BattleObject.GetComponent<BattlePlayers>();

        Queue = BattleObject.GetComponent<CommandQueue>();

        BattleAnimations = BattleObject.GetComponent<Animator>();

        for (int i = 0; i < BattleObject.GetComponent<BattleEnemies>().badParty.Count; i++)
        {
            //BadParty.Add(Enemy.RandomSelectEnemy().createBattler(x, y));
            slots.createSlots(SlotPosition.FRONT, BattleTag.ENEMY, BattleObject.GetComponent<BattleEnemies>().badParty[i], i);
            x += 1;
        }

        for (int i = 0; i < 2; i++)
        {
            PlayerObjects[i] = new GameObject();
            //Players.Initialize(GameObject.Find(i.ToString()), BattleObject, BadParty, i);
            //PlayerObjects[i] = Players.CreateCharacterById(i);
            DontDestroyOnLoad(PlayerObjects[i]);
            slots.createSlots(SlotPosition.FRONT, BattleTag.PLAYER, Players.GetPlayer(i), i);
        }
    }

    IEnumerator ChangeScene()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(PreviousSceneIndex, LoadSceneMode.Additive);

        while (!async.isDone)
        {
            yield return null;
        }

        AsyncOperation unload = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        while (!unload.isDone)
        {
            Destroy(BattleObject);
            gameStateMessage Message = new gameStateMessage();
            Unsubscribe();
            Message.construct(States.MAIN, null);
            Messenger.Init();
            Destroy(Players);
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
            BattleObject.GetComponent<commandMenus>().DrawStats(Players.battleParty);
        }
        EndBattle();
    }


    public void EndBattle()
    {
        // Add Exp, Items and Levels


    }

}
