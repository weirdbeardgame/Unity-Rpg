using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Inputs { NULL, LEFT, RIGHT, UP, DOWN, A, B, X, Y, START }

struct InputData : IMessage
{ 
    public Inputs CurrentInput;
    public float Axis; // For 3D games. Handle Full Joystick axis

    public MessageType GetMessageType()
    {
        return MessageType.INPUT;
    }
}

public class input : MonoBehaviour, IReceiver
{
    StateMachine gameState;
    bool canInput = true;

    InputData ToInput;

    Queue<object> inbox;

    Messaging message;

    void Start()
    {
        message = FindObjectOfType<Messaging>();

        inbox = new Queue<object>();

        Subscribe();

    }

    public void Receive(object message)
    {
        inbox.Enqueue(message);
    }

    public void Subscribe()
    {
        message.Subscribe(MessageType.GAME_STATE, this);
    }

    public void Unsubscribe()
    {
        message.Unsubscribe(MessageType.GAME_STATE, this);
    }

    void PushButton(Inputs I)
    {
        if (canInput)
        {
            ToInput = new InputData();
            ToInput.CurrentInput = I;
            message.Enqueue(ToInput);
        }
        else
        {
            return;
        }
    }

    void SetAxis(float RecievedAxis)
    {
        if (canInput)
        {
            ToInput.Axis = RecievedAxis;
        }
    }
}