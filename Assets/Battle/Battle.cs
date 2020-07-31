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

    Enemies Enemy;
    Skills Skills;

    List<Baddies> BadParty;

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
    int X, Y = 3;
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

        Messenger = FindObjectOfType<Messaging>();
        Inbox = new Queue<object>();

        PreviousSceneIndex = Scene;

        Subscribe();

        Skills = BattleObject.AddComponent<Skills>();

        Players = BattleObject.GetComponent<BattlePlayers>();
        BadParty = new List<Baddies>();

        Queue = BattleObject.GetComponent<CommandQueue>();

        BattleAnimations = BattleObject.GetComponent<Animator>();

        for (int i = 0; i < 2; i++)
        {
            Enemy.EnemyData[i].createBattler(X, Y);
            BadParty.Add(Enemy.EnemyData[i]);
            Y += 1;
        }

        for (int i = 0; i < 2; i++)
        {
            PlayerObjects[i] = new GameObject();
            Players.Initialize(GameObject.Find(i.ToString()), BattleObject, BadParty, i);
            PlayerObjects[i] = Players.CreateCharacterById(i);
            DontDestroyOnLoad(PlayerObjects[i]);
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
            gameStateMessage Message = ScriptableObject.CreateInstance<gameStateMessage>();
            Unsubscribe();
            Message.construct(States.MAIN);
            Messenger.Init();
            Destroy(Players);
            BadParty.Clear();
            yield return null;
        }
    }


    void Update()
    {
        // Run Battle Logic in here
        for (int i = 0; i < Players.BattleParty.Count; i++)
        {
            Players.Battle(i);
        }

        EndBattle();
    }


    public void EndBattle()
    {

        for (int i = 0; i < BadParty.Count; i++)
        {
            if (!BadParty[i].checkHealth())
            {
                EnemyKilled++;
                BadParty.RemoveAt(i);
            }
        }

        if (EnemyKilled == 2)
        {
            Debug.Log("Battle Finished");
            StartCoroutine(ChangeScene());
        }


        // Add Exp, Items and Levels


    }

}
