using System.IO;
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;
using System.Collections;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

[CustomPropertyDrawer(typeof(Texture2D))]
public class EnemyEditorWindow : EditorWindow
{
    List<Baddies> Editable;

    Baddies Edit;
    string filePath = "Assets/Enemies/Enemies.json";
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
        Editable = new List<Baddies>();

        if (File.Exists(filePath))
        {
            jsonData = File.ReadAllText(filePath);
            Editable = JsonConvert.DeserializeObject<List<Baddies>>(jsonData, settings);
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

        if (GUILayout.Button("New Enemy"))
        {
            if (Editable == null)
            {
                Editable = new List<Baddies>();
                Names = new List<string>();
            }

            Edit = new Baddies();
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

                EditorGUILayout.LabelField("Enemy ID");
                Edit.ID = EditorGUILayout.IntField(Edit.ID);

                EditorGUILayout.LabelField("Level");
                Edit.Level = EditorGUILayout.IntField(Edit.Level);
                EditorGUILayout.LabelField("Health");
                Edit.Stats.statList[(int)StatType.HEALTH].stat = EditorGUILayout.FloatField(Edit.Stats.statList[(int)StatType.HEALTH].stat);
                EditorGUILayout.LabelField("Strength");
                Edit.Stats.statList[(int)StatType.STRENGTH].stat = EditorGUILayout.FloatField(Edit.Stats.statList[(int)StatType.STRENGTH].stat);
                EditorGUILayout.LabelField("Magic");
                Edit.Stats.statList[(int)StatType.MAGIC].stat = EditorGUILayout.FloatField(Edit.Stats.statList[(int)StatType.MAGIC].stat);
                EditorGUILayout.LabelField("Speed");
                Edit.Stats.statList[(int)StatType.SPEED].stat = EditorGUILayout.FloatField(Edit.Stats.statList[(int)StatType.SPEED].stat);
                EditorGUILayout.LabelField("Defense");
                Edit.Stats.statList[(int)StatType.DEFENSE].stat = EditorGUILayout.FloatField(Edit.Stats.statList[(int)StatType.DEFENSE].stat);
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


    [MenuItem("Window/Enemy")]
    public static void ShowWindow()
    {
        GetWindow<EnemyEditorWindow>("Add Enemy");
    }

}
