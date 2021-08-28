using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using System.IO;
using questing;


// This is the interface always remeber that, the rest is just data and nothing more.
// As the interface all this should do is hold the data and be aware of what the quest book is doing

public class QuestManager : MonoBehaviour, IReceiver
{
    public List<QuestData> Quests;
    QuestData QuestD;
    Messaging Messenger;

    QuestBook Book;

    string FilePath = "Assets/Quests/Quest.json";
    string Data;

    int ActiveID;

    InventoryMessage InvMessage;
    questMessage questMessage;


    // We need a check in the lower quests there should be smaller quests then overarching larger ones? 
    Queue<object> inbox; // The Receiver

    public void Subscribe()
    {
        Messenger.Subscribe(MessageType.QUEST, this);
        //Messenger.Subscribe(MessageType.INVENTORY, this);
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        Subscribe();
    }

    void Initialize()
    {
        Messenger = FindObjectOfType<Messaging>();
        Book = FindObjectOfType<QuestBook>();
        inbox = new Queue<object>();
        if (File.Exists(FilePath))
        {
            Quests = new List<QuestData>();

            Data = File.ReadAllText(FilePath);
            Quests = JsonConvert.DeserializeObject<List<QuestData>>(Data);
        }
    }

    public QuestData Get(int id)
    {
        return Quests[id];
    }

    public void Receive(object message)
    {
        inbox.Enqueue(message);
    }

    public void Unsubscribe()
    {
        Messenger.Unsubscribe(MessageType.QUEST, this);
    }

    public void Progress() // progresses through the list of objectives
    {

    }

    public void Complete()
    {

    }


    void Update()
    {

    }
}

