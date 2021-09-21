using System.Collections.Generic;
using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using UnityEditor;
using System.IO;
using Questing;
using UnityEngine.Assertions.Must;

public class QuestEditor : EditorWindow
{
    string JsonData;
    string FilePath = "Assets/Quests/Quest.json";
    string FlagPath = "Assets/Flags.json";
    string DialoguePath = "Assets/Dialogue/Dialogue.json";

    List<Flags> FlagList;
    List<QuestData> Quests;
    GameAssetManager manager;

    // For item collect quests
    public Dictionary<int, ItemData> itemsToCollect;
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
        manager = GameAssetManager.Instance;

        if (itemsToCollect == null || itemsToCollect.Count == 0)
        {
        if (manager.isFilled())
        {
            foreach(var asset in manager.Data)
            {
                if (asset.Value is ItemData)
                {
                    if (itemsToCollect == null)
                    {
                        itemsToCollect = new Dictionary<int, ItemData>();
                    }
                    ItemData temp = (ItemData)asset.Value;
                    if (!itemsToCollect.ContainsKey(temp.itemID) || !itemsToCollect.ContainsValue(temp))
                    {
                        itemsToCollect.Add(temp.itemID, temp);
                    }
                }
                }
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

        TopProperties = new Rect(500, 0, 1000, 200);
        ButtonList = new Rect(0, 0, 150, position.height);
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

        tabIndex = GUI.Toolbar(new Rect(25, 25, 250, 30), tabIndex, array);

        switch(tabIndex)
        {
            case 0:
                if (GUILayout.Button("New Quest"))
                {
                    questToCreate = new QuestData();
                    questToCreate.events = new List<QuestEvent>();
                    questToCreate.questName = "temp";
                    Quests.Add(questToCreate);
                }

                GUI.Box(PropertyPage, "Quests");

                GUILayout.BeginHorizontal();
                GUILayout.BeginVertical("box", GUILayout.MaxWidth(150), GUILayout.ExpandHeight(true));
                if (questToCreate != null)
                {
                    EditorGUILayout.LabelField("Quest Data");
                    questToCreate.questID = EditorGUILayout.IntField(questToCreate.questID);
                    EditorGUILayout.LabelField("Name Of Quest");
                    questToCreate.questName = EditorGUILayout.TextField(questToCreate.questName);
                    EditorGUILayout.LabelField("Quest Description");
                    questToCreate.nonActiveDescription = EditorGUILayout.TextField(questToCreate.nonActiveDescription);


                }

                for (int i = 0; i < Quests.Count; i++)
                {
                    if (GUILayout.Button(Quests[i].questName))
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



