using System.Collections.Generic;
using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using UnityEditor;
using System.IO;
using questing;
using UnityEngine.Assertions.Must;

public class QuestEditor : EditorWindow
{
    string JsonData;
    string ItemFilePath = "Assets/Items.json";
    string FilePath = "Assets/Quests/Quest.json";
    string FlagPath = "Assets/Flags.json";
    string DialoguePath = "Assets/Dialogue/Dialogue.json";

    List<QuestData> Quests;
    public Dictionary<int, ItemData> ItemsToCollect;
    List<string> TempItemNames;

    Item GetItem;

    QuestData QuestToCreate;
    QuestObjective Objective;

    Rect PropertyPage;
    Rect ButtonList;
    Rect TopProperties;

    int ItemSelection = 0;

    bool IsInit = false;

    [MenuItem("Window/Quest Editor")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<QuestEditor>();
    }

    void ReadJson()
    {        
        if (File.Exists(ItemFilePath))
        {
            JsonData = null;

            ItemsToCollect = new Dictionary<int, ItemData>();
            TempItemNames = new List<string>();
            JsonData = File.ReadAllText(ItemFilePath);
            ItemsToCollect = JsonConvert.DeserializeObject<Dictionary<int, ItemData>>(JsonData);
            for (int i = 0; i < ItemsToCollect.Count; i++)
            {
                TempItemNames.Add(ItemsToCollect[i].ItemName);
            }
        }
        if (File.Exists(FilePath))
        {
            Quests = new List<QuestData>();
            JsonData = File.ReadAllText(FilePath);
            Quests = JsonConvert.DeserializeObject<List<QuestData>>(JsonData);
        }
    }

    void Init()
    {
        ReadJson();
        if (Quests == null)
        {
            Quests = new List<QuestData>();
        }

        ButtonList = new Rect(0, 0, 150, position.height);
        TopProperties = new Rect(500, 0, 1000, 200);
        PropertyPage = new Rect(500, 200, 1000, position.height);

        IsInit = true;
    }

    private void OnGUI()
    {
        if (!IsInit)
        {
            Init();
        }
            
        GUI.Box(PropertyPage, "Quests");            
        GUI.Box(ButtonList, "Buttons");
    
        GUILayout.BeginArea(TopProperties);
            
        if (GUILayout.Button("New Quest"))            
        {                    
            QuestToCreate = new QuestData();
            QuestToCreate.Objectives = new List<QuestObjective>();
            QuestToCreate.QuestName = "Temp";
            Quests.Add(QuestToCreate);
        }
        
        if (QuestToCreate != null)        
        {
            EditorGUILayout.LabelField("Quest Data");
            QuestToCreate.QuestID = EditorGUILayout.IntField(QuestToCreate.QuestID);
            EditorGUILayout.LabelField("Name Of Quest");
            QuestToCreate.QuestName = EditorGUILayout.TextField(QuestToCreate.QuestName);
            EditorGUILayout.LabelField("Quest Description");
            QuestToCreate.Description = EditorGUILayout.TextArea(QuestToCreate.Description);
                      
            if (GUILayout.Button("Add Objective"))
            { 
                Objective = new QuestObjective();
                Objective.RequiredItems = new List<ItemData>();
                QuestToCreate.Objectives.Add(Objective);
            }
        }

        GUILayout.EndArea();

        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical("box", GUILayout.MaxWidth(150), GUILayout.ExpandHeight(true));
        for (int i = 0; i < Quests.Count; i++)
        {
            if (GUILayout.Button(Quests[i].QuestName))
            {
                QuestToCreate = Quests[i];
                Objective = QuestToCreate.Objectives[0];
            }
        }
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();

        if (Objective != null)            
        {               
            GUILayout.BeginArea(PropertyPage);
            EditorGUILayout.LabelField("Objective ID");
            Objective.ObjectiveID = EditorGUILayout.IntField(Objective.ObjectiveID);
            EditorGUILayout.LabelField("Objective Name");
            Objective.Name = EditorGUILayout.TextField(Objective.Name);
            EditorGUILayout.LabelField("Objective Description");
            Objective.Description = EditorGUILayout.TextField(Objective.Description);
            EditorGUILayout.LabelField("Type of Objective");
            Objective.Type = (QuestObjectiveType)EditorGUILayout.EnumPopup(Objective.Type);
            switch (Objective.Type)
            {
                case QuestObjectiveType.COLLECT:
                    EditorGUILayout.LabelField("Item To Collect");
                    ItemSelection = EditorGUILayout.Popup(ItemSelection, TempItemNames.ToArray());
                    Objective.RequiredItems.Add(ItemsToCollect[ItemSelection]);
                    break;

                case QuestObjectiveType.KILL:
                    EditorGUILayout.LabelField("Monster to Kill");
                    break;
            
            }

            GUILayout.EndArea();            
        }
        
        GUILayout.FlexibleSpace();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Save"))
        {
            if (!Quests.Contains(QuestToCreate))
            {
                Quests.Add(QuestToCreate);
            }
            string Data = JsonConvert.SerializeObject(Quests);
            File.WriteAllText(FilePath, Data);
        }
        GUILayout.EndHorizontal();
    }
}



