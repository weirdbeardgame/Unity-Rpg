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

    List<Flags> FlagList;
    List<QuestData> Quests;

    // For item collect quests
    public Dictionary<int, ItemData> ItemsToCollect;
    List<string> TempItemNames;
    Item GetItem;
    int itemSelection = 0;
    QuestData questToCreate;
    QuestEvent questEvent;
    Rect PropertyPage;
    Rect ButtonList;
    Rect TopProperties;

    bool IsInit = false;
    string[] array = {"Quest, Objectives, Events"}; 
    int tabIndex = 0;

    [MenuItem("Window/Quest Editor")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<QuestEditor>();
    }

    void ReadJson()
    {
        if (File.Exists(ItemFilePath))
        {
            ItemsToCollect = new Dictionary<int, ItemData>();
            TempItemNames = new List<string>();
            JsonData = File.ReadAllText(ItemFilePath);
            ItemsToCollect = JsonConvert.DeserializeObject<Dictionary<int, ItemData>>(JsonData);
            for (int i = 0; i < ItemsToCollect.Count; i++)
            {
                TempItemNames.Add(ItemsToCollect[i].ItemName);
            }
        }
        if (File.Exists(FlagPath))
        {
            FlagList = new List<Flags>();
            JsonData = File.ReadAllText(FlagPath);
            FlagList = JsonConvert.DeserializeObject<List<Flags>>(JsonData);
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
    /**********************************************************************************  
    * TO DO:
    * Need to figure out why it's saving the item a hundred billion times. 
    * Need to figure out how I want events stored. If it'll be a trigger ala RpgMaker
    * Or if the QuestData will store them and there's a constant check of flags.
    * Need to add display of objectives and other objective types. 
    ***********************************************************************************/
    private void OnGUI()
    {
        if (!IsInit)
        {
            Init();
        }

        tabIndex = GUILayout.Toolbar(tabIndex, array);

        switch(tabIndex)
        {
            case 0:
                if (GUILayout.Button("New Quest"))
                {
                    questToCreate = new QuestData();
                    questToCreate.events = new List<QuestEvent>();
                    questToCreate.QuestName = "temp";
                    Quests.Add(questToCreate);
                }

                GUI.Box(PropertyPage, "Quests");

                GUILayout.BeginHorizontal();
                GUILayout.BeginVertical("box", GUILayout.MaxWidth(150), GUILayout.ExpandHeight(true));
                if (questToCreate != null)
                {
                    EditorGUILayout.LabelField("Quest Data");
                    questToCreate.QuestID = EditorGUILayout.IntField(questToCreate.QuestID);
                    EditorGUILayout.LabelField("Name Of Quest");
                    questToCreate.QuestName = EditorGUILayout.TextField(questToCreate.QuestName);
                    EditorGUILayout.LabelField("Quest Description");
                    questToCreate.Description = EditorGUILayout.TextArea(questToCreate.Description);
                }

                for (int i = 0; i < Quests.Count; i++)
                {
                    if (GUILayout.Button(Quests[i].QuestName))
                    {
                        questToCreate = Quests[i];
                    }
                }
                GUILayout.EndVertical();
                GUILayout.EndHorizontal();


                GUILayout.FlexibleSpace();
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Save"))
                {
                    if (!Quests.Contains(questToCreate))
                    {
                        Quests.Add(questToCreate);
                    }
                    string Data = JsonConvert.SerializeObject(Quests);
                    File.WriteAllText(FilePath, Data);
                }
                GUILayout.EndHorizontal();
                break;
        }
    }
}



