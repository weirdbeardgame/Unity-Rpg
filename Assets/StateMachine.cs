using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum States { MAIN, BATTLE, CUTSCENE, PAUSE };
public enum FlagReqSet { REQUIRED, SET }

public class Flags
{
    private int _ID;
    private string _Flag;

    private bool _SubFlag; // Digging into the issues. It doesn't list what quest it belongs too. There's no route to the next event flag though I suppose the quest could take care of that
    // Perhaps the flags don't need to be aware they belong to an objective or quest. The quest / objective does

    [System.NonSerialized]
    private bool _IsActive;

    public string Flag
    {
        get
        {
            return _Flag;
        }

        set
        {
            _Flag = value;
        }
    }

    public int ID
    {
        get
        {
            return _ID;
        }
        set
        {
            _ID = value;
        }
    }

    public bool SubFlag
    {
        get
        {
            return _SubFlag;
        }

        set
        {
            _SubFlag = value;
        }

    }

    public bool Activate()
    {
        return _IsActive = true;
    }

    public bool DeActivate()
    {
        return _IsActive = false;
    }

    public bool IsActive()
    {
        return _IsActive;
    }
}

class StateMachine : MonoBehaviour, IReceiver
{
    private States _State;
    Queue<object> Inbox;
    Messaging Messenger;
    List<Flags> FlagData;
    gameStateMessage Message;

    string FlagJson;
    string FlagPath = "Assets/Flags.json";


    private Flags _CurerntFlag;

    public States State
    {
        get
        {
            return _State;
        }

        set
        {
            _State = value;
        }
    }

    public Flags CurrrentFlag
    {
        get
        {
            return _CurerntFlag;
        }

        set
        {
            _CurerntFlag = value;
        }

    }

    void ReadJson()
    {
        FlagData = new List<Flags>();
        if (File.Exists(FlagPath))
        {
            FlagJson = File.ReadAllText(FlagPath);
            FlagData = JsonConvert.DeserializeObject<List<Flags>>(FlagJson);
        }
    }

    // Start is called before the first frame update
    void Start()
    {    
        Messenger = FindObjectOfType<Messaging>(); 
        Inbox = new Queue<object>();
        ReadJson();
        Subscribe();
        CurrrentFlag = FlagData[0];
        DontDestroyOnLoad(this);
    }

    public void Subscribe()
    {
        Messenger.Subscribe(MessageType.GAME_STATE, this);
    }

    public void Unsubscribe()
    {
        Messenger.Unsubscribe(MessageType.GAME_STATE, this);
    }

    public void Receive(object Message)
    {
        Inbox.Enqueue(Message);
    }

    void Update()
    {

        if (Inbox.Count > 0)
        {
            Message = (gameStateMessage)Inbox.Dequeue();
            if (Message.GetState() != null)
            {
                _State = Message.GetState();
            }
                
            _CurerntFlag = Message.GetFlag();
            _CurerntFlag.Activate();
        }
    }


}
