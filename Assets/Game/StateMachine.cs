using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

public enum States { MAIN, BATTLE, CUTSCENE, PAUSE, DIALOGUE };
public enum FlagReqSet { REQUIRED, SET }

public class Flags
{
    private int id;
    private string flag;
    private bool isActive;

    public string Flag
    {
        get
        {
            return flag;
        }

        set
        {
            flag = value;
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

    public bool Activate()
    {
        return isActive = true;
    }

    public bool DeActivate()
    {
        return isActive = false;
    }

    public bool IsActive()
    {
        return isActive;
    }
}

public class StateChangeEventArgs : EventArgs
{
    public Event onStateChange;
    public StateChangeEventArgs(Flags flags, States s)
    {
        flag = flags;
        state = s;
    }
    public Flags flag
    {
        get; private set;
    }
    public States state
    {
        get; private set;
    }
}

class StateMachine : MonoBehaviour
{
    private States state;

    StateChangeEventArgs del;
    public States State
    {
        get
        {
            return state;
        }

        set
        {
            state = value;
        }
    }

    public States SetState(States s)
    {
        return state = s;
    }

    public static void onStateChanged()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        //del.onStateChange += SetState(del.state);
        DontDestroyOnLoad(this);

    }

    void Update()
    {
    }
}
