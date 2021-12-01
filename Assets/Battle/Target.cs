using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Use Dependancy Injection!!

public class Target : MonoBehaviour
{
    [SerializeField]
    GameObject selectionArrow;
    List<Creature> targets;
    List<Creature> secondTargets;

    ActionIface skill;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Init(List<Creature> toTargetStart, List<Creature> secondaryTargets, ActionIface action)
    {
        skill = action;
        targets = toTargetStart;

        Instantiate(selectionArrow, Vector3.zero, Quaternion.identity, toTargetStart[0].prefab.transform);
    }

    public ActionIface TargetIndex()
    {
        int index = 0;
        if (Input.GetButtonDown("Right"))
        {
            index += 1;
        }
        else if (Input.GetButtonDown("Left"))
        {
            index -= 1;
        }

        // Changing Row to target
        if (Input.GetButtonDown("Up"))
        {
            
        }
        if (Input.GetButtonDown("Down"))
        {
            
        }

        if (Input.GetButtonDown("Submit"))
        {
            skill.target = targets[index];
        }

        return skill;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
