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
    public int MapID;
    public int NpcID;
    public int QuestID;

    public bool HasQuest;

    public Vector2 SpritePosition;

    public float X;
    public float Y;

    public string NpcName;
    public string SpritePath;

    public NPCMovement Move;

    public List<int> Dialogue;
    public List<MoveDirections> Directions;

    Animator animator;
    AnimationClip[] clips;

    public QuestFlag Flag;
    public GameObject NPC;

    questBook _QuestBook;
}