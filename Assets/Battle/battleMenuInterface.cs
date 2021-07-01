using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMIface : MonoBehaviour
{
    protected bool open;
    [SerializeField]
    protected JobSystem job;

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
