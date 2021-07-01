using menu;
using UnityEngine;
using System.Collections;
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

    // Update is called once per frame
    void FixedUpdate()
    {
        if (state.State == States.PAUSE)
        {
            // Grid or list movement
            switch(manager.cApp.display)
            {
                case MenuDisplay.GRID:
                    // Read x an y for true grid movement counting diagonals. We need to check which Widget we're looking at from there though this could easily just be the 2d array or list being moved in
                    Vector2 posGrid = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                    manager.Move(posGrid);
                    break;
                case MenuDisplay.LIST:
                    // Vertical list lol
                    int posList = (int)Input.GetAxisRaw("Vertical");
                    manager.Move(new Vector2(0, posList));
                    break;
            }

            if (Input.GetButtonDown("Submit"))
            {
                manager.Accept();
            }
        }
    }
}
