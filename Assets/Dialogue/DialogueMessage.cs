using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeType { DIALOUGE, FLAG, CHOICE, EVENT }
public enum NodeDirection { LEFT, RIGHT }
public enum FlagReqSet { REQUIRED, SET }


public struct ChoiceData
{
    public Flags SetFlag;
    public string ChoiceName; // Yes, No. No way kid. Absolutely sir.
    public NodeDirection Dir;
};


[System.Serializable]
public class DialogueMessage : IComparable<DialogueMessage>
{

    private int _ID;
    private int _Index;
    private Speaker _SpeakerID;

    private string _ToReturn;
    private string _Line;

    public ChoiceData[] Choices;

    private Flags _NodeFlag;

    private NodeType _NodeT;

    private FlagReqSet _FlagType;

    public int NpcId;
    public int Quest;

    public DialogueMessage()
    {

    }

    public string Line
    {
        get
        {
            return _Line;
        }

        set
        {
            _Line = value;
        }
    }

    public int ID
    {
        get
        {
            return _ID;
        }
        set
        {
            _ID = value;
        }
    }

    public Speaker SpeakerID
    {
        get
        {
            return _SpeakerID;
        }

        set
        {
            _SpeakerID = value;
        }
    }

    public Flags Flag
    {
        get
        {
            return _NodeFlag;
        }

        set
        {
            _NodeFlag = value;
        }
    }

    public NodeType NodeT
    {

        get
        {
            return _NodeT;
        }

        set
        {
            _NodeT = value;
        }

    }

    public FlagReqSet FlagType
    {
        get
        {
            return _FlagType;
        }

        set
        {
            _FlagType = value;
        }
    }

    public int CompareTo(DialogueMessage obj)
    {
        if (this._ID < obj._ID)
        {
            return -1;
        }

        if (_ID > obj._ID)
        {
            return 1;
        }

        return 0;
    }
}
