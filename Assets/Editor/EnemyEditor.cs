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
    Dictionary<int, Baddies> Editable;

    Baddies edit;
    string filePath = "Assets/Enemies/Enemies.json";
    string jsonData;
    int Index = 0;
    string PlayerName;
    static bool[] fold = new bool[10];
    List<string> Names;
    bool IsInit = false;
    Sprite sprite;
    //Rect Position;
    SerializedProperty property;

    int ID = 0;

    static JsonSerializerSettings settings = new JsonSerializerSettings
    {
        TypeNameHandling = TypeNameHandling.All
    };

    public void readJson()
    {
        Editable = new Dictionary<int, Baddies>();

        if (File.Exists(filePath))
        {
            jsonData = File.ReadAllText(filePath);
            Editable = JsonConvert.DeserializeObject<Dictionary<int, Baddies>>(jsonData, settings);
        }

        Names = new List<string>();

        if (Editable != null && Editable.Count > 0)
        {
            for (int i = 1; i < Editable.Count; i++)
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
                Editable = new Dictionary<int, Baddies>();
                Names = new List<string>();
            }

            edit = new Baddies();
            edit.creatureName = PlayerName;
            edit.id = (ID += 1); // This'll handle local instance but no more then that!
            edit.Stats = new StatManager();
            edit.Stats.Initalize();

            Editable.Add(edit.id, edit);
            Names.Add(PlayerName);
            Repaint();
        }

        if (Editable != null && Editable.Count > 0)
        {
            Index = EditorGUILayout.Popup(Index, Names.ToArray()) + 1;
            if (Editable.ContainsKey(Index))
            {
                edit = Editable[Index];
                Debug.Log("EID: " + Index);
                ID = Editable[Editable.Count].id;
            }
            if (edit != null)
            {
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
                if (sprite)
                {
                    edit.spritePath = AssetDatabase.GetAssetPath(sprite);
                }
            }
        }
        GUILayout.FlexibleSpace();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Save"))
        {
            //Editable[Index] = edit;
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
