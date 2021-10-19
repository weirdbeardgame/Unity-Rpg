using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMIface : MonoBehaviour
{
    protected int appID;
    protected bool open;
    [SerializeField]
    protected JobSystem job;
    protected string appName;

    public virtual void Open()
    {
        // Open prefab
    }

    public virtual void Close()
    {

    }

    public bool isOpen()
    {
        return open;
    }

    public JobSystem getJob()
    {
        return job;
    }
}
