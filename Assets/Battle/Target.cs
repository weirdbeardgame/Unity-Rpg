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

// Use Dependancy Injection !!

// How does this stuff get activated I wonder.
public class Target : MonoBehaviour
{
    [SerializeField]
    GameObject selectionArrow;
    Dictionary<TargetRow, List<BattlerFloor>> targets;

    TargetRow row;

    public static Target targetInstance;

    ActionIface skill;
    // Start is called before the first frame update
    private void Awake() 
    {
        if (targetInstance == null)
        {
            targetInstance = this;
        }
    }

    public void Init(List<BattlerFloor> player, List<BattlerFloor> enemy, ActionIface action, TargetRow t)
    {
        skill = action;
        row = t;

        targets = new Dictionary<TargetRow, List<BattlerFloor>>();
        targets.Add(TargetRow.PLAYERS, player);
        targets.Add(TargetRow.ENEMIES, enemy);

        Instantiate(selectionArrow, Vector3.zero, Quaternion.identity, targets[row][0].transform);
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
