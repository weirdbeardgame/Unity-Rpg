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
        //Messenger.Subscribe(MessageType.QUEST, this);
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
        //Messenger.Unsubscribe(MessageType.QUEST, this);
    }

    public void Progress() // progresses through the list of objectives
    {

        /*if (Book.ActiveQuest != null)
        {
            switch (Book.ActiveQuest.Objectives[Book.ActiveQuest.ActiveObjective].Type)
            {
                case QuestObjectiveType.COLLECT:
                    if (Book.ActiveQuest.Objectives[Book.ActiveQuest.ActiveObjective].AmountCollected == Book.ActiveQuest.Objectives[Book.ActiveQuest.ActiveObjective].RequiredItems[0].RequiredAmount)
                    {
                        // ADD REWARD
                        Book.ActiveQuest.Objectives[Book.ActiveQuest.ActiveObjective].State = QuestObjectiveState.COMPLETED;
                        Book.ActiveQuest.ActiveObjective++;
                        if (Book.ActiveQuest.Objectives[Book.ActiveQuest.ActiveObjective] == null)
                        {
                            Complete();
                        }
                        Debug.Log("Quest Complete");
                    }
                    break;
                case QuestObjectiveType.KILL:
                    if (Book.ActiveQuest.Objectives[Book.ActiveQuest.ActiveObjective].AmountCollected == Book.ActiveQuest.Objectives[Book.ActiveQuest.ActiveObjective].RequiredItems[0].RequiredAmount)
                    {
                        // ADD REWARD
                        Book.ActiveQuest.Objectives[Book.ActiveQuest.ActiveObjective].State = QuestObjectiveState.COMPLETED;
                        Book.ActiveQuest.ActiveObjective++;
                    }
                    break;
            }
        }*/
    }

    public void Complete()
    {

        // Apply next flag then go from there. I really need subflags. What I can do is have this be a loop checking for required subflags?

    }


    void Update()
    {

        /*if (inbox.Count > 0)
        {
            if (inbox.Peek() is InventoryMessage && Book.ActiveQuest != null && Book.ActiveQuest.Type == QuestType.COLLECT)
            {
                InvMessage = (InventoryMessage)inbox.Dequeue();
                if (InvMessage.GetID() == Book.ActiveQuest.Objectives[Book.ActiveQuest.ActiveObjective].RequiredItems[0].ItemID)
                {
                    Book.ActiveQuest.Objectives[Book.ActiveQuest.ActiveObjective].AmountCollected++;
                    Debug.Log("Item recieved");
                }
            }

            else if (inbox.Peek() is questMessage)
            {
                questMessage = (questMessage)inbox.Dequeue();
                ActiveID = questMessage.getID();

                Debug.Log("Quest Active");
            }
        }


        Progress(); // Whether this belongs here or in the book's half is TBD

    }*/

    }
}