using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Events { DIALOGUE, CUTSCENE, FOLLOW };

public class QuestEvent : ScriptableObject, IReceiver
{

    public List<BinarySearchTree<DialogueMessage>> Trees; // Each Event can hold an instance of Tree. 
    NPCMovement ScriptedMovement; // Perhaps an instance of movement even

    private string _EventName;

    private Events _Type;

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

public string EventName
{
    get
    {
        return _EventName;
    }

    set
    {
        _EventName = value;
    }
}

    public void Initalize()
    {

    }

    public void Subscribe()
    {

    }


    public void Receive(object Message)
    {

    }

    public void Unsubscribe()
    {

    }

}
