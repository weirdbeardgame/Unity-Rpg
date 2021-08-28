using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

// This holds objectives and events whilest tracking the overall state and progress of said quest.
namespace questing
{
    public enum GoalType { KILL, COLLECT, LOCATION, FIND}
    public enum QuestState { IS_ACTIVE, NOT_ACTIVE, COMPLETED };

    struct Objective
    {
        GoalType goal;
        bool isComplete;
        int objectiveProgress;
        int requiredProgress;

        // Objectives to check against
        [Header("Objectives")]
        [SerializeField] private Item itemToCollect;
        [SerializeField] private Baddies enemyToKill;
        [SerializeField] private Scene locationToGo;

        // Find feels more like a category rather then a set quest
        [Header("Find Quest")]
        [SerializeField] private NPC npcToFind;
        [SerializeField] private Item itemToFind;
        [SerializeField] private Weapon weaponToFind;

        // Goals
        public Baddies killed;
        public Item collected;

        /********************************************************************************************
        * I can look for a posted event. Or, I can have this directly called somehow from each class.
        * When that quest type is active or some shit like that. But really... I want this called from
        * The central game manager class somewhere and that carries the message through to here.
        * Or, this is in the QuestManager proper and that's polling everything along the line
        **********************************************************************************************/
        public void progress()
        {
            switch(goal)
            {
                case GoalType.KILL:
                    if (objectiveProgress < requiredProgress)
                    {
                        if (enemyToKill == killed)
                        {
                            objectiveProgress += 1;
                        }
                    }
                else if (objectiveProgress >= requiredProgress)
                {
                    complete();
                }
                    break;
                case GoalType.COLLECT:
                    if (objectiveProgress < requiredProgress)
                    {
                        if (itemToCollect == collected)
                        {
                            objectiveProgress += 1;
                        }
                    }
                    break;
                case GoalType.FIND:
                    /*if (toFind == isFound)
                    {
                    }*/
                    break;
            }
        }

        void complete()
        {
            isComplete = true;
        }
    }

    public class QuestData
    {
        public float questProgress;
        public int QuestID;
        public string QuestName;
        public string Description;
        public List<Flags> Flag; // Multiple flags set and requiured?
        public FlagReqSet FlagRequirement;
        public QuestState QuestState;
        // Rewards
        public List<Item> Reward;
        public List<QuestEvent> events;
    }
}

