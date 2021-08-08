using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UI;
using Newtonsoft.Json;
using System.IO;
using System;


[System.Serializable]
public class DialogueNode : IComparable<DialogueNode>
{
    private int nodeID;
    public int treeID;

    int flagToSet = 0;
    int flagType = 0;
    int flagChoice1 = 0;
    int flagChoice2 = 0;
    int setSpeaker = 0;

    string jsonData;

    [System.NonSerialized]
    public Rect node;

    string FilePath = "Assets/NPC/NPC.json";

    List<NPCData> speakerSelection;
    List<String> speakerName;

    NodeType nType;

    private Vector2 nodeSize;

    public float posX, posY;

    private string nodeTitle;

    public int npcId;
    public int quest;

    [System.NonSerialized]
    private GUIStyle nodeStyle;

    private bool isDragged;

    public DialogueMessage dNode;

    [System.NonSerialized]
    public Action<DialogueMessage> onRemoveNode;

    public void CreateNode(string NodeTitle, Vector2 Position, float SizeW, float SizeH, GUIStyle NodeStyle,
    GUIStyle inPointStyle, GUIStyle outPointStyle, Action<DialogueMessage> RemoveNode, ref int ID, NodeType Type)
    {
        nodeStyle = NodeStyle;
        nodeTitle = NodeTitle;

        nodeID = ID;

        dNode = new DialogueMessage();
        dNode.ID = nodeID;

        dNode.NodeT = Type;

        if (File.Exists(FilePath))
        {
            jsonData = File.ReadAllText(FilePath);
            speakerSelection = JsonConvert.DeserializeObject<List<NPCData>>(jsonData);

            speakerName = new List<string>();
            dNode.SpeakerID = new NPCData();

            for (int i = 0; i < speakerSelection.Count; i++)
            {
                speakerName.Add(speakerSelection[i].NpcName);
            }
        }

        onRemoveNode = RemoveNode;

        setSpeaker = dNode.SpeakerID.NpcID;

        node = new Rect(Position.x, Position.y, SizeW, SizeH);
        ID += 1;
        return;
    }


    public void CreateNode(string NodeTitle, Vector2 Position, float SizeW, float SizeH, GUIStyle NodeStyle,
        GUIStyle inPointStyle, GUIStyle outPointStyle, Action<DialogueMessage> RemoveNode, ref int ID, DialogueMessage MNode, NodeType Type)
    {
        nodeStyle = NodeStyle;
        nodeTitle = NodeTitle;

        nodeID = ID;

        dNode = new DialogueMessage();
        dNode = MNode;

        dNode.NodeT = Type;        
        if (File.Exists(FilePath))        
        {        
            jsonData = File.ReadAllText(FilePath);                    
            speakerSelection = JsonConvert.DeserializeObject<List<NPCData>>(jsonData);
         
            speakerName = new List<string>();                   
            dNode.SpeakerID = new NPCData();
        
            for (int i = 0; i < speakerSelection.Count; i++)
            {
                speakerName.Add(speakerSelection[i].NpcName);                  
            }
        }

        switch (Type)
        {
            case NodeType.DIALOUGE:
                break;

            case NodeType.FLAG:
                dNode.Flag = new Flags();
                flagToSet = 0;
                break;

            case NodeType.CHOICE:
                dNode.Choices = new ChoiceData[2];
                flagChoice1 = 0;
                flagChoice2 = 0;
                break;
        }

        posX = Position.x;
        posY = Position.y;

        onRemoveNode = RemoveNode;

        node = new Rect(Position.x, Position.y, SizeW, SizeH);
        return;
    }


    public void Drag(Vector2 drag)
    {

        posX = drag.x;
        posY = drag.y;


        // Set position based on Mouse Cordinites
        node.position += drag;
    }

    public void Draw(string[] flag, List<Flags> flagData)
    {
        GUI.Box(node, nodeTitle, nodeStyle);

        GUILayout.BeginArea(new Rect(node.x, node.y, 250, 450));
        GUILayout.BeginVertical();

        switch (dNode.NodeT)
        {
            case NodeType.FLAG:
                EditorGUILayout.LabelField("Flag: ");
                flagToSet = dNode.Flag.ID;
                flagToSet = EditorGUILayout.Popup(flagToSet, flag);
                EditorGUILayout.LabelField("Is Required?");
                dNode.FlagType = (FlagReqSet)EditorGUILayout.EnumPopup(dNode.FlagType);
                dNode.Flag = flagData[flagToSet];
                break;

            case NodeType.DIALOUGE:
                EditorGUILayout.LabelField("Speaker ID: ");
                setSpeaker = EditorGUILayout.Popup(setSpeaker, speakerName.ToArray());
                dNode.SpeakerID = speakerSelection[setSpeaker];
                dNode.Line = GUILayout.TextArea(dNode.Line, GUILayout.Height(EditorGUIUtility.singleLineHeight * 5));
                break;

            case NodeType.CHOICE:
                // List Options and flags they set
                EditorGUILayout.LabelField("Options");
                flagChoice1 = EditorGUILayout.Popup(flagChoice1, flag);
                flagChoice2 = EditorGUILayout.Popup(flagChoice2, flag);
                dNode.Choices[0].SetFlag = flagData[flagChoice1];
                dNode.Choices[1].SetFlag = flagData[flagChoice2];
                dNode.Choices[0].ChoiceName = EditorGUILayout.TextField(dNode.Choices[0].ChoiceName);
                dNode.Choices[1].ChoiceName = EditorGUILayout.TextField(dNode.Choices[1].ChoiceName);
                break;
        }

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }

    private void OnClickRemoveNode()
    {
        if (onRemoveNode != null)
        {
            //OnRemoveNode(this);
        }
    }

    private void ProcessContextMenu()
    {
        GenericMenu genericMenu = new GenericMenu();
        genericMenu.AddItem(new GUIContent("Remove node"), false, OnClickRemoveNode);
        genericMenu.ShowAsContext();
    }

    public bool ProcessNodeEvents(Event e)
    {

        switch (e.type)
        {
            case EventType.MouseDown:
                if (e.button == 0) // left clicky
                {
                    if (node.Contains(e.mousePosition))
                    {
                        isDragged = true;
                        GUI.changed = true;
                    }
                }
                break;

            case EventType.MouseUp:
                isDragged = false;
                break;

            case EventType.MouseDrag:
                if (e.button == 0 && isDragged)
                {
                    Drag(e.delta);
                    e.Use();
                    return true;
                }
                break;
        }

        if (e.button == 1 && node.Contains(e.mousePosition))
        {
            ProcessContextMenu();
            e.Use();
        }

        return false;
    }

    public int CompareTo(DialogueNode obj)
    {
        if (nodeID < obj.nodeID)
        {
            return -1;
        }

        if (nodeID > obj.nodeID)
        {
            return 1;
        }

        return 0;
    }



}
