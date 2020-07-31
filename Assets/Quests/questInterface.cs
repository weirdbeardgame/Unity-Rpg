using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace questing
{
    public enum QuestType { COLLECT, KILL, RESCUE }; // The represents the whole of the overaching quest, not the objectives themselves nessacarily. 
    public enum QuestState { IS_ACTIVE, NOT_ACTIVE, COMPLETED };

    public interface questInterface
    {
        void Progress(int id);
        void Complete(int id);
    }
}