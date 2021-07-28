
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlotPosition { FRONT, BACK };

struct Slot
{
    int id;
    // Also known as the battle player's prefab
    public GameObject slotInstance;
    BattleTag type;
    Creature fighter;

    public Slot createSlot(int slID, BattleTag sType, Creature c)
    {
        id = slID;
        type = sType;
        fighter = c;
        return this;
    }

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
    Dictionary<SlotPosition, Slot> battleSlots;
    Slot newSlot;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void createSlots(SlotPosition pos, BattleTag type, Creature c, int requestedSlotCount)
    {
        battleSlots = new Dictionary<SlotPosition, Slot>();
        for (int i = 0; i < requestedSlotCount; i++)
        {
            newSlot = new Slot();
            newSlot = newSlot.createSlot(i, type, c);
            battleSlots.Add(pos, newSlot);
        }
    }

    public void move(SlotPosition pos, Creature c, BattleTag t)
    {
        battleSlots[pos].moveSlot(t, c);
    }

    public void destroySlot(SlotPosition pos)
    {
        battleSlots[pos].destroySlot();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
