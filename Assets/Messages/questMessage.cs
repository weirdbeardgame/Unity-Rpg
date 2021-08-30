using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using questing;


public class questMessage : ScriptableObject
{

    // This can have meny different messages it sends IE. Completed X. Objective or X Quest enabled.
    // Perhaps an enum could be more benificial in this situation?   

    MessageType mType;

    senderInterface Sender;
    Messaging messenger;
    public QuestState questState;
    public int questID;
    public int currentObjective;

    public void construct(int qID, QuestState state)
    {
        mType = MessageType.QUEST;
        questState = state;
        questID = qID;
        send();
    }

    public senderInterface getSender()
    {
        return Sender;
    }

    public QuestState getstate()
    {
        return questState;
    }

    public int getID()
    {
        return questID;
    }

    public void send()
    {
        messenger = FindObjectOfType<Messaging>();
        //messenger.Send(this, MessageType.QUEST);
    }

    public MessageType GetMessageType()
    {
        return mType;
    }
}
