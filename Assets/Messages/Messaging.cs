using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using questing;


public enum MessageType { BATTLE, QUEST, INPUT, GAME_STATE, INVENTORY, COLLISION, SWITCH }


public class Messaging : MonoBehaviour
{
    Dictionary<MessageType, List<IReceiver>> subscriptions; // This contains the list of who's subscribing to what while the below is the accesser to who's subscribing specifically.
    Dictionary<MessageType, List<IReceiver>> Subscriptions
    {
        get
        {
            if (subscriptions == null)
            {
                subscriptions = new Dictionary<MessageType, List<IReceiver>>();
            }
            return subscriptions;
        }
    }

    List<IReceiver> receivers;
    int mID;

    StateMachine states;

    MessageType types;

    Battle battle;

    // Start is called before the first frame update
    void Start()
    {

        states = FindObjectOfType<StateMachine>();

    }

    public void Init()
    {
        states = FindObjectOfType<StateMachine>();


        if (states.State == States.BATTLE)
        {
            battle = FindObjectOfType<Battle>();
        }

        if (subscriptions == null)
        {
            subscriptions = new Dictionary<MessageType, List<IReceiver>>();
        }

    }

    public void Subscribe(MessageType type, IReceiver listener)
    {

        if (!Subscriptions.ContainsKey(type))
        {
            Subscriptions.Add(type, new List<IReceiver>());
        }

        Subscriptions[type].Add(listener);

    }

    public void Unsubscribe(MessageType type, IReceiver listener)
    {
        if (Subscriptions.ContainsKey(type))
        {
            Subscriptions[type].Remove(listener);
        }
    }

    MessageType GetMessageType()
    {
        return types;
    }

    public void Send(object message, MessageType type) // S is sender, R is Receiver, D is data
    {
        if (states.State == States.MAIN || GetComponent<StateMachine>().State == States.PAUSE)
        {
            if (Subscriptions.Count > 0)
            {
                if (Subscriptions.ContainsKey(type))
                {
                    for (int i = 0; i < Subscriptions[type].Count; i++)
                    {
                        Subscriptions[type][i].Receive(message);
                    }
                }
            }
        }

        if (states.State == States.BATTLE)
        {
            if (Subscriptions.Count > 0)
            {
                if (Subscriptions.ContainsKey(type))
                {
                    for (int i = 0; i < Subscriptions[type].Count; i++)
                    {
                        Subscriptions[type][i].Receive(message);
                    }
                }

                message = null;
            }
        }
    }
}