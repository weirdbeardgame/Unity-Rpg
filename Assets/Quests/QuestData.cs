﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

// This holds objectives and events whilest tracking the overall state and progress of said quest.
namespace Questing
{
    public enum GoalType { KILL, COLLECT, LOCATION, FIND}
    public enum QuestState { IS_ACTIVE, NOT_ACTIVE, COMPLETED };


    // This belongs on a linked list! Or rather this is a node in a list or tree or graph depending on linearity of game
    public struct Objective
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
        public QuestData()
        {
            flag = null;
            questID = 0;
            questName = " ";
            questProgress = 0;

            flagRequirement = FlagReqSet.SET;
            questState = QuestState.NOT_ACTIVE;
        }
        public QuestData (QuestData data)
        {
            questID = data.questID;
            questProgress = data.questProgress;

            questName = data.questName;
            nonActiveDescription = data.nonActiveDescription;

            flag = data.flag;
            flagRequirement = data.flagRequirement;

            questState = data.questState;
        }

        public int questID;
        public int questProgress;

        public string questName;
        public string activeDescription;
        public string nonActiveDescription;

        NPCData starter;
        NPCData finisher;

        // If there's any scripted events in the quest. IE. A story related quest that might activate a cutscene
        public List<QuestEvent> events;

        // List of quest objectives. Think of these as nodes in a linked list or tree. List for the fact this is linear
        public List<Objective> objectives;

        // Flags required and flags to set. Like an elder scrolls level of depth potentially
        // Though i'm limiting it to make a linear game
        public List<Flags> flag; // Multiple flags set and requiured?
        public FlagReqSet flagRequirement;

        public QuestState questState;

        // Rewards
        public List<Item> reward;
        int xp;
    }
}

