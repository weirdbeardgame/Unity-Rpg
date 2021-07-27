using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace questing
{
    public enum QuestObjectiveType { KILL, COLLECT, NPC }

    public class QuestObjective
    {
        public string Name;
        public string Description;

        //public event QuestObjective;
        public List<Flags> flags // Required, Set
        {
            get
            {
                return flags;
            }
            private set
            {
                flags = value;
            }
        }
        private FlagReqSet IsRequired;
        public QuestObjectiveType objectiveType
        {
            get
            {
                return objectiveType;
            }
            set
            {
                objectiveType = value;
            }
        }
        public int ObjectiveID;
        public List<ItemData> RequiredItems;
        int MaxAmount = 0;
    }
}