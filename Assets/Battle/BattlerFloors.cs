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

    public void createSlots(Dictionary<int, Player> c, GameObject prefab)
    {
        if (slots != null && slots.Count > 0)
        {
            for (int i = 0; i < c.Count; i++)
            {
                if (slots[i].type == c[i].Data.tag && slots[i].battler == null)
                {
                    slots[i].Insert(c[i].Data, c[i].prefab);
                }
            }
        }
    }
    public void createSlots(List<Baddies> c, GameObject prefab)
    {
        if (slots != null && slots.Count > 0)
        {
            for (int i = 0; i < c.Count; i++)
            {
                if (slots[i].type == c[i].Data.tag && slots[i].battler == null)
                {
                    slots[i].Insert(c[i].Data, prefab);
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
