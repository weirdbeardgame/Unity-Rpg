using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Inputs { NULL, LEFT, RIGHT, UP, DOWN, A, B, X, Y, START }

public class input : MonoBehaviour, IReceiver
{
    StateMachine gameState;
    bool canInput = true;
    bool isPressed;

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


    // Update is called once per frame
    void Update()
    {
        gameStateMessage temp = ScriptableObject.CreateInstance<gameStateMessage>() as gameStateMessage;
        if (inbox.Count > 0)
        {
            temp = (gameStateMessage)inbox.Dequeue();

            if (temp.GetState() == States.CUTSCENE)
            {
                setCanInput(false);
            }
        }
        if (canInput)
        {
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                if (!isPressed)
                {
                    if ((Input.GetAxisRaw("Horizontal") == 1) != (Input.GetAxis("Horizontal") == -1)) // HACKFIX, MANUALLY CLEAR LATER
                    {
                        if (Input.GetAxisRaw("Horizontal") == 1)
                        {
                            isPressed = true;
                            message.Send(Inputs.RIGHT, MessageType.INPUT);
                        }

                        if (Input.GetAxisRaw("Horizontal") == -1)    /// Stick move 1, 0, -1 This lasts a frame
                        {
                            isPressed = true;
                            message.Send(Inputs.LEFT, MessageType.INPUT);
                        }
                    }

                    if (Input.GetAxisRaw("Horizontal") == 0)
                    {
                        isPressed = false;
                    }
                }
            }

            if (Input.GetAxisRaw("Vertical") != 0)
            {
                if (!isPressed)
                {
                    if (Input.GetAxisRaw("Vertical") == 1)
                    {
                        isPressed = true;
                        message.Send(Inputs.UP, MessageType.INPUT);
                    }

                    if (Input.GetAxisRaw("Vertical") == -1)
                    {
                        isPressed = true;
                        message.Send(Inputs.DOWN, MessageType.INPUT);
                    }
                }
            }

            if (Input.GetAxisRaw("Vertical") == 0)
            {
                isPressed = false;
            }

            if (Input.GetButtonDown("Submit")) // Happy Ansem Noises
            {
                message.Send(Inputs.A, MessageType.INPUT);
                return;
            }

            if (Input.GetButtonDown("Cancel"))
            {
                message.Send(Inputs.START, MessageType.INPUT);
                return;
            }
        }
    }

    public void setCanInput(bool i)
    {
        canInput = i;
    }

}