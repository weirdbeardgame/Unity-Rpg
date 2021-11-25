
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is supposed to represent where the player's are standing in the scene.
// They can be in the front of the line or the back which will offer them more defense but less ability to act.
// Using this as a Dictionary as it was prior was and is wrong
public enum SlotPosition { FRONT, BACK };
public enum BattleTag { PLAYER, ENEMY };

// If elemental effect is appiled.
public enum FloorElement { FIRE, ICE, HOLE, NORMAL, HEAL };

public class BattlerFloor : MonoBehaviour 
{
    int id;

    // If is false. Slot is broken
    bool isAlive;

    // Also known as the battle player's prefab
    public GameObject battler;

    [SerializeField]
    public BattleTag type;

    Creature fighter;

    // For combo attacks or when the slot breaks.
    Creature secondary;

    public void Insert(Creature c, GameObject prefab)
    {
        if (c.tag == type && fighter != c)
        {
            battler = prefab;
            fighter = c;
            battler.transform.SetParent(this.transform);
            battler.transform.localPosition = Vector2.zero;
        }
    }

    // This function is to allow players to move on the scene if needed. IE, if their footing or ground breaks
    public void moveSlot(BattlerFloor slot, Creature c, GameObject prefab)
    {
        // Need to clear the slot we're moving from.
        slot.Insert(c, prefab);
    }

    // The idea would be to include logic that removes the Slot from the list and moves the player on it from the slot to another one
    public void destroySlot(BattlerFloor toMoveTo)
    {
        isAlive = false;
        // ToDo: Decide logic for A. Checking if slots avalible. B. Moving player from there.
        moveSlot(toMoveTo, fighter, battler);
    }
}
