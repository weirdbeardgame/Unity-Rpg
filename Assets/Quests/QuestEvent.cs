using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestEvent : ScriptableObject
{

    //public List<BinarySearchTree<DialogueMessage>> Trees; // Each Event can hold an instance of Tree. 

    private Flags Flag;
    private Events _Type;
    private FlagReqSet IsRequired;
    private int itemID = 0;


    public Events Type
    {
        get
        {
            return _Type;
        }

        set
        {
            _Type = value;
        }
    }

    public FlagReqSet SetOrRequired
    {

        get
        {
            return IsRequired;
        }

        set
        {
            IsRequired = value;
        }
    
    }

    public Flags CurrentFlag
    {
        get
        {
            return Flag;
        }

        set
        {
            Flag = value;
        }
    }

    public int Item
    { 
        get
        {
            return itemID;
        }

        set
        {
            itemID = value;
        }
    }


    public void Execute()
    {
        switch (_Type)
        {
            case Events.DIALOGUE:
                // Let's assume there's going to be an execute per Event. Execute meaning open Dialogue based on flag and quest.
                // Dialogues for Quests can have different speakers like Musungo. 
                break;


            case Events.FOLLOW:
                // Set Waypoints
                break;


            case Events.CUTSCENE:
                // Using WayPoints, and everything else.
                break;

            case Events.ADDITEM:

                break;
        }
    }

}
