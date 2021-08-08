﻿using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeType { DIALOUGE, FLAG, CHOICE, EVENT }
public enum NodeDirection { LEFT, RIGHT }
public enum Emotion {HAPPY, NEUTRAL, EXCITED, SURPISED, SCARED}

public struct ChoiceData
{
    public Flags SetFlag;
    public string ChoiceName; // Yes, No. No way kid. Absolutely sir.
    public NodeDirection Dir;
};


[System.Serializable]
public class DialogueMessage : IComparable<DialogueMessage>
{

    private int id;
    private int index;
    private NPCData speakerID;

    private string toReturn;
    private string line;

    public ChoiceData[] Choices;

    private Flags nodeFlag;

    private NodeType nodeT;

    private FlagReqSet flagType;

    public int quest;

    public DialogueMessage()
    {

    }

    public DialogueMessage(ref int mID)
    {
        id = mID;
    }

    public string Line
    {
        get
        {
            return line;
        }

        set
        {
            line = value;
        }
    }

    public int ID
    {
        get
        {
            return id;
        }
        set
        {
            id = value;
        }
    }

    public NPCData SpeakerID
    {
        get
        {
            return speakerID;
        }

        set
        {
            speakerID = value;
        }
    }

    public Flags Flag
    {
        get
        {
            return nodeFlag;
        }

        set
        {
            nodeFlag = value;
        }
    }

    public NodeType NodeT
    {

        get
        {
            return nodeT;
        }

        set
        {
            nodeT = value;
        }

    }

    public FlagReqSet FlagType
    {
        get
        {
            return flagType;
        }

        set
        {
            flagType = value;
        }
    }

    public int CompareTo(DialogueMessage obj)
    {
        if (id < obj.id)
        {
            return -1;
        }

        if (id > obj.id)
        {
            return 1;
        }

        return 0;
    }
}
