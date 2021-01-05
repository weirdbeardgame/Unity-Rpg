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
    public List<NPCData> ToInit;
    public NPCData Initalizer;
    string FilePath = "Assets/NPC/NPC.json";
    string NPCData;

    Vector2 position;
    bool Collided;

    int NpcID;

    CollisionMessage _Collided;

    Queue<object> Inbox; // The Receiver

    DialogueManager manager;

    Messaging _Messenger;

    public Dictionary<int, NPCData> ConstructedNPC;
  
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
            ToInit = new List<NPCData>();
            Initalizer = new NPCData();
            Inbox = new Queue<object>();
            NPCData = File.ReadAllText(FilePath);
            ToInit = JsonConvert.DeserializeObject<List<NPCData>>(NPCData, settings);
        }
        foreach(var npc in ToInit)
        {
            if (File.Exists(npc.NpcEventPath))
            {
                npc.EventData = new List<NPCEventData>();
                NPCData = File.ReadAllText(npc.NpcEventPath);
                npc.EventData = JsonConvert.DeserializeObject<List<NPCEventData>>(NPCData);
            }
        }
    }

    public void Construct(NPCData D)
    {
        if (ConstructedNPC == null)
        {
            ConstructedNPC = new Dictionary<int, NPCData>();
        }

        if (!ConstructedNPC.ContainsKey(D.NpcID))
        {
            ConstructedNPC.Add(D.NpcID, D);
        }
    }

    public void Subscribe()
    {
        _Messenger = FindObjectOfType<Messaging>();
        //_Messenger.Subscribe(MessageType.QUEST, this);
        //_Messenger.Subscribe(MessageType.COLLISION, this);
    }

    public void Receive(object message)
    {
        Inbox.Enqueue(message);
    }

    public void Unsubscribe()
    {
        //_Messenger.Unsubscribe(MessageType.QUEST, this);
    }

}
