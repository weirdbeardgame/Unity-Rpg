using TMPro;
using menu;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;


[ExecuteInEditMode()]
public class Widget : MonoBehaviour
{  // Start is called before the first frame update

    int sizeX;
    int sizeY;

    int MenuToOpen;

    //public Action Executeable;

    bool isHeld;

    string _Title;

    public int ToUse = 0;

    protected MenuManager _Manager;

    public string Title
    {
        get
        {
            return _Title;
        }

        set
        {
            Title = value;
        }
    }

    public virtual void OnUI()
    {

    }

    public virtual void Awake()
    {
        OnUI();
    }

    public virtual void Initalize(int Var1, string Name)
    {

    }

    public virtual void Execute()
    {
        //Executeable();
    }

    public virtual void Update()
    {
        if (Application.isEditor)
        {
            OnUI();
        }
    }

    ~Widget()
    {

    }

}
