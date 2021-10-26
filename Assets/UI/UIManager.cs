using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    bool isOpen;
    public GameObject pScreen;
    public List<GameObject> menuPrefabs;
    StateMachine gameState; // Need to pass as events rather than direct call

    // Publish a state change as an event
    public delegate StateChangeEventArgs stateDelagate(States s);
    public event stateDelagate stateEvent;

    // Start is called before the first frame update
    void Start()
    {
        gameState = FindObjectOfType<StateMachine>();
    }

    public void Open(int index)
    {
        if (!isOpen)
        {
            pScreen.SetActive(true);
            pScreen = Instantiate(menuPrefabs[0], pScreen.transform, false);
            pScreen.transform.localPosition = new Vector3(0, 0, 0);
            gameState.SetState(States.PAUSE);
            isOpen = true;
        }
        else
        {
            pScreen = Instantiate(menuPrefabs[index], pScreen.transform, false);
            pScreen.transform.localPosition = new Vector3(0, 0, 0);
        }
    }

    public void Close()
    {
        if (pScreen && isOpen)
        {
            pScreen.SetActive(false);
            //Destroy(pScreen);
            isOpen = false;
            gameState.SetState(States.MAIN);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
