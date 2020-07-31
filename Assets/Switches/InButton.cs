using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InButton : MonoBehaviour
{
    private SwitchMessage _SwitchMessage;
    private bool _IsActive;
    private int _LeverID;

    bool Collided;

    public int LeverID
    {
        get
        {
            return _LeverID;
        }

        set
        {
            _LeverID = value;
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

    public void Press() // I can either have a Message play sound or the event itself
    {
        _IsActive = true;
        _SwitchMessage.Construct(_LeverID, _IsActive);
    }


    // Update is called once per frame
    void Update()
    {

        if (Collided && Input.GetButtonDown("Submit"))
        {
            Press();
        }


    }
}
