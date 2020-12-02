﻿using TMPro;
using menu;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Widget : MonoBehaviour
{
    public string ButtonText;
    public GameObject Icon;
    
    public GameObject Text;

    public void Instantiate()
    {
    }

    public void SetParent(GameObject Parent)
    {
        if (Parent)
        {           
            transform.SetParent(Parent.transform);
        }
    }

    // Start is called before the first frame update
    public virtual void Execute() // Override for each individual Widget type. This could be just widgetBase we're describing here
    {

    }


}
