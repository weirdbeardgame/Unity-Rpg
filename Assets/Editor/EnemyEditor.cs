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

    Baddies edit;
    string filePath = "Assets/Enemies/Enemies.json";
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
                Names.Add(Editable[i].creatureName);
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

            edit = new Baddies();
            edit.creatureName = PlayerName;
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
                //Rect spriteRect = new Rect();
                //spriteRect.position = new Vector2(10, 10);
                //sprite = (Sprite)EditorGUI.ObjectField(spriteRect, sprite, typeof(Texture2D), false);

                EditorGUILayout.LabelField("Enemy ID");
                edit.id = EditorGUILayout.IntField(edit.id);

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
                edit.job = (JobSystem)EditorGUILayout.EnumPopup(edit.job);
                EditorGUILayout.LabelField("Sprite Selector");
                sprite = (Sprite)EditorGUILayout.ObjectField("Sprite", sprite, typeof(Sprite), false);
                edit.spritePath = sprite.name;
            }
        }
        GUILayout.FlexibleSpace();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Save"))
        {
            Editable[Index] = edit;
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
