
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is supposed to represent where the player's are standing in the scene.
// They can be in the front of the line or the back which will offer them more defense but less ability to act.
// Using this as a Dictionary as it was prior was and is wrong
public enum SlotPosition { FRONT, BACK };
public enum BattleTag { PLAYER, ENEMY }

struct Slot
{
    int id;

    // Also known as the battle player's prefab
    public GameObject battler;

    [SerializeField]
    public BattleTag type;

    Creature fighter;

    // For combo attacks or when the slot breaks.
    Creature secondary;

    void Insert(Creature c, GameObject prefab)
    {
        if (c.tag == type && fighter != c)
        {
            battler = prefab;
            fighter = c;
        }
    }

    // This function is to allow players to move on the scene if needed. IE, if their footing or ground breaks
    public void moveSlot(BattleTag tag, Creature c)
    {
        type = tag;
        fighter = c;
    }

    // The idea would be to include logic that removes the Slot from the list and moves the player on it from the slot to another one
    public void destroySlot()
    {

    }
}
public class BattleSlots : MonoBehaviour
{
    List<Slot> slots;

    Slot newSlot;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void createSlots(Creature c)
    {
        if (slots != null && slots.Count > 0)
        {
            foreach (var slot in slots)
            {
                if (slot.type == c.tag)
                {

                }
            }
        }
    }

    public void move(SlotPosition pos, Creature c, BattleTag t)
    {
        //slots[pos].moveSlot(t, c);
    }

    public void destroySlot(SlotPosition pos)
    {
        //slots[pos].destroySlot();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
