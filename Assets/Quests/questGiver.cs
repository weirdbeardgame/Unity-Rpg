using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using questing;


public class questGiver : ScriptableObject
{
    [SerializeField]
    public int questID;
    QuestManager quests;
    questBook book;
    NPCData _NPC;

    public bool hasQuest;
    bool trigger;
    public bool isGiven;

    void Give()
    {


        book.Give(quests.Get(questID));
        book.Activate(questID);
        isGiven = true;
    }

}
