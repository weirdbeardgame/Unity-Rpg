using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used to tell if starting target is PLAYERS or ENEMIES
// Ie. If player is attacking they're probably attacking an enemy ;)
// This can change if player or enemy is confused
public enum TargetRow
{
    PLAYERS = 0,
    ENEMIES = 1
}

// Use Dependancy Injection!!
// Think of a Query for active units.

public class Target : MonoBehaviour
{
    [SerializeField]
    GameObject selectionArrow;

    [SerializeField]
    Dictionary<TargetRow, List<BattlerFloor>> targets;

    TargetRow row;

    // This would make Target a global in the scene. One targeter for everyone etc.
    // public static Target targetInstance;

    ActionIface skill;
    // Start is called before the first frame update
    private void Awake() 
    {
        /*if (targetInstance == null)
        {
            targetInstance = this;
        }*/
    }

    public void Init(List<BattlerFloor> player, List<BattlerFloor> enemy)
    {
        targets = new Dictionary<TargetRow, List<BattlerFloor>>();
        targets.Add(TargetRow.PLAYERS, player);
        targets.Add(TargetRow.ENEMIES, enemy);

        //Instantiate(selectionArrow, Vector3.zero, Quaternion.identity, targets[row][0].transform);
    }

    // To make a targerter per player my guy
    public ActionIface TargetIndex(ActionIface action, TargetRow t, List<BattlerFloor> enemy = default, List<BattlerFloor> player = default)
    {
        row = t;
        skill = action;
        if (targets == null)
        {
            targets.Add(TargetRow.PLAYERS, player);
            targets.Add(TargetRow.ENEMIES, enemy);
        }
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
            if (row < TargetRow.ENEMIES)
            {
                row = TargetRow.ENEMIES;
            }
        }
        if (Input.GetButtonDown("Down"))
        {
            if (row > TargetRow.PLAYERS)
            {
                row = TargetRow.PLAYERS;
            }
        }
        if (Input.GetButtonDown("Submit"))
        {
            skill.target = targets[row][index].fighter;
        }
        return skill;
    }

    public ActionIface TargetIndex(ActionIface action, TargetRow t)
    {
        row = t;
        skill = action;
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
            if (row < TargetRow.ENEMIES)
            {
                row = TargetRow.ENEMIES;
            }
        }
        if (Input.GetButtonDown("Down"))
        {
            if (row > TargetRow.PLAYERS)
            {
                row = TargetRow.PLAYERS;
            }
        }
        if (Input.GetButtonDown("Submit"))
        {
            skill.target = targets[row][index].fighter;
        }
        return skill;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
