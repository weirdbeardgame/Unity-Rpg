using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    private bool isActive;

    public int leverID;

    bool collided;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collided = true;
        }

        else
        {
            collided = false;
        }

    }

    public void Flip()
    {
        // Play Sound and animation in here
        isActive = true;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
