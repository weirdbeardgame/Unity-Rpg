
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum SlotPosition { FRONT, BACK };

public class BattleSlots : MonoBehaviour
{
    private GameObject[] slots;
    private SlotPosition position;
    private BattleTag slotType;

    // Start is called before the first frame update
    void Start()
    {

    }

    void CreateSlots(SlotPosition Pos, BattleTag Type, int SlotCount)
    {
        slots = new GameObject[SlotCount];
        position = Pos;
        slotType = Type;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
