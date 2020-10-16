using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using UnityEngine;
using UnityEditor;
    


[CustomEditor(typeof(NPC))]
public class NPCEditor : Editor
{
    NPC Data;
    bool IsInit;
    int FlagSelected;
    string JsonData;
    string FilePath = "Assets/NPC/NPC.json";
    string EventFilePath = "Assets/NPC/Events.json";
    string ItemFilePath = "Assets/Items.json";
    string FlagPath = "Assets/Flags.json";
    Dictionary<int, ItemData> ItemsToCollect;
    List<QuestEventData> Events;
    List<Flags>FlagList;
    List<NPCData> Editable;
    List<string> FlagString;
    List<string> ItemNames;
    List<string> EventNames;
    void ReadJson()
    {
        if (File.Exists(FilePath))
        {
            JsonData = File.ReadAllText(FilePath);
            Editable = JsonConvert.DeserializeObject<List<NPCData>>(JsonData);
        }
        if (File.Exists(EventFilePath))
        {
            JsonData = null;
            JsonData = File.ReadAllText(FilePath);
            Events = JsonConvert.DeserializeObject<List<QuestEventData>>(JsonData);
        }
        if (File.Exists(FlagPath))
        {
            JsonData = null;
            FlagList = new List<Flags>();
            JsonData = File.ReadAllText(FlagPath);
            FlagList = JsonConvert.DeserializeObject<List<Flags>>(JsonData);

            for (int i = 0; i < FlagList.Count; i++)
            {
                FlagString.Add(FlagList[i].Flag);
            }
        }
        if (File.Exists(ItemFilePath))
        {
            JsonData = null;            
            ItemsToCollect = new Dictionary<int, ItemData>();
            ItemNames = new List<string>();
            JsonData = File.ReadAllText(ItemFilePath);
            ItemsToCollect = JsonConvert.DeserializeObject<Dictionary<int, ItemData>>(JsonData);
            for (int i = 0; i < ItemsToCollect.Count; i++)
            {
                ItemNames.Add(ItemsToCollect[i].ItemName);
            }
        }
    }

    void Init()
    {
        if (!IsInit)
        {
            ReadJson();
            Data = (NPC)target;
            Data.NpcData = new NPCData();
            IsInit = true;
        }
    }

    public override void OnInspectorGUI()
    {
        Init();

        base.OnInspectorGUI();

        if (GUILayout.Button("AddEvent"))
        {
            Data.NpcData.Event = new QuestEventData();
            FlagSelected = EditorGUILayout.Popup(FlagSelected, FlagString.ToArray());
            Data.NpcData.Event.RequiredFlag = FlagList[FlagSelected];

            switch (Data.NpcData.Event.Type)
            {
                case QuestEventType.DIALOUGE: 
                    if (GUILayout.Button("Open Dialogue Editor"))       
                    { 
                        // Open Dialogue Editor, ensuring it has the flag and Quest ID corresonded.

                    }
                    break;
                case QuestEventType.ADDITEM:
                    // What Item to Add
                    if (ItemsToCollect == null)
                    {
                        Init();
                    }
                    int ItemID = 0;
                    ItemID = EditorGUILayout.Popup(ItemID, ItemNames.ToArray());
                    Data.NpcData.Event.Item = new ItemData();
                    Data.NpcData.Event.Item = ItemsToCollect[ItemID];
                    break;
            }

            if (GUILayout.Button("Save Event"))
            {
                Data.NpcData.AddEvent();
            }
        }
    }

}
