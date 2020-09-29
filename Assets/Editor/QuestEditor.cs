using System.Collections.Generic;
using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using UnityEditor;
using System.IO;
using questing;



public class QuestEditor : EditorWindow
{

    List<QuestData> Quests;
    QuestData QuestToCreate;

    List<BinarySearchTree<DialogueMessage>> Trees;

    List<string> QuestNames;
    List<string> ObjectiveNames;
    List<int> EventNames;
    List<string> TreeNames;
    List<string> FlagString;

    List<Flags> FlagData;
    Flags Flag;
        
    bool Initalized;

    int MaxObjectives = 0;
    int MaxEvents = 0;

    int FlagToSet = 0;
    int RequiredFlag = 0;
    int ObjectiveFlagToSet = 0;
    int ObjectiveRequiredFlag = 0;

    int Index = 0;
    int Tab = 0;

    string Data;
    string ItemFilePath = "Assets/Items.json";
    string FilePath = "Assets/Quests/Quest.json";
    string FlagPath = "Assets/Flags.json";
    string DialoguePath = "Assets/Dialogue/Dialogue.json";

    string[] Items = new string[3] { "Quest", "Objective", "Event" };
    public Dictionary<int, ItemData> ItemDictionary;

    bool DialogueExists;

    DialogueEditor DEditor;


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
            EventNames = new List<int>();
            ObjectiveNames = new List<string>();

            Data = File.ReadAllText(FilePath);
            Quests = JsonConvert.DeserializeObject<List<QuestData>>(Data);

        }

        QuestNames = new List<string>();

        if (Quests != null)
        {
            for (int i = 0; i < Quests.Count; i++)
            {
                if (Quests[i] != null)
                {
                    QuestNames.Add(Quests[i].QuestName);

                    if (Quests[i].Objectives != null)
                    {
                        for (int j = 0; j < Quests[i].Objectives.Count; j++)
                        {
                            ObjectiveNames.Add(Quests[i].Objectives[j].Name);
                        }
                    }
                }
            }
        }

        if (File.Exists(FlagPath))
        {
            Data = File.ReadAllText(FlagPath);
            FlagData = JsonConvert.DeserializeObject<List<Flags>>(Data);

            FlagString = new List<string>();

            for (int i = 0; i < FlagData.Count; i++)
            {
                FlagString.Add(FlagData[i].Flag);
            }

        }

        if (File.Exists(DialoguePath))
        {
            Data = File.ReadAllText(DialoguePath);
            Trees = JsonConvert.DeserializeObject<List<BinarySearchTree<DialogueMessage>>>(Data);

            TreeNames = new List<string>();

            for (int i = 0; i < Trees.Count; i++)
            {
                TreeNames.Add("Tree: " + Trees[i].Tree.Data.ID.ToString());
            }
        }


        Initalized = true;
    }


    List<string> GetItemNames()
    {

        List<string> TempNames = new List<string>();

        if (File.Exists(ItemFilePath))
        {
            string ItemData = File.ReadAllText(ItemFilePath);
            ItemDictionary = JsonConvert.DeserializeObject<Dictionary<int, ItemData>>(ItemData);
        }

        for (int i = 0; i < ItemDictionary.Count; i++)
        {
            TempNames.Add(ItemDictionary[i].ItemName);
        }

        return TempNames;
    }
    void OnGUI()
    {

        if (!Initalized)
        {
            ReadJson();
        }

        if (GUILayout.Button("New Quest"))
        {

            QuestToCreate = new QuestData();
            QuestToCreate.Objectives = new List<QuestObjective>();
            QuestToCreate.Events = new List<QuestEvent>();
            ObjectiveNames = new List<string>();
        }

        if (Quests != null && Quests.Count > 0)
        {
            Index = EditorGUILayout.Popup(Index, QuestNames.ToArray());
            QuestToCreate = Quests[Index];
        }

        if (QuestToCreate != null)
        {

            GUILayout.BeginHorizontal();
            Tab = GUILayout.Toolbar(Tab, Items);
            GUILayout.EndHorizontal();

            switch (Tab)
            {
                case 0:

                    EditorGUILayout.LabelField("Quest ID");
                    QuestToCreate.QuestID = EditorGUILayout.IntField(QuestToCreate.QuestID);
                    EditorGUILayout.LabelField("Quest Name");
                    QuestToCreate.QuestName = EditorGUILayout.TextField(QuestToCreate.QuestName);
                    EditorGUILayout.LabelField("Quest Type");
                    QuestToCreate.Type = (QuestType)EditorGUILayout.EnumPopup(QuestToCreate.Type);
                    EditorGUILayout.LabelField("Is Flag required or set ");
                    RequiredFlag = EditorGUILayout.Popup(RequiredFlag, FlagString.ToArray());
                    EditorGUILayout.LabelField("Flag Set By Quest: ");
                    FlagToSet = EditorGUILayout.Popup(FlagToSet, FlagString.ToArray());
                    QuestToCreate.Flag = FlagData[RequiredFlag];

                    break;
                case 1:

                    // Quest Objectives

                    if (QuestToCreate.Objectives != null)
                    {
                        MaxObjectives = QuestToCreate.Objectives.Count;
                    }

                    int ObjectiveIndex = 0;

                    if (GUILayout.Button("New Objective"))
                    {
                        QuestObjective Temp = new QuestObjective();
                        Temp.Name = "Objective: " + MaxObjectives;
                        Temp.ObjectiveID = MaxObjectives;
                        ObjectiveNames.Add(Temp.Name);
                        if (QuestToCreate.Objectives == null)
                        {
                            QuestToCreate.Objectives = new List<QuestObjective>();
                        }
                        QuestToCreate.Objectives.Add(Temp);
                        MaxObjectives++;
                    }

                    if (QuestToCreate.Objectives != null && QuestToCreate.Objectives.Count > 0)
                    {
                        ObjectiveIndex = EditorGUILayout.Popup(ObjectiveIndex, ObjectiveNames.ToArray());
                        EditorGUILayout.LabelField("Objective Title");
                        QuestToCreate.Objectives[ObjectiveIndex].Name = GUILayout.TextField(QuestToCreate.Objectives[ObjectiveIndex].Name);
                        EditorGUILayout.LabelField("Objective Description");
                        QuestToCreate.Objectives[ObjectiveIndex].Description = GUILayout.TextField(QuestToCreate.Objectives[ObjectiveIndex].Description);
                        EditorGUILayout.LabelField("Objective Type: ");
                        QuestToCreate.Objectives[ObjectiveIndex].Type = (QuestObjectiveType)EditorGUILayout.EnumPopup(QuestToCreate.Objectives[ObjectiveIndex].Type);
                        switch (QuestToCreate.Objectives[ObjectiveIndex].Type)
                        {

                            case QuestObjectiveType.COLLECT:
                                Item Temp = new Item();
                                GUILayout.Label("Items To Collect");

                                GUILayout.BeginHorizontal();
                                if (GUILayout.Button("Add Item"))
                                {
                                    if (QuestToCreate.Objectives[ObjectiveIndex].RequiredItems == null)
                                    {
                                        QuestToCreate.Objectives[ObjectiveIndex].RequiredItems = new List<ItemToCollect>();
                                    }

                                    ItemToCollect ItemToAdd = new ItemToCollect();
                                    QuestToCreate.Objectives[ObjectiveIndex].RequiredItems.Add(ItemToAdd);

                                }

                                if (GUILayout.Button("Remove Selection"))
                                {

                                }

                                EditorGUILayout.LabelField("Required Flag To Start Objective: ");
                                EditorGUILayout.LabelField("Is Flag required or set ");
                                RequiredFlag = EditorGUILayout.Popup(RequiredFlag, FlagString.ToArray());
                                EditorGUILayout.LabelField("Flag Set By Quest: ");
                                FlagToSet = EditorGUILayout.Popup(FlagToSet, FlagString.ToArray());
                                QuestToCreate.Flag = FlagData[RequiredFlag];




                                GUILayout.EndHorizontal();

                                if (QuestToCreate.Objectives[ObjectiveIndex].RequiredItems.Count > 0)
                                {

                                    for (int i = 0; i < QuestToCreate.Objectives[ObjectiveIndex].RequiredItems.Count; i++)
                                    {

                                        QuestToCreate.Objectives[ObjectiveIndex].RequiredItems[i].ItemID = EditorGUILayout.Popup(QuestToCreate.Objectives[ObjectiveIndex].RequiredItems[i].ItemID, GetItemNames().ToArray());
                                        QuestToCreate.Objectives[ObjectiveIndex].RequiredItems[i].Item = ItemDictionary[QuestToCreate.Objectives[ObjectiveIndex].RequiredItems[i].ItemID];

                                        GUILayout.Label("Amount To Collect");
                                        QuestToCreate.Objectives[ObjectiveIndex].RequiredItems[i].RequiredAmount = EditorGUILayout.IntField(QuestToCreate.Objectives[ObjectiveIndex].RequiredItems[i].RequiredAmount);
                                    }
                                }
                                break;

                            case QuestObjectiveType.KILL:
                                GUILayout.Label("Amount To Kill");
                                //QuestToCreate.Objectives[ObjectiveIndex].MaxAmount = EditorGUILayout.IntField(QuestToCreate.Objectives[ObjectiveIndex].MaxAmount);
                                break;
                        }
                    }
                    break;

                case 2:
                    int EventIndex = 0;

                    Events TempType = new Events();
                    string Name = " ";

                    EditorGUILayout.LabelField("Event Type");
                    TempType = (Events)EditorGUILayout.EnumPopup(TempType);

                    EditorGUILayout.LabelField("Event Name");
                    Name = EditorGUILayout.TextField(Name);

                    if (GUILayout.Button("New Event"))
                    {
                        QuestEvent Temp = new QuestEvent();

                        if (QuestToCreate.Events == null)
                        {
                            QuestToCreate.Events = new List<QuestEvent>();
                        }

                        Temp.Type = TempType;

                        QuestToCreate.Events.Add(Temp);
                    }


                    if (QuestToCreate.Events != null && QuestToCreate.Events.Count > 0)
                    {

                        QuestToCreate.Events[EventIndex].Type = (Events)EditorGUILayout.EnumPopup(QuestToCreate.Events[EventIndex].Type);


                        switch (QuestToCreate.Events[EventIndex].Type)
                        {
                            case Events.DIALOGUE:
                                int DialogueID = 0;
                                int index = 0;

                                if (GUILayout.Button("Add Existing Dialogue"))
                                {
                                    if (QuestToCreate.Events[EventIndex].Trees == null)
                                    {
                                        QuestToCreate.Events[EventIndex].Trees = new List<BinarySearchTree<DialogueMessage>>();
                                    }

                                    DialogueExists = true;
                                }
                                if (DialogueExists)
                                {
                                    DialogueID = EditorGUILayout.Popup(DialogueID, TreeNames.ToArray()); // List of trees

                                    QuestToCreate.Events[EventIndex].Trees.Insert(index, Trees[DialogueID]);
                                    index++;
                                }

                                if (GUILayout.Button("New Dialogue"))
                                {

                                    if (QuestToCreate.Events[EventIndex].Trees == null)
                                    {
                                        QuestToCreate.Events[EventIndex].Trees = new List<BinarySearchTree<DialogueMessage>>();
                                    }

                                    // Open Dialogue Editor!
                                    BinarySearchTree<DialogueMessage> DialogueTree = new BinarySearchTree<DialogueMessage>();
                                    DialogueTree.Tree = new TNode<DialogueMessage>();

                                    DEditor = new DialogueEditor();
                                    DEditor.OpenWindowEditor(DialogueTree, QuestToCreate.QuestID);
                                    QuestToCreate.Events[EventIndex].Trees.Insert(index, DialogueTree);
                                    index++;
                                }

                                break;

                            case Events.CUTSCENE:

                                break;


                            case Events.FOLLOW:
                                // NPC have scripted movement pattern to either follow player or player follow NPC
                                break;

                        }

                        // Quest Events
                    }
                    break;
            }

            GUILayout.FlexibleSpace();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Save"))
            {
                Save();
            }
            GUILayout.EndHorizontal();
        }
    }

    void Save()
    {
        if (!Quests.Contains(QuestToCreate))
        {
            QuestNames.Add(QuestToCreate.QuestName);
            Quests.Add(QuestToCreate);
        }

        string Save = JsonConvert.SerializeObject(Quests);
        File.WriteAllText(FilePath, Save);
    }
}



