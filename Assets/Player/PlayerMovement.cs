
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using menu;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public Animator animate;
    public Rigidbody2D body;
    public bool canMove = true;

    Messaging messenger;

    Queue<InputData> inbox;

    float horizontal;
    float vertical;

    Vector2 position;

    StateMachine state;
    MenuManager manager;

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

    void Start()
    {
        state = FindObjectOfType<StateMachine>();
        tilemap = FindObjectOfType<Tilemap>();
        messenger = FindObjectOfType<Messaging>();
        manager = FindObjectOfType<MenuManager>();
        animator = GetComponent<Animator>();

        DontDestroyOnLoad(this);
    }

    void Update()
    {
        switch(state.State)
        {
            case States.MAIN:
            canMove = true;
            break;

            default:
            canMove = false;
            break;
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

        if (Input.GetButtonDown("Cancel"))
        {
            if (!manager.isOpen)
            {
                manager.Open(0);
            }
            else
            {
                manager.Close();
            }
        }

        animate.SetFloat("Horizontal", v2.x);
        animate.SetFloat("Vertical", v2.y);
        animate.SetFloat("Speed", v2.sqrMagnitude);
    }
}