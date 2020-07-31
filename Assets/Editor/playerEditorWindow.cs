using System.IO;
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;
using System.Collections;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

[CustomPropertyDrawer(typeof(Texture2D))]
public class playerEditorWindow : EditorWindow
{
    List<Player> Editable;

    Player Edit;
    string filePath = "Assets/Player/Actors.json";
    string jsonData;

    int Index;
    string PlayerName;

    static bool[] fold = new bool[10];
    List<string> Names;

    bool IsInit = false;


    //Rect Position;
    SerializedProperty property;

    static JsonSerializerSettings settings = new JsonSerializerSettings
    {
        TypeNameHandling = TypeNameHandling.All
    };

    public void readJson()
    {
        Editable = new List<Player>();

        if (File.Exists(filePath))
        {
            jsonData = File.ReadAllText(filePath);
            Editable = JsonConvert.DeserializeObject<List<Player>>(jsonData, settings);
        }

        Names = new List<string>();

        if (Editable != null && Editable.Count > 0)
        {
            for (int i = 0; i < Editable.Count; i++)
            {
                Names.Add(Editable[i].CreatureName);
            }
        }

        IsInit = true;
    }

    void OnGUI()
    {
        if (!IsInit)
        {
            readJson();
        }

        EditorGUILayout.LabelField("Name: ");
        PlayerName = EditorGUILayout.TextField(PlayerName, PlayerName);

        if (GUILayout.Button("New Character"))
        {
            if (Editable == null)
            {
                Editable = new List<Player>();
                Names = new List<string>();
            }

            Edit = new Player();
            Edit.CreatureName = PlayerName;
            Edit.Stats = new StatManager();
            Edit.Stats.Initalize();

            Editable.Add(Edit);
            Names.Add(PlayerName);
            Repaint();
        }

        if (Editable != null && Editable.Count > 0)
        {
            Index = EditorGUILayout.Popup(Index, Names.ToArray());
            Edit = Editable[Index];

            if (Edit != null)
            {
                //Rect spriteRect = new Rect();
                //spriteRect.position = new Vector2(10, 10);
                //sprite = (Sprite)EditorGUI.ObjectField(spriteRect, sprite, typeof(Texture2D), false);

                EditorGUILayout.LabelField("Level");
                Edit.Level = EditorGUILayout.IntField(Edit.Level);
                EditorGUILayout.LabelField("Health");
                Edit.Stats.StatList[(int)StatType.HEALTH].Stat = EditorGUILayout.FloatField(Edit.Stats.StatList[(int)StatType.HEALTH].Stat);
                EditorGUILayout.LabelField("Strength");
                Edit.Stats.StatList[(int)StatType.STRENGTH].Stat = EditorGUILayout.FloatField(Edit.Stats.StatList[(int)StatType.STRENGTH].Stat);
                EditorGUILayout.LabelField("Magic");
                Edit.Stats.StatList[(int)StatType.MAGIC].Stat = EditorGUILayout.FloatField(Edit.Stats.StatList[(int)StatType.MAGIC].Stat);
                EditorGUILayout.LabelField("Speed");
                Edit.Stats.StatList[(int)StatType.SPEED].Stat = EditorGUILayout.FloatField(Edit.Stats.StatList[(int)StatType.SPEED].Stat);
                EditorGUILayout.LabelField("Defense");
                Edit.Stats.StatList[(int)StatType.DEFENSE].Stat = EditorGUILayout.FloatField(Edit.Stats.StatList[(int)StatType.DEFENSE].Stat);
                EditorGUILayout.LabelField("Job");
                Edit.Job = (JobSystem)EditorGUILayout.EnumPopup(Edit.Job);
            }
        }
        GUILayout.FlexibleSpace();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Save"))
        {
            string data = JsonConvert.SerializeObject(Editable);
            File.WriteAllText(filePath, data);
        }
        GUILayout.EndHorizontal();
    }
    public void OnInspectorUpdate()
    {
        this.Repaint();
    }


    [MenuItem("Window/Player")]
    public static void ShowWindow()
    {
        GetWindow<playerEditorWindow>("Add Player");
    }

}
