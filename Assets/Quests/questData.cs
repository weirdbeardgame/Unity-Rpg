using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This holds objectives and events whilest tracking the overall state and progress of said quest.
namespace questing
{
    public enum QuestState { IS_ACTIVE, NOT_ACTIVE, COMPLETED };
    public struct QuestData
    {
        public int QuestID;

        public string QuestName;
        public string Description;

        public List<Flags> Flag; // Multiple flags set and requiured?
        public FlagReqSet FlagRequirement;

        QuestObjective ActiveObjective;

        public QuestState QuestState;

        // Rewards
        public List<Item> Reward;
        public List<QuestObjective> Objectives;
        //public List<QuestEvent> Events; Not sure if this is where events belong. Events are compolex interaction between the engine and the player. Perhaps a touch too complex to be held here
        // If not here then the mystery is where
    }
}

