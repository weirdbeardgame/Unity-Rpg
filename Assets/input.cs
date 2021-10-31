using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Inputs { NULL, LEFT, RIGHT, UP, DOWN, A, B, X, Y, START }

struct InputData
{ 
    public Inputs CurrentInput;
    public float Axis; // For 3D games. Handle Full Joystick axis
}

public class input : MonoBehaviour
{
    StateMachine gameState;
    bool canInput = true;

    InputData ToInput;

    void Start()
    {
    }

    void PushButton(Inputs I)
    {
        if (canInput)
        {
            ToInput = new InputData();
            ToInput.CurrentInput = I;
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