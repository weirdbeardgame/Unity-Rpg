using menu;
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class MenuControls : MonoBehaviour
{
    StateMachine state;
    MenuManager manager;

    // Start is called before the first frame update
    void Start()
    {
        state = FindObjectOfType<StateMachine>();
        manager = FindObjectOfType<MenuManager>();
    }

    public void OnMove(InputAction.CallbackContext input)
    {
        if (state.State == States.PAUSE)
        {
            // Grid or list movement
            switch(manager.cApp.display)
            {
                case MenuDisplay.GRID:
                    // Read x an y for true grid movement counting diagonals. We need to check which Widget we're looking at from there though this could easily just be the 2d array or list being moved in
                    Vector2 posGrid = input.ReadValue<Vector2>();
                    manager.Move(posGrid);
                    break;
                case MenuDisplay.LIST:
                    // Vertical list lol
                    float posList = input.ReadValue<Vector2>().y;
                    manager.Move(new Vector2(0, posList));
                    break;
            }
        }
    }

    public void OnAccept(InputAction.CallbackContext input)
    {
        if (input.action.triggered)
        {
            manager.Accept();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
