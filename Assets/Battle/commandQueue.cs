using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandQueue : MonoBehaviour
{
    private List<ActionIface> _Commands;

    public List<ActionIface> Commands
    {
        get
        {
            return _Commands;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _Commands = new List<ActionIface>();
    }

    public void Enqueue(ActionIface Action, int i = 0)
    {
        if (_Commands == null)
        {
            _Commands = new List<ActionIface>();
        }

        /*if (i > 0)
        {
            _Commands[i] = Action; // Enqueue Action above slow persons.
        }*/

        else
        {
            _Commands.Add(Action);
        }
    }

    public ActionIface Dequeue()
    {
        if (_Commands.Count > 0)
        {
            ActionIface Temp = _Commands[0];
            _Commands.RemoveAt(0);
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
        if (_Commands.Count > 0)
        {
            return _Commands[0];
        }

        else
        {
            return null;
        }
    }

    public int Size()
    {
        return _Commands.Count;
    }
}
