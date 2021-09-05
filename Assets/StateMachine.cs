using System.Collections;
using System.Collections.Generic;
using System.IO;
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

class StateMachine : MonoBehaviour
{
    private States state;
    Messaging messenger;
    gameStateMessage message;

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

    // Start is called before the first frame update
    void Start()
    {
        messenger = FindObjectOfType<Messaging>(); 
        DontDestroyOnLoad(this);
    }

    void Update()
    {
    }
}
