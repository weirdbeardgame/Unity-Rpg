using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public enum QuestEventType {DIALOUGE, ADDITEM, FOLLOW};

[CreateAssetMenu(menuName = "New Quest Event")]
public class QuestEventData : ScriptableObject
{
    int ID = 0;
    public Flags RequiredFlag;
    public Flags FlagToSet;
    public QuestEventType Type;
    [System.NonSerialized]
    public ItemData Item;
    InventoryMessage message;
    public void Execute()
    {
        switch (Type)
        {
            case QuestEventType.ADDITEM:
                message = new InventoryMessage();
                message.construct(Item, itemState.RECIEVED);
                break;

        }
    }

}
