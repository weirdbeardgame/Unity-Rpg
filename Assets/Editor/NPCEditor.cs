using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using UnityEngine;
using UnityEditor;
using Questing;

[CustomEditor(typeof(NPCManager))]
public class NPCEditor : Editor
{
    NPCManager Data;
    NPCEventData Event;
    DialogueEditor DEditor;

    bool IsInit;
    int selected;
    int FlagSelected;
    int EventSelector;
    int FlagSelectedSet;
    int questSelected;

    string JsonData;
    string FlagPath = "Assets/Flags.json";
    string FilePath = "Assets/NPC/NPC.json";
    string QuestFilePath = "Assets/Quests/Quest.json";

    Dictionary<int, ItemData> ItemsToCollect;
    
    List<Flags> FlagList;
    List<NPCData> Editable;
    List<QuestData> Quests;
    List<string> NpcNames;
    List<string> ItemNames;
    List<string> EventNames;
    List<string> QuestNames;
    List<string> FlagString;

    JsonSerializerSettings settings = new JsonSerializerSettings
    {
        TypeNameHandling = TypeNameHandling.All,
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
    };

    void ReadJson()
    {
        if (File.Exists(FilePath))
        {
            NpcNames = new List<string>();
            EventNames = new List<string>();
            JsonData = File.ReadAllText(FilePath);
            Editable = JsonConvert.DeserializeObject<List<NPCData>>(JsonData);
            for (int i = 0; i < Editable.Count; i++)
            { 
                if (Editable[i] != null)
                {
                    NpcNames.Add(Editable[i].NpcName);
                }
            }
        }
        for (int i = 0; i < Editable.Count; i++)
        {
            if (File.Exists(Editable[i].NpcEventPath))
            {
                JsonData = null;
                JsonData = File.ReadAllText(Editable[i].NpcEventPath);
                Editable[i].EventData = JsonConvert.DeserializeObject<List<NPCEventData>>(JsonData);

                if (Editable[i].EventData != null)
                {
                    foreach (var ev in Editable[i].EventData)
                    {
                        EventNames.Add(ev.EventName);
                    }
                }

            }
        }
        if (File.Exists(FlagPath))
        {
            JsonData = null;
            FlagList = new List<Flags>();
            FlagString = new List<string>();
            JsonData = File.ReadAllText(FlagPath);
            FlagList = JsonConvert.DeserializeObject<List<Flags>>(JsonData);

            for (int i = 0; i < FlagList.Count; i++)
            {
                FlagString.Add(FlagList[i].Flag);
            }
        }

        if (File.Exists(QuestFilePath))
        {
            Quests = new List<QuestData>();
            QuestNames = new List<string>();
            JsonData = File.ReadAllText(QuestFilePath);
            Quests = JsonConvert.DeserializeObject<List<QuestData>> (JsonData);
            foreach(var QD in Quests)
            {
                QuestNames.Add(QD.questName);
            }
        }
    }

    void Init()
    {
        if (!IsInit)
        {
            ReadJson();
            Data = new NPCManager();
            Data = (NPCManager)target;
            Data.Initalizer = new NPCData();
            IsInit = true;
        }
    }

    public override void OnInspectorGUI()
    {
        Init();

        base.OnInspectorGUI();

        if (GUILayout.Button("New Npc"))
        {
            if (Editable == null)
            {
                Editable = new List<NPCData>();
                NpcNames = new List<string>();
            }

            Data.Initalizer = new NPCData();
            Data.Initalizer.NpcName = "New NPC";
            Data.Initalizer.NpcID = (Editable.Count + 1);
            Editable.Add(Data.Initalizer);
            NpcNames.Add(Data.Initalizer.NpcName);
        } 
        if (NpcNames != null && NpcNames.Count > 0)                   
        {
            selected = EditorGUILayout.Popup(selected, NpcNames.ToArray());     
            Data.Initalizer = Editable[selected];
            EventNames.Clear();
            Event = null;

            if (Data.Initalizer.EventData != null)
            {
                foreach (var ev in Data.Initalizer.EventData)
                {
                    EventNames.Add(ev.EventName);
                }
            }
        }
        if (Data.Initalizer != null)
        {
            
            EditorGUILayout.LabelField("NPC ID");
            Data.Initalizer.NpcID = EditorGUILayout.IntField(Data.Initalizer.NpcID);
            EditorGUILayout.LabelField("NPC Name");
            Data.Initalizer.NpcName = EditorGUILayout.TextField(Data.Initalizer.NpcName);
            Data.Initalizer.NpcEventPath = "Assets/NPC/Events/" + Data.Initalizer.NpcName + ".json";

            if (GUILayout.Button("AddEvent"))
            {
                if (Data.Initalizer.EventData == null)
                {
                    Data.Initalizer.EventData = new List<NPCEventData>();
                    EventNames = new List<string>();
                }
                Event = new NPCEventData();
                Event.RequiredFlag = new Flags();
                Event.EventName = "New Event";
                Data.Initalizer.EventData.Add(Event);
                EventNames.Add(Event.EventName);
            }

            if (EventNames != null && EventNames.Count > 0 || Data.Initalizer.EventData != null)
            {
                EventSelector = EditorGUILayout.Popup(EventSelector, EventNames.ToArray());
                Event = Data.Initalizer.EventData[EventSelector];

                if (Event.RequiredFlag != null && Event.FlagToSet != null)
                {
                    Event.RequiredFlag = Data.Initalizer.EventData[EventSelector].RequiredFlag;
                    FlagSelected = Data.Initalizer.EventData[EventSelector].RequiredFlag.ID;
                    Event.FlagToSet = Data.Initalizer.EventData[EventSelector].FlagToSet;
                    FlagSelectedSet = Data.Initalizer.EventData[EventSelector].FlagToSet.ID;
                }
            }
            if (Event != null)
            {
                EditorGUILayout.LabelField("Flag Required");
                FlagSelected = EditorGUILayout.Popup(FlagSelected, FlagString.ToArray());
                Event.RequiredFlag = FlagList[FlagSelected];
                EditorGUILayout.LabelField("Event Name");
                Event.EventName = EditorGUILayout.TextField(Event.EventName);
                EditorGUILayout.LabelField("Event Type");
                Event.Type = (NPCEventType)EditorGUILayout.EnumPopup(Event.Type);
                EditorGUILayout.LabelField("Flag To Set");
                FlagSelectedSet = EditorGUILayout.Popup(FlagSelectedSet, FlagString.ToArray());
                Event.FlagToSet = FlagList[FlagSelectedSet];

                switch (Event.Type)
                {
                    case NPCEventType.DIALOUGE:
                        if (GUILayout.Button("Open Dialogue Editor"))
                        {
                            // Open Dialogue Editor, ensuring it has the flag and Quest ID corresonded.
                            if (DEditor == null && Event.binarySearchTrees == null)
                            {
                                DEditor = new DialogueEditor();
                                Event.binarySearchTrees = new List<BinarySearchTree<DialogueMessage>>();
                            }
                            else
                            {
                                DEditor = new DialogueEditor();
                            }
                            if (Event.binarySearchTrees.Count > 0)
                            {
                                DEditor.OpenWindowEditorList(Event.binarySearchTrees);
                            }
                            else
                            {
                                Event.TreeToEdit = new BinarySearchTree<DialogueMessage>();
                                DEditor.OpenWindowEditor(Event.TreeToEdit, Event.binarySearchTrees);
                            }
                        }
                        break;
                    case NPCEventType.ADDITEM:
                        // What Item to Add
                        if (ItemsToCollect == null)
                        {
                            Init();
                        }
                        int ItemID = 0;
                        ItemID = EditorGUILayout.Popup(ItemID, ItemNames.ToArray());
                        Event.Item = new ItemData();
                        Event.Item = ItemsToCollect[ItemID];
                        break;
                    case NPCEventType.FOLLOW:
                        // Take control of player and redirect to Waypoint
                        break;

                    case NPCEventType.ADDQUEST:
                        // Give that bitch quest no matter what.
                        EditorGUILayout.Popup(questSelected, QuestNames.ToArray());
                        Event.Quests = Quests[questSelected];
                        break;
                }
                if (GUILayout.Button("Save Event"))
                {
                    if (!Data.Initalizer.EventData.Contains(Event))
                    {
                        Data.Initalizer.EventData.Add(Event);
                    }
                    string eventD = " ";
                    eventD = JsonConvert.SerializeObject(Data.Initalizer.EventData, settings);
                    File.WriteAllText(Data.Initalizer.NpcEventPath, eventD);
                }
            }
            if (GUILayout.Button("Save"))
            {
                string Data = " ";
                Data = JsonConvert.SerializeObject(Editable, settings);
                File.WriteAllText(FilePath, Data);
            }
        }
    }
}
