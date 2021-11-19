
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
    public GameObject slotInstance;

    BattleTag type;

    Creature fighter;

    // For combo attacks or when the slot breaks.
    Creature secondary;

    public Slot createSlot(int slID, BattleTag sType, Creature c)
    {
        id = slID;
        type = sType;
        fighter = c;
        return this;
    }

    // This function is to allow players to move on the scene if needed. IE, if their footing or ground breaks
    public void moveSlot(BattleTag tag, Creature c)
    {
        type = tag;
        fighter = c;
    }

    public void destroySlot()
    {
        id = 0;
        slotInstance = null;
        type = 0;
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

    public void createSlots(SlotPosition pos, BattleTag type, Creature c, int requestedSlotCount)
    {
        slots = new List<Slot>();
        for (int i = 0; i < requestedSlotCount; i++)
        {
            newSlot = new Slot();
            newSlot = newSlot.createSlot(i, type, c);
            slots.Add(newSlot);
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
