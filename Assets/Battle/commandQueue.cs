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

    public void Enqueue(ActionIface Action, int i = 0)
    {
        if (commands == null)
        {
            commands = new List<ActionIface>();
        }

        /*if (i > 0)
        {
            commands[i] = Action; // Enqueue Action above slow persons.
        }*/

        else
        {
            commands.Add(Action);
        }
    }

    public ActionIface Dequeue()
    {
        if (commands.Count > 0)
        {
            ActionIface Temp = commands[0];
            commands.RemoveAt(0);
            return Temp;
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
