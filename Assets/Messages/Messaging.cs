using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using questing;

public class Messaging : MonoBehaviour
{
    Queue<Action<int>> Inbox;
    List<IReceiver> Subscriptions;

    List<IReceiver> receivers;
    int mID;

    StateMachine states;

    MessageType types;

    Battle battle;

    // Start is called before the first frame update
    void Start()
    {
        states = FindObjectOfType<StateMachine>();
        Inbox = new Queue<Action<int>>();
    }

    public void Init()
    {
        states = FindObjectOfType<StateMachine>();

        if (states.State == States.BATTLE)
        {
            battle = FindObjectOfType<Battle>();
        }

        if (Subscriptions == null)
        {
            Subscriptions = new List<IReceiver>();
        }
    }

    public void Subscribe(IReceiver listener)
    {
        if (!Subscriptions.Contains(listener))
        {
            Subscriptions.Add(listener);
        }
        Subscriptions.Add(listener);
    }

    public void Unsubscribe(IReceiver listener)
    {
        if (Subscriptions.Contains(listener))
        {
            Subscriptions.Remove(listener);
        }
    }
    public void Enqueue(Action<int> message)
    {
        if (Inbox != null && message != null)
        {
            Inbox.Enqueue(message);
        }
    }

    public void Send() // S is sender, R is Receiver, D is data
    {
        while (Inbox.Count > 0)
        {
            for (int i = 0; i < Subscriptions.Count; i++)
            {
                //if (Inbox.Peek())
                Subscriptions[i].Receive(Inbox.Dequeue());
            }
        }
    }
}