using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlerFloors : MonoBehaviour
{
    [SerializeField]
    List<BattlerFloor> slots;

    BattlerFloor newSlot;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void createSlots(Creature c, GameObject prefab)
    {
        if (slots != null && slots.Count > 0)
        {
            foreach (var slot in slots)
            {
                if (slot.type == c.tag)
                {
                    slot.Insert(c, prefab);
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
