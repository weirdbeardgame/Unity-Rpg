using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using System.Collections;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
//using UnityEngine.Experimental.Rendering.Universal;

public class NPCManager : MonoBehaviour, IReceiver
{
    List<NPCData> _Data;
    NPCData _Initalizer;
    string FilePath = "Assets/NPC.json";
    string NPCData;

    string SpeakerPath = "Assets/Speakers.json";
    string SpeakerData;

    Vector2 position;
    bool Collided;

    public int NpcID;

    public List<Speaker> Speakers;

    CollisionMessage _Collided;

    Queue<object> Inbox; // The Receiver

    DialogueManager manager;

    Messaging _Messenger;

    public List<NPCData> NPC
    {
        get
        {
            return _Data;
        }
    }

    void Start()
    {
        Initalize();
        Subscribe();
    }

    static JsonSerializerSettings settings = new JsonSerializerSettings
    {
        TypeNameHandling = TypeNameHandling.All
    };

    // This is runtime style NPC's
    void Initalize()
    {
        if (File.Exists(FilePath))
        {
            _Data = new List<NPCData>();
            _Initalizer = new NPCData();
            Inbox = new Queue<object>();
            NPCData = File.ReadAllText(FilePath);
            _Data = JsonConvert.DeserializeObject<List<NPCData>>(NPCData, settings);
        }

        if (File.Exists(SpeakerPath))
        {
            Speakers = new List<Speaker>();
            SpeakerData = File.ReadAllText(SpeakerPath);

            Speakers = JsonConvert.DeserializeObject<List<Speaker>>(SpeakerData, settings);
        }
    }

    /*void checkQuest()
   {
       questMessage message = (questMessage)inbox.Dequeue();

       if (message.getstate() == QuestState.IS_ACTIVE)
       {
           if (QID == message.getID())
           {
               // Does NPC have event or quest?
           }
       }
   }*/

    void Talk()
    {
        for (int i = 0; i < _Data.Count; i++)
        {
            if (_Collided.CollidedID == _Data[i].NpcID)
            {
                if (_Data[i].Flag == QuestFlag.HAS_QUEST)
                {

                }
            }

        }
    }

    public void Subscribe()
    {
        _Messenger = FindObjectOfType<Messaging>();
        _Messenger.Subscribe(MessageType.QUEST, this); // there's going to be alot of things subscribed too in here.
        _Messenger.Subscribe(MessageType.COLLISION, this);
    }

    public void Receive(object message)
    {
        Inbox.Enqueue(message);
    }

    public void Unsubscribe()
    {
        _Messenger.Unsubscribe(MessageType.QUEST, this);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (Inbox.Count > 0 && Inbox.Peek() is CollisionMessage)
        {
            _Collided = (CollisionMessage)Inbox.Dequeue();
        }

        if (Input.GetButtonDown("Submit") && _Collided != null)
        {
            Talk();
        }

        /*if ((_Data[0].Flag & QuestFlag.HAS_QUEST) > 0)
          {
              //checkQuest(); // This is Ref's prefered style ie. Constant polling of player's quest state
              // This will get more complex later to cover what part of the quest we're in and if they have an event for that part etc.
          }*/
    }
}
