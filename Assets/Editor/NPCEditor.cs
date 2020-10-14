using questing;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class NPCEditor : EditorWindow
{
    string NpcName;
    string FilePath = "Assets/NPC.json";
    string QuestFilePath = "Assets/Quests/Quest.json";

    string JsonData;
    string QuestData;

    int index;
    int MapID;

    NPCData EditNPC;
    List<NPCData> Editable;
    List<QuestData> Quests;

    List<string> QuestNames;
    List<int> CurrentScene;

    bool initalized;
    NPCData Temp;
    static bool[] fold = new bool[10];


    DialogueEditor DialogueEditor;


    [MenuItem("Window/NPCEdit")]
    private static void ShowWindow()
    {
        var window = GetWindow<NPCEditor>();
        window.titleContent = new GUIContent("npcEditor");
        window.Show();
    }

    static JsonSerializerSettings settings = new JsonSerializerSettings
    {
        TypeNameHandling = TypeNameHandling.All,
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
    };

    public void ReadJson()
    {
        Editable = new List<NPCData>();

        if (File.Exists(FilePath))
        {
            JsonData = File.ReadAllText(FilePath);
            Editable = JsonConvert.DeserializeObject<List<NPCData>>(JsonData, settings);
            initalized = true;
        }

        if (File.Exists(QuestFilePath))
        {
            Quests = new List<QuestData>();


            QuestData = File.ReadAllText(QuestFilePath);
            Quests = JsonConvert.DeserializeObject<List<QuestData>>(QuestData);
        }

        QuestNames = new List<string>();

        if (Quests != null)
        {
            for (int i = 0; i < Quests.Count; i++)
            {
                QuestNames.Add(Quests[i].QuestName);
            }
        }
    }

    void OnGUI()
    {
        if (initalized == false)
        {
            CurrentScene = new List<int>();

            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                CurrentScene.Add(i);
            }

            ReadJson();
        }

        GUILayout.Label("Npc Name");
        NpcName = EditorGUILayout.TextField(NpcName);

        if (GUILayout.Button("New NPC"))
        {
            Temp = new NPCData();
            Temp.NpcName = NpcName;
            Editable.Add(Temp);
        }

        for (int i = 0; i < Editable.Count; i++)
        {

            //EditorGUILayout.Popup(j, CurrentScene.ToString());

            fold[i] = EditorGUILayout.Foldout(fold[i], Editable[i].NpcName);

            if (fold[i])
            {

                EditorGUILayout.LabelField("ID");
                Editable[i].NpcID = EditorGUILayout.IntField(Editable[i].NpcID);
                EditorGUILayout.LabelField("Name");
                Editable[i].NpcName = GUILayout.TextField(Editable[i].NpcName);
                if (GUILayout.Button("Select Sprite"))
                {
                    Editable[i].SpritePath = EditorUtility.OpenFilePanel("Select Sprite: ", "Assets", "png");
                }
                EditorGUILayout.LabelField("Sprite Path");
                GUILayout.TextField(Editable[i].SpritePath);

                Editable[i].HasQuest = EditorGUILayout.Toggle("Has Quest ", Editable[i].HasQuest);

                if (Editable[i].HasQuest)
                {
                    Editable[i].QuestID = EditorGUILayout.Popup(Editable[i].QuestID, QuestNames.ToArray());
                }
            }
        }

        if (GUILayout.Button("Save"))
        {
            JsonSerializer serializer = new JsonSerializer();

            using (StreamWriter sw = new StreamWriter(FilePath))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, Editable);
            }
        }
    }
}
