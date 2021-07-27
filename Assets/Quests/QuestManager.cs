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
        if (message is questMessage)
        {
            inbox.Enqueue((questMessage)message);
        }

        if (message is InventoryMessage)
        {
            inbox.Enqueue((InventoryMessage)message);
        }
    }

    public void Unsubscribe()
    {
        Messenger.Unsubscribe(MessageType.QUEST, this);
    }

    public void Progress() // progresses through the list of objectives
    {
        // Need's rewrite
        /*if (Book.getActiveQuest != null)
        {
            switch (Book.getActiveQuest.Objectives[Book.getActiveQuest.ActiveObjective.ObjectiveID].Type)
            {
                case QuestObjectiveType.COLLECT:
                    if (Book.getActiveQuest.Objectives[Book.getActiveQuest.ActiveObjective.ObjectiveID].AmountCollected == Book.getActiveQuest.Objectives[Book.getActiveQuest.ActiveObjective].RequiredItems[0].RequiredAmount)
                    {
                        // ADD REWARD
                        Book.getActiveQuest.Objectives[Book.getActiveQuest.ActiveObjective.ObjectiveID].State = QuestObjectiveState.COMPLETED;
                        //Book.getActiveQuest.ActiveObjective.ObjectiveID++;
                        if (Book.getActiveQuest.Objectives[Book.getActiveQuest.ActiveObjective.ObjectiveID] == null)
                        {
                            Complete();
                        }
                        Debug.Log("Quest Complete");
                    }
                    break;
                case QuestObjectiveType.KILL:
                    if (Book.getActiveQuest.Objectives[Book.getActiveQuest.ActiveObjective.ObjectiveID].AmountCollected == Book.getActiveQuest.Objectives[Book.getActiveQuest.ActiveObjective].RequiredItems[0].RequiredAmount)
                    {
                        // ADD REWARD
                        Book.getActiveQuest.Objectives[Book.getActiveQuest.ActiveObjective.ObjectiveID].State = QuestObjectiveState.COMPLETED;
                        //Book.getActiveQuest.ActiveObjective++;
                    }
                    break;
            }
        }*/
    }

    public void Complete()
    {

    }


    void Update()
    {

    }
}

