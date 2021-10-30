using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Questing;

public class Messaging : MonoBehaviour
{
    Queue<IMessage> Inbox;
    Dictionary<MessageType, List<IReceiver>> Subscriptions;

    List<IReceiver> receivers;
    int mID;

    StateMachine states;

    MessageType types;

    Battle battle;

    // Start is called before the first frame update
    void Start()
    {
        states = FindObjectOfType<StateMachine>();
        Inbox = new Queue<IMessage>();
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
            Subscriptions = new Dictionary<MessageType, List<IReceiver>>();
        }
    }

    public void Subscribe(MessageType T, IReceiver listener)
    {
        if (Subscriptions == null)
        {
            Subscriptions = new Dictionary<MessageType, List<IReceiver>>();
        }

        if (!Subscriptions.ContainsKey(T))
        {
            Subscriptions.Add(T, new List<IReceiver>());
        }
        Subscriptions[T].Add(listener);
   }

    public void Unsubscribe(MessageType type, IReceiver listener)
    {
        if (Subscriptions.ContainsKey(type))
        {
            Subscriptions[type].Remove(listener);
        }
    }
    public void Enqueue(IMessage Message)
    {
        if (Inbox != null && Message != null)
        {
            Inbox.Enqueue(Message);
            mID += 1;
        }
    }

    public void Send() // S is sender, R is Receiver, D is data
    {
        while (Inbox.Count > 0)
        {
            IMessage ToSend = Inbox.Dequeue();
            for (int i = 0; i < Subscriptions[ToSend.GetMessageType()].Count; i++)
            {
                Subscriptions[ToSend.GetMessageType()][i].Receive(ToSend);
            }
        }
    }

    private void Update()
    {
        //Send();
    }

}
