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
    public int QuestID;
    public bool HasQuest;
    public string NpcName;
    public string SpritePath;
    List<Waypoint> Waypoints;
    List<QuestEventData> EventData;
    public QuestEventData Event;
    public SpeakerData Speaker;
    public Waypoint Waypoint;
    public NPCData()
    {
        NpcID = 0;
        QuestID = 0;
        SpritePath = " ";
        NpcName = " ";
        HasQuest = false;
        Waypoints = null;
        EventData = null;
        Waypoint = null;
        Speaker = null;
        Event = null;
    }

    public void AddEvent()
    {
        if (EventData == null)
        {
            EventData = new List<QuestEventData>();
        }

        if (!EventData.Contains(Event))
        {
            EventData.Add(Event);
        }

        else
        {
            Debug.LogError("Event Exsts!!");
        }
    }
}