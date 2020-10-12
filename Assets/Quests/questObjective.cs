using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace questing
{
    public enum QuestObjectiveType { KILL, COLLECT }

    public class QuestObjective
    {
        public string Name;
        public string Description;

        private List<Flags> Flags; // Required, Set
        private FlagReqSet IsRequired;
        private QuestObjectiveType ObjectiveType;

        public int ObjectiveID;

        public List<ItemData> RequiredItems;

        int MaxAmount = 0;

        public QuestObjectiveType Type
        {
            get
            {
                return ObjectiveType;
            }

            set
            {
                ObjectiveType = value;
            }
        }

        public List<Flags> FlagData
        {
            get
            {
                return Flags;
            }

            set
            {
                Flags = value;
            }
        }
    }
}