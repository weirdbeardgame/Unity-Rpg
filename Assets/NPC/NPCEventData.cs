using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEditor;
using Questing;

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

    QuestBook QuestBook;
    StateMachine CurrentState; 
    DialogueManager Dialogue;

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
                break;

            case NPCEventType.FOLLOW:
                // Take control of the player and either follow them or they follow you NPC!
                break;
                
            // Give that bitch a Quest no matter what!
            case NPCEventType.ADDQUEST:                
                QuestBook = FindObjectOfType<QuestBook>();
                CurrentState = FindObjectOfType<StateMachine>();
                
                QuestBook.Give(Quests);

                break;

        }
    }

}
