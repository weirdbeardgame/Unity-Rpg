using System.Collections.Generic;
using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using UnityEditor;
using System.IO;
using questing;



public class QuestEditor : EditorWindow
{
    string JsonData;
    string ItemFilePath = "Assets/Items.json";
    string FilePath = "Assets/Quests/Quest.json";
    string FlagPath = "Assets/Flags.json";
    string DialoguePath = "Assets/Dialogue/Dialogue.json";

    List<QuestData> Quests;

    QuestData QuestToCreate;

    bool IsInit = false;

    [MenuItem("Window/Quest Editor")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<QuestEditor>();
    }

    void ReadJson()
    {
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

        IsInit = true;
    }

    private void OnGUI()
    {
        if (!IsInit)
        {
            Init();
        }

        if (GUILayout.Button("New Quest"))
        {
            QuestToCreate = new QuestData();
        }

        EditorGUILayout.LabelField("Quest Data");
        QuestToCreate.QuestID = EditorGUILayout.IntField(QuestToCreate.QuestID);
        EditorGUILayout.LabelField("Name Of Quest");
        QuestToCreate.QuestName = EditorGUILayout.TextField(QuestToCreate.QuestName);
        EditorGUILayout.LabelField("Quest Description");
        QuestToCreate.Description = EditorGUILayout.TextArea(QuestToCreate.Description);

        if (GUILayout.Button("Save"))
        {
            Quests.Add(QuestToCreate);
        }
    }

}



