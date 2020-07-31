using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    DialogueManager Manager;
    public int Message;
    bool Collided;

    // Start is called before the first frame update
    void Start()
    {
        Manager = FindObjectOfType<DialogueManager>();
    }

    void Talk()
    {

        if (Collided == true)
        {
            Manager = FindObjectOfType<DialogueManager>();

            Debug.Log("Trigger Active");

            Manager.Talk(Message);
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

    // Update is called once per frame
    void Update()
    {
        if (Collided && Input.GetButtonDown("Submit"))
        {
            Talk();
            Collided = false;
        }

    }
}
