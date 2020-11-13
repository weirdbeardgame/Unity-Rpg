using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using questing;

public enum QuestEventType {DIALOUGE, ADDITEM, FOLLOW, ADDQUEST};

[CreateAssetMenu(menuName = "New Quest Event")]
public class QuestEventData : ScriptableObject
{
    int ID = 0;
    public string EventName;
    public Flags RequiredFlag;
    public Flags FlagToSet;
    public QuestData Quests;
    public QuestEventType Type;
    public ItemData Item;

    questBook QuestBook;
    StateMachine CurrentState; 
    InventoryMessage message;
    gameStateMessage state;
    DialogueManager Dialogue;

    public List<BinarySearchTree<DialogueMessage>> binarySearchTrees;
    
    public BinarySearchTree<DialogueMessage> TreeToEdit;
    public void Execute(GameObject Speaker)
    {
        switch (Type)
        {
            case QuestEventType.ADDITEM:
                message = new InventoryMessage();
                message.construct(Item, itemState.RECIEVED);
                Debug.Log("Item Given");
                break;

            case QuestEventType.DIALOUGE:
                Debug.Log("EVENT EXECUTE");
                Dialogue = FindObjectOfType<DialogueManager>();
                CurrentState = FindObjectOfType<StateMachine>();
                Dialogue.OpenDialogueBox(binarySearchTrees[0], Speaker);
                state = new gameStateMessage();
                state.construct(CurrentState.State, FlagToSet);
                break;

            case QuestEventType.FOLLOW:
                // Take control of the player and either follow them or they follow you NPC!
                break;
                
            // Give that bitch a Quest no matter what!
            case QuestEventType.ADDQUEST:
                Debug.Log("Quest Given");
                
                QuestBook = FindObjectOfType<questBook>();
                CurrentState = FindObjectOfType<StateMachine>();
                
                QuestBook.Give(Quests);

                state = new gameStateMessage();
                state.construct(CurrentState.State, FlagToSet);
                break;

        }
    }

}
