using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This holds objectives and events whilest tracking the overall state and progress of said quest.
namespace questing
{
    public class QuestData
    {
        public string QuestName;
        public string Description;

        public Flags RequiredFlag;
        public Flags FlagToSet;

        public QuestType Type;

        [System.NonSerialized]
        public QuestState QuestState;

        public int ItemCountNeeded;
        public int QuestID;

        public int ActiveObjective;

        public NPCData QuestGiverID;
        public NPCData QuestCompleterID;

        public List<Item> Reward;
        public List<QuestObjective> Objectives;
        public List<QuestEvent> Events;
    }
}

