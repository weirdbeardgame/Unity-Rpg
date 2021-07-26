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
    Player edit;
    string filePath = "Assets/Player/Actors.json";
    string jsonData;
    int Index;
    string PlayerName;
    static bool[] fold = new bool[10];
    List<string> Names;
    bool IsInit = false;
    Sprite sprite;

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

            edit = new Player();
            edit.CreatureName = PlayerName;
            edit.Stats = new StatManager();
            edit.Stats.Initalize();

            Editable.Add(edit);
            Names.Add(PlayerName);
            Repaint();
        }

        if (Editable != null && Editable.Count > 0)
        {
            Index = EditorGUILayout.Popup(Index, Names.ToArray());
            edit = Editable[Index];

            if (edit != null)
            {
                Rect spriteRect = new Rect();
                spriteRect.position = new Vector2(10, 10);
                sprite = (Sprite)EditorGUI.ObjectField(spriteRect, sprite, typeof(Texture2D), false);

                EditorGUILayout.LabelField("Level");
                edit.level = EditorGUILayout.IntField(edit.level);
                EditorGUILayout.LabelField("Health");
                edit.Stats.statList[(int)StatType.HEALTH].stat = EditorGUILayout.FloatField(edit.Stats.statList[(int)StatType.HEALTH].stat);
                EditorGUILayout.LabelField("Strength");
                edit.Stats.statList[(int)StatType.STRENGTH].stat = EditorGUILayout.FloatField(edit.Stats.statList[(int)StatType.STRENGTH].stat);
                EditorGUILayout.LabelField("Magic");
                edit.Stats.statList[(int)StatType.MAGIC].stat = EditorGUILayout.FloatField(edit.Stats.statList[(int)StatType.MAGIC].stat);
                EditorGUILayout.LabelField("Speed");
                edit.Stats.statList[(int)StatType.SPEED].stat = EditorGUILayout.FloatField(edit.Stats.statList[(int)StatType.SPEED].stat);
                EditorGUILayout.LabelField("Defense");
                edit.Stats.statList[(int)StatType.DEFENSE].stat = EditorGUILayout.FloatField(edit.Stats.statList[(int)StatType.DEFENSE].stat);
                EditorGUILayout.LabelField("Job");
                edit.Job = (JobSystem)EditorGUILayout.EnumPopup(edit.Job);
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
