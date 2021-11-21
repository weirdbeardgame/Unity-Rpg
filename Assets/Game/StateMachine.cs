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
    States state;
    Flags flag;
    public StateChangeEventArgs()
    {
        flag = null;
        state = States.MAIN;
    }
    public StateChangeEventArgs(States s)
    {
        state = s;
    }
    public States State
    {
        get
        {
            return state;
        } 
        private set
        {
            state = value;
        }
    }
}

class StateMachine : MonoBehaviour
{
    private States state;

    // This here be wrong! I'm effctively ignoring the overloaded Event Args type above but for now :shrug:
    public delegate void StateChange(States s);
    public event StateChange StateChangeEvent;

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

    public void InvokeStateChange(States s)
    {
        StateChangeEvent.Invoke(s);
    }

    public void OnStateChanged(States s)
    {
        if (state != s)
        {
            Debug.Log("State: " + s.ToString());
            state = s;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StateChangeEvent += (s) => { OnStateChanged(s); };
        DontDestroyOnLoad(this);
    }

    void Update()
    {
    }
}
