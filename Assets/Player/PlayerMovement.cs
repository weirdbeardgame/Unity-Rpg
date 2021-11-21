
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public Animator animate;
    public Rigidbody2D body;
    public bool canMove = true;

    Vector2 position;

    StateMachine state;

    Animator animator;

    UIManager ui;

    public float playerSpeed = 4f;

    Vector2 v2;

    Tilemap tilemap;

    void Start()
    {
        state = FindObjectOfType<StateMachine>();
        state.StateChangeEvent += CanMove;
        tilemap = FindObjectOfType<Tilemap>();
        animator = GetComponent<Animator>();
        ui = FindObjectOfType<UIManager>();

        DontDestroyOnLoad(this);
    }

    public void CanMove(States s)
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
            switch (state.State)
            {
            case States.MAIN:
                ui.Open(0);
                break;
            case States.PAUSE:
                ui.Close();
                break;
            }
        }
        animate.SetFloat("Horizontal", v2.x);
        animate.SetFloat("Vertical", v2.y);
        animate.SetFloat("Speed", v2.sqrMagnitude);
    }
}