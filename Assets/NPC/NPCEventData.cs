using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using questing;

public enum NPCEventType {DIALOUGE, ADDITEM, FOLLOW, ADDQUEST};

[CreateAssetMenu(menuName = "New Quest Event")]
public class NPCEventData : ScriptableObject
{
    int ID = 0;
    public string EventName;
    public Flags RequiredFlag;
    public Flags FlagToSet;
    public QuestData Quests;
    public NPCEventType Type;
    public ItemData Item;
    public Inventory Inv;

    questBook QuestBook;
    StateMachine CurrentState; 
    InventoryMessage message;
    gameStateMessage state;
    DialogueManager Dialogue;
    Messaging Messenger;

    public List<BinarySearchTree<DialogueMessage>> binarySearchTrees;
    
    public BinarySearchTree<DialogueMessage> TreeToEdit;
    public void Execute(GameObject Speaker)
    {
        switch (Type)
        {
            case NPCEventType.ADDITEM:
                Inv = FindObjectOfType<Inventory>();
                Inv.Add(Item);
                Debug.Log("Item Given");
                break;

            case NPCEventType.DIALOUGE:
                Dialogue = FindObjectOfType<DialogueManager>();
                CurrentState = FindObjectOfType<StateMachine>();
                Dialogue.OpenDialogueBox(binarySearchTrees[0]);
                state = new gameStateMessage();
                state.construct(CurrentState.State, FlagToSet);
                Messenger = FindObjectOfType<Messaging>();
                Messenger.Enqueue(state);
                break;

            case NPCEventType.FOLLOW:
                // Take control of the player and either follow them or they follow you NPC!
                break;
                
            // Give that bitch a Quest no matter what!
            case NPCEventType.ADDQUEST:                
                QuestBook = FindObjectOfType<questBook>();
                CurrentState = FindObjectOfType<StateMachine>();
                
                QuestBook.Give(Quests);

                state = new gameStateMessage();
                state.construct(CurrentState.State, FlagToSet);
                Messenger = FindObjectOfType<Messaging>();
                Messenger.Enqueue(state);
                break;

        }
    }

}
