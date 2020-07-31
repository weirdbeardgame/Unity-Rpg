using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SwitchMessage : senderInterface
{
    private int _SwitchID;
    private bool _IsActive;

    Messaging Messenger;


    public Creature getCreature()
    {
        return null;
    }


    public void Construct(int ID, bool Active)
    {
        _SwitchID = ID;
        _IsActive = Active;

        Messenger = ScriptableObject.FindObjectOfType<Messaging>();

        send(this);
    }


    public void send(object message)
    {
        Messenger.Send(this, MessageType.SWITCH);
    }

}
