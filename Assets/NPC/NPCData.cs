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

public class NPCData : ScriptableObject
{

    public int NpcID;
    public string NpcName;
    public string NpcEventPath;

    public GameObject CurrentSpeaker;

    [System.NonSerialized]
    List<Waypoint> Waypoints;

    [System.NonSerialized]
    public List<QuestEventData> EventData; // Quest Event Data is here

    [System.NonSerialized]
    public SpeakerData Speaker;
    [System.NonSerialized]
    public Waypoint Waypoint;
    
    public NPCData()
    {
        NpcID = 0;
        NpcName = " ";
        Waypoints = null;
        EventData = null;
        Waypoint = null;
        Speaker = null;
    }

    public NPCData Deserialize()
    {
        NPCData ToConstruct = new NPCData();

        return ToConstruct;
    }
}