using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestEvent : MonoBehaviour
{
    

    StateMachine state;

    private void Start()
    {
        state = FindObjectOfType<StateMachine>();
    }

    private void Update()
    {
        /*if (state.CurrrentFlag == eventToCall.RequiredFlag) //check if current flag is required flag then execute
        {
            eventToCall.Execute(); 
        }*/
    }
}
