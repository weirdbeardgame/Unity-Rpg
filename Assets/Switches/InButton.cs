using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InButton : MonoBehaviour
{
    private bool isActive;
    private int leverID;

    bool Collided;

    public int LeverID
    {
        get
        {
            return leverID;
        }

        set
        {
            leverID = value;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Collided = true;
        }

        else
        {
            Collided = false;
        }

    }

    public void Press()
    {
        // Play sound and animation in here
        isActive = true;
    }


    // Update is called once per frame
    void Update()
    {


    }
}
