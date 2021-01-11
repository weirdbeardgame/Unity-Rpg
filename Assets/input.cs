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

    // Update is called once per frame
    void FixedUpdate()
    {
        gameStateMessage temp = new gameStateMessage();
        if (inbox.Count > 0)
        {
            temp = (gameStateMessage)inbox.Dequeue();

            if (temp.GetState() == States.CUTSCENE)
            {
                //setCanInput(false);
            }
        }
                   
        if (Input.GetAxisRaw("Horizontal") != 0)           
        {                    
            if ((Input.GetAxisRaw("Horizontal") == 1) != (Input.GetAxis("Horizontal") == -1)) // HACKFIX, MANUALLY CLEAR LATER                    
            {                        
                if (Input.GetAxisRaw("Horizontal") == 1)
                {          
                    PushButton(Inputs.RIGHT);                        
                }
         
                if (Input.GetAxisRaw("Horizontal") == -1)    /// Stick move 1, 0, -1 This lasts a frame                       
                {                            
                    PushButton(Inputs.LEFT);                        
                }        
            }    
        }

            if (Input.GetAxisRaw("Vertical") != 0)
            {    
                if (Input.GetAxisRaw("Vertical") == 1)
                {
                    PushButton(Inputs.UP);
                }

                    if (Input.GetAxisRaw("Vertical") == -1)
                    {
                        PushButton(Inputs.DOWN);
                    }
                }
            

            if (Input.GetButtonDown("Submit")) // Happy Ansem Noises
            {
                PushButton(Inputs.A);
                return;
            }

            if (Input.GetButtonDown("Cancel"))
            {
                PushButton(Inputs.START);
                return;
            }
    }
}