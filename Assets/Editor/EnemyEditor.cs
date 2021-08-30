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
    List<Asset> serialize;
    Baddies edit;
    int Index = 0;
    string enemyName;
    static bool[] fold = new bool[10];
    List<string> Names;
    bool IsInit = false;
    Sprite sprite;
    SerializedProperty property;
    GameAssetManager manager;
    int ID = 0;

    static JsonSerializerSettings settings = new JsonSerializerSettings
    {
        TypeNameHandling = TypeNameHandling.All
    };

    void init()
    {
        if (manager.isFilled() > 0)
        {
            foreach(var asset in manager.Data)
            {
                if (asset.Value.indexedType == AssetType.ENEMY)
                {
                    Baddies bad = (Baddies)asset.Value.Data;
                    bad.Prefab = AssetDatabase.LoadAssetAtPath<GameObject>(bad.prefabPath);
                    Editable.Add(bad);
                    Names.Add(bad.Data.creatureName);
                    ID += 1;
                }
            }
        }
    }

    void createPrefab()
    {
        edit.Prefab = new GameObject();
        edit.Prefab.AddComponent<Gauge>();
        edit.Prefab.AddComponent<Animator>();
        edit.Prefab.AddComponent<Rigidbody2D>();
        edit.Prefab.AddComponent<BoxCollider2D>();
        edit.Prefab.AddComponent<SpriteRenderer>();
        edit.Prefab.name = edit.Data.creatureName;
        if (!Directory.Exists("Assets/Resources/Prefabs/Enemies/"))
        {
            Directory.CreateDirectory("Assets/Resources/Prefabs/Enemies/");
        }
        PrefabUtility.SaveAsPrefabAsset(edit.Prefab, ("Assets/Resources/Prefabs/Enemies/" + edit.Data.creatureName + ".prefab"));
        edit.prefabPath = ("Prefabs/Enemies/" + edit.Data.creatureName);
    }

    void OnGUI()
    {
        if (!IsInit)
        {
            Names = new List<string>();
            Editable = new List<Baddies>();
            manager = new GameAssetManager();
            manager.Init();
            init();
            IsInit = true;
        }

        EditorGUILayout.LabelField("Name: ");
        enemyName = EditorGUILayout.TextField(enemyName, enemyName);

        if (GUILayout.Button("New Enemy"))
        {
            edit = new Baddies();

            edit.Data = new Creature();
            edit.Data.creatureName = enemyName;

            edit.id = ID;

            edit.Data.Stats = new StatManager();
            edit.Data.Stats.Initalize();

            createPrefab();

            if (!Editable.Contains(edit))
            {
                Editable.Add(edit);
                Names.Add(enemyName);
            }
            ID += 1;
            Repaint();
        }

        if (Editable != null && Editable.Count > 0)
        {
            Index = EditorGUILayout.Popup(Index, Names.ToArray());
            edit = Editable[Index];
            //Debug.Log("EID: " + Index);

            if (edit.Data != null)
            {
                EditorGUILayout.LabelField("Enemy ID");
                edit.id = EditorGUILayout.IntField(edit.id);

                EditorGUILayout.LabelField("Description");
                edit.Data.description = EditorGUILayout.TextArea(edit.Data.description, GUILayout.Height(75));

                EditorGUILayout.LabelField("Level");
                edit.level = EditorGUILayout.IntField(edit.level);

                EditorGUILayout.LabelField("Health");
                edit.Data.Stats.statList[(int)StatType.HEALTH].stat = EditorGUILayout.FloatField(edit.Data.Stats.statList[(int)StatType.HEALTH].stat);

                EditorGUILayout.LabelField("Strength");
                edit.Data.Stats.statList[(int)StatType.STRENGTH].stat = EditorGUILayout.FloatField(edit.Data.Stats.statList[(int)StatType.STRENGTH].stat);

                EditorGUILayout.LabelField("Magic");
                edit.Data.Stats.statList[(int)StatType.MAGIC].stat = EditorGUILayout.FloatField(edit.Data.Stats.statList[(int)StatType.MAGIC].stat);

                EditorGUILayout.LabelField("Speed");
                edit.Data.Stats.statList[(int)StatType.SPEED].stat = EditorGUILayout.FloatField(edit.Data.Stats.statList[(int)StatType.SPEED].stat);

                EditorGUILayout.LabelField("Defense");
                edit.Data.Stats.statList[(int)StatType.DEFENSE].stat = EditorGUILayout.FloatField(edit.Data.Stats.statList[(int)StatType.DEFENSE].stat);

                EditorGUILayout.LabelField("Job");
                edit.Data.job = (JobSystem)EditorGUILayout.EnumPopup(edit.Data.job);

                if (edit.Prefab)
                {
                    if (GUILayout.Button("Edit Prefab"))
                    {
                        AssetDatabase.OpenAsset(PrefabUtility.LoadPrefabContents(("Resources/Prefabs/Enemies/" + edit.Data.creatureName + ".prefab")));
                    }
                }
            }
        }
        GUILayout.FlexibleSpace();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Save"))
        {
            for (int i = 0; i < Editable.Count; i++)
            {
                manager.AddAsset(new Asset(Editable[i], AssetType.ENEMY), Editable[i].Data.creatureName);
            }
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
