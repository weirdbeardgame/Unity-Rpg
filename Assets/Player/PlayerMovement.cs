using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

using menu;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour, IReceiver
{
    public Animator animate;
    public Rigidbody2D body;
    public bool canMove = true;

    Messaging messenger;

    Queue<Inputs> inbox;

    float horizontal;
    float vertical;

    Vector2 position;

    StateMachine state;

    //Pause pause;

    Animator animator;

    string mapLoad = null;
    int index = 0;
    int X = 0;
    int Y = 0;
    int i = 0;

    public float playerSpeed = 4f;

    Vector2 v2;

    Tilemap tilemap;

    input recievedInput;

    public void Receive(object message)
    {
        inbox.Enqueue((Inputs)message);
    }

    public void Subscribe()
    {
        //messenger.Subscribe(MessageType.INPUT, this);
    }

    public void Unsubscribe()
    {
        //messenger.Unsubscribe(MessageType.INPUT, this);
    }

    void Start()
    {
        state = FindObjectOfType<StateMachine>();
        tilemap = FindObjectOfType<Tilemap>();
        //pause = FindObjectOfType<Pause>();

        messenger = FindObjectOfType<Messaging>();


        animator = GetComponent<Animator>();

        inbox = new Queue<Inputs>();

        DontDestroyOnLoad(this);
    }

    void Update()
    {
        if (state.State == States.MAIN)
        {
            canMove = true;
        }

        if (state.State == States.BATTLE)
        {
            canMove = false;
        }

        if (state.State == States.CUTSCENE)
        {
            canMove = false;
        }
        else if (state.State == States.PAUSE)
        {
            canMove = false;
        }
    }

    void FixedUpdate()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (canMove)
        {
            v2 = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            GetComponent<Rigidbody2D>().velocity = v2 * playerSpeed;
        }

        else if (!canMove)
        {
            v2 = new Vector2(0, 0);
            GetComponent<Rigidbody2D>().velocity = v2 * 0.0f;
        }


        /*if (canMove)
        {

            if (inbox.Count > 0)
            {
                switch (inbox.Dequeue())
                {
                    case inputs.UP:
                        v2 = new Vector2(0, 1);
                        GetComponent<Rigidbody2D>().velocity = v2 * playerSpeed;
                        break;

                    case inputs.DOWN:
                        v2 = new Vector2(0, -1);
                        GetComponent<Rigidbody2D>().velocity = v2 * playerSpeed;
                        break;

                    case inputs.LEFT:
                        v2 = new Vector2(-1, 0);
                        GetComponent<Rigidbody2D>().velocity = v2 * playerSpeed;
                        break;

                    case inputs.RIGHT:
                        v2 = new Vector2(1, 0);
                        GetComponent<Rigidbody2D>().velocity = v2 * playerSpeed;
                        break;
                }
            }

            else
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }

        }*/


        animate.SetFloat("Horizontal", v2.x);
        animate.SetFloat("Vertical", v2.y);
        animate.SetFloat("Speed", v2.sqrMagnitude);
    }
}