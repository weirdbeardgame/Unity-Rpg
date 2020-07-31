using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{

    private SwitchMessage _SwitchMessage;
    private bool _IsActive;

    public int _LeverID;

    bool Collided;



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

    public void Flip()
    {
        _IsActive = true;
        _SwitchMessage.Construct(_LeverID, _IsActive);
    }


    // Update is called once per frame
    void Update()
    {

        if (Collided && Input.GetButtonDown("Submit"))
        {
            Flip();
        }


    }
}
