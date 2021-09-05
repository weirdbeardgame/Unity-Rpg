using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Questing;
public class QuestGiver : ScriptableObject
{
    [SerializeField]
    public int questID;
    QuestManager quests;
    QuestBook book;
    //NPCData npc; // An older refrence to "quest starter"

    public bool hasQuest;
    bool trigger;
    public bool isGiven;

    void Give()
    {
        book.Give(quests.Get(questID));
        book.Activate(quests.Get(questID));
        isGiven = true;
    }

}
