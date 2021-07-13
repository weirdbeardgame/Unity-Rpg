using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionWidget : Widget
{

    [SerializeField] // Not sure this is the right idea but... Meh for now
    private ActionIface action;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Execute()
    {
        base.Execute();
        // Slot action into character for targeter to activate
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
