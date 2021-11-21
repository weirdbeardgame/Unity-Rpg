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
    public StateChangeEventArgs(Flags flags, States s)
    {
        flag = flags;
        state = s;
    }
    public Flags Flag
    {
        get
        {
            return flag;
        }
        private set
        {
            flag = value;
        }
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

    public delegate void ChangeStateDel(States s);
    public event ChangeStateDel stateChangedEvent;

    public void ChangeSate(States s)
    {
        state = s;
        stateChangedEvent.Invoke(state);
    }

}

class StateMachine : MonoBehaviour
{
    private States state;

    StateChangeEventArgs events;
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

    public void OnStateChanged(States s)
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        //events.stateChangedEvent += OnStateChanged(events.state);
        DontDestroyOnLoad(this);

    }

    void Update()
    {
    }
}
