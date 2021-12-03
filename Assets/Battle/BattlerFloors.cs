using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlerFloors : MonoBehaviour
{
    [SerializeField]
    List<BattlerFloor> playerSlots;
    [SerializeField]
    List<BattlerFloor> enemySlots;

    public List<BattlerFloor> PlayerSlots
    {
        get
        {
            return playerSlots;
        }
    }

    public List<BattlerFloor> EnemySlots
    {
        get
        {
            return enemySlots;
        }
    }

    BattlerFloor newSlot;

    // Start is called before the first frame update
    void Start()
    {
        playerSlots = new List<BattlerFloor>();
        enemySlots = new List<BattlerFloor>();
    }

    public void createSlots(List<Player> c, GameObject prefab)
    {
        if (playerSlots != null && playerSlots.Count > 0)
        {
            for (int i = 0; i < c.Count; i++)
            {
                if (playerSlots[i].type == c[i].Data.tag && playerSlots[i].battler == null)
                {
                    playerSlots[i].Insert(c[i].Data, c[i].prefab);
                }
            }
        }
    }
    public void createSlots(List<Baddies> c, GameObject prefab)
    {
        if (enemySlots != null && enemySlots.Count > 0)
        {
            for (int i = 0; i < c.Count; i++)
            {
                if (enemySlots[i].type == c[i].Data.tag && enemySlots[i].battler == null)
                {
                    enemySlots[i].Insert(c[i].Data, prefab);
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
