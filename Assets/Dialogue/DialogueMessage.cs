using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeType { DIALOUGE, FLAG, CHOICE, EVENT }

[System.Serializable]
public class DialogueMessage : IComparable<DialogueMessage>
{

    private int _ID;
    private int _Index;
    private Speaker _SpeakerID;

    private string _Name;
    private string _ToReturn;
    private string _Line;

    public string[] Choices;

    private Flags _NodeFlag;
    private Flags _FlagToSet;

    private NodeType _NodeT;

    private bool _Choice;

    public int NpcId;
    public int Quest;

    public string Name
    {
        get
        {
            return _Name;
        }

        set
        {
            _Name = value;
        }
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

    public Flags FlagToSet
    {
        get
        {
            return _FlagToSet;
        }

        set
        {
            _FlagToSet = value;
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

    public bool Choice
    {
        get
        {
            return _Choice;
        }

        set
        {
            _Choice = value;
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
