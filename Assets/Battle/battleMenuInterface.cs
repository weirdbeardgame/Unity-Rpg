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
    public List<Widget> widgetsToAdd;
    public static List<Widget> widgets;

    public virtual void Open()
    {

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
