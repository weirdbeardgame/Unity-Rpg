using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Inputs { NULL, LEFT, RIGHT, UP, DOWN, A, B, X, Y, START }

public class input : MonoBehaviour, IReceiver
{
    StateMachine gameState;
    Inputs CurrentInput;
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
        //message.Subscribe(MessageType.GAME_STATE, this);
    }

    public void Unsubscribe()
    {
        //message.Unsubscribe(MessageType.GAME_STATE, this);
    }

    void PushButton(Inputs I)
    {
        CurrentInput = I;
    }


    // Update is called once per frame
    void FixedUpdate()
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
                            CurrentInput = Inputs.RIGHT;
                        }

                        if (Input.GetAxisRaw("Horizontal") == -1)    /// Stick move 1, 0, -1 This lasts a frame
                        {
                            isPressed = true;
                            CurrentInput = Inputs.LEFT;
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
                        CurrentInput = Inputs.UP;
                    }

                    if (Input.GetAxisRaw("Vertical") == -1)
                    {
                        isPressed = true;
                        CurrentInput = Inputs.DOWN;
                    }
                }
            }

            if (Input.GetAxisRaw("Vertical") == 0)
            {
                isPressed = false;
            }

            if (Input.GetButtonDown("Submit")) // Happy Ansem Noises
            {
                CurrentInput = Inputs.A;
                return;
            }

            if (Input.GetButtonDown("Cancel"))
            {
                CurrentInput = Inputs.START;
                return;
            }

            message.Enqueue(PushButton => CurrentInput);

        }
    }

    public void setCanInput(bool i)
    {
        canInput = i;
    }

}