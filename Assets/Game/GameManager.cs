using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using Questing;
using System.IO;
using System;


public class GameManager : MonoBehaviour
{
    StateMachine state;
    List<Flags> flags;

    private Flags currentSetFlag;
    string flagJson;
    string flagPath = "Assets/Flags.json";

    GameAssetManager assetManager;
    AudioManager soundManager;
    QuestManager questSystem;
    NPCManager npcManager;

    StateChangeEventArgs stateMachine;

    //JrpgSceneManager scenes;

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManager();
            }
            return instance;
        }
    }

    public Flags CurrrentFlag
    {
        get
        {
            return currentSetFlag;
        }

        set
        {
            currentSetFlag = value;
        }
    }

    void Init()
    {
        flags = new List<Flags>();
        if (File.Exists(flagPath))
        {
            flagJson = File.ReadAllText(flagPath);
            flags = JsonConvert.DeserializeObject<List<Flags>>(flagJson);
        }

        questSystem.Init();
        state.InvokeStateChange(States.MAIN);
        assetManager = GameAssetManager.Instance;
        npcManager.Initalize();
    }

    // Start is called before the first frame update
    void Start()
    {
        state = GetComponent<StateMachine>();
        stateMachine = new StateChangeEventArgs();
        questSystem = GetComponent<QuestManager>();
        soundManager = GetComponent<AudioManager>();
        npcManager = GetComponent<NPCManager>();
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        // Idealy we're going to have an event and a delagate that holds Asset data that's needed and sends or recieves it. For now it's null
        //questSystem.Progress(null);
    }
}
