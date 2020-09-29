using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Events { DIALOGUE, CUTSCENE, FOLLOW };

public class QuestEvent : ScriptableObject
{

    public List<BinarySearchTree<DialogueMessage>> Trees; // Each Event can hold an instance of Tree. 

    private Flags Flag;
    private Events _Type;

    private FlagReqSet IsRequired;


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



    public void Execute()
    {
        switch (_Type)
        {
            case Events.DIALOGUE:

                break;


            case Events.FOLLOW:

                break;


            case Events.CUTSCENE:

                break;
        }
    }

}
