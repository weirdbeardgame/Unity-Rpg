using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using UnityEngine.AI;
using System;

// The collection of what an NPC is in the system

using questing;

public enum QuestFlag { HAS_QUEST, QUEST_ACTIVE };

public class NPCData
{
    public int NpcID;
    public int QuestID;

    public bool HasQuest;

    public string NpcName;
}