using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    UIManager screens;
    // Start is called before the first frame update
    void Start()
    {
        screens = GetComponent<UIManager>();
    }

    // Open Item screen
    public void Items()
    {
        // Items must always be 1
        screens.Open(1);
    }

    // Open Quest log menu
    public void Quests()
    {

    }

    // Open Text Message menu
    public void Messages()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
