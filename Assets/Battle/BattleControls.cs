using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleControls : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public int index()
    {
        return (int)Input.GetAxisRaw("Vertical");
    }

    public bool submit()
    {
        return Input.GetButtonDown("Submit");
    }

    public bool back()
    {
        return Input.GetButtonDown("Back");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
