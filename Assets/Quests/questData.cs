using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This holds objectives and events whilest tracking the overall state and progress of said quest.
namespace questing
{
    public enum QuestType { COLLECT, KILL, RESCUE }; // The represents the whole of the overaching quest, not the objectives themselves nessacarily. 
    public enum QuestState { IS_ACTIVE, NOT_ACTIVE, COMPLETED };
    public class QuestData
    {
        public string QuestName;
        public string Description;

        public Flags Flag;

        public FlagReqSet FlagRequirement;

        public QuestType Type;

        public QuestState QuestState;

        public int QuestID;

        public int ActiveObjective;

        // Rewards
        public List<Item> Reward;
        public List<QuestObjective> Objectives;
        public List<QuestEvent> Events;
    }
}

