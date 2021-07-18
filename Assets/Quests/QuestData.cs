using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This holds objectives and events whilest tracking the overall state and progress of said quest.
namespace questing
{
    public enum QuestState { IS_ACTIVE, NOT_ACTIVE, COMPLETED };
    public class QuestData
    {
        public int QuestID;

        public string QuestName;
        public string Description;

        public List<Flags> Flag; // Multiple flags set and requiured?
        public FlagReqSet FlagRequirement;

        public QuestObjective ActiveObjective;

        public QuestState QuestState;

        // Rewards
        public List<Item> Reward;
        public List<QuestObjective> Objectives;
    }
}

