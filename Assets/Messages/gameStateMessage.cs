using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameStateMessage : ScriptableObject
{

    Messaging Messenger;
    private Flags _Flag;
    private States _CurrentState;

    public void construct(States GameState, Flags SetFlag)
    {
        Messenger = FindObjectOfType<Messaging>();
        _CurrentState = GameState;
        _Flag = SetFlag;

        Messenger.Send(this, MessageType.GAME_STATE);
    }

    public States GetState()
    {
        return _CurrentState;
    }

    public Flags GetFlag()
    {
        return _Flag;
    }

}
