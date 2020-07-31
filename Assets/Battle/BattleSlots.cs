
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum SlotPosition { FRONT, BACK };

public class BattleSlots : MonoBehaviour
{
    private GameObject[] _Slots;
    private SlotPosition _Position;
    private BattleTag _SlotType;

    // Start is called before the first frame update
    void Start()
    {

    }

    void CreateSlots(SlotPosition Pos, BattleTag Type, int SlotCount)
    {
        _Slots = new GameObject[SlotCount];
        _Position = Pos;
        _SlotType = Type;
    }



    // Update is called once per frame
    void Update()
    {

    }
}
