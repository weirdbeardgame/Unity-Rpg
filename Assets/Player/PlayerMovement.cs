
using menu;
using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public Animator animate;
    public Rigidbody2D body;
    public bool canMove = true;
    float horizontal;
    float vertical;

    Vector2 movement;

    StateMachine state;

    Animator animator;

    MenuManager menu;

    string mapLoad = null;
    int index = 0;
    int X = 0;
    int Y = 0;
    int i = 0;
    public float playerSpeed = 4f;

    input recievedInput;

    void Start()
    {
        state = FindObjectOfType<StateMachine>();
        menu = FindObjectOfType<MenuManager>();
        body = GetComponent<Rigidbody2D>();
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

    public void OnMove(InputAction.CallbackContext input)
    {
        if (input.action.triggered && canMove)
        {
            movement = new Vector2(input.ReadValue<Vector2>().x, input.ReadValue<Vector2>().y);
            body.velocity = (movement * playerSpeed);
            animate.SetFloat("Horizontal", movement.x);
            animate.SetFloat("Vertical", movement.y);
            animate.SetFloat("Speed", movement.sqrMagnitude);
        }
        else
        {
            body.velocity = Vector2.zero;
        }
    }

        public void OnAccept(InputAction.CallbackContext input)
        {
            // Check if near NPC. Go from there to execute their held event.
        }

    public void OnOpenMenu(InputAction.CallbackContext input)
    {
        if (input.action.triggered)
        {
            Debug.Log("PRESSED MENU");
            // Open Menu
            //PlayerInput
            menu.Open(0);
        }
    }
}