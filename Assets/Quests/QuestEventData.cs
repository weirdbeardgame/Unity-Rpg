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
    InventoryMessage message;
    DialogueManager Dialogue;

    public List<BinarySearchTree<DialogueMessage>> binarySearchTrees;
    
    public BinarySearchTree<DialogueMessage> TreeToEdit;
    public void Execute()
    {
        switch (Type)
        {
            case QuestEventType.ADDITEM:
                message = new InventoryMessage();
                message.construct(Item, itemState.RECIEVED);
                break;

            case QuestEventType.DIALOUGE:
                //Dialogue.OpenDialogueBox(binarySearchTrees[0].Tree);
                break;

            case QuestEventType.FOLLOW:
                // Take control of the player and either follow them or they follow you NPC!
                break;

            case QuestEventType.ADDQUEST:
                // Give that bitch a Quest no matter what!
                break;

        }
    }

}
