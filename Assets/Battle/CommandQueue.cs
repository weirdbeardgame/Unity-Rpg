using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandQueue : MonoBehaviour
{
    private List<ActionIface> commands;

    public List<ActionIface> Commands
    {
        get
        {
            return commands;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        commands = new List<ActionIface>();
    }

    public void enqueue(ActionIface action, bool haste = false)
    {
        if (commands == null)
        {
            commands = new List<ActionIface>();
        }
        else
        {
            commands.Add(action);
            // Set Player state!
        }
    }

    public ActionIface dequeue()
    {
        if (commands.Count > 0)
        {
            ActionIface temp = commands[0];
            commands.RemoveAt(0);
            return temp;
        }
        else
        {
            Debug.Log("Error! No Command Enqueued!");
            return null;
        }
    }

    public ActionIface Peek()
    {
        if (commands.Count > 0)
        {
            return commands[0];
        }

        else
        {
            return null;
        }
    }

    public int Size()
    {
        return commands.Count;
    }
}
