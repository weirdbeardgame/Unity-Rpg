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
    List<IAsset> serialize;
    Player edit;
    int Index;
    string PlayerName;
    static bool[] fold = new bool[10];
    List<string> Names;
    bool IsInit = false;
    Sprite sprite;
    Player player;

    GameAssetManager manager;

    //Rect Position;
    SerializedProperty property;

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
                    if (asset.Value is Player && (player = (Player)asset.Value) != null)
                    {
                        player.prefab = AssetDatabase.LoadAssetAtPath<GameObject>(player.prefabPath);
                        Editable.Add(player);
                        Names.Add(player.Data.creatureName);
                    }
            }
        }
        IsInit = true;
    }

    void createPrefab()
    {
        edit.prefab = new GameObject();
        edit.prefab.AddComponent<Gauge>();
        edit.prefab.AddComponent<Animator>();
        edit.prefab.AddComponent<Rigidbody2D>();
        edit.prefab.AddComponent<BoxCollider2D>();
        edit.prefab.AddComponent<SpriteRenderer>();
        edit.prefab.name = edit.Data.creatureName;
    }

    void OnGUI()
    {
        if (!IsInit)
        {
            Names = new List<string>();
            Editable = new List<Player>();
            manager = GameAssetManager.Instance;
            manager.Init();
            init();
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
            edit.Data = new Creature();
            edit.Data.creatureName = PlayerName;
            edit.Data.Stats = new StatManager();
            edit.Data.Stats.Initalize();

            if (!edit.prefab)
            {
                createPrefab();
                if (!Directory.Exists("Assets/Resources/Prefabs/Players/"))
                {
                    Directory.CreateDirectory("Assets/Resources/Prefabs/Players/");
                }
                PrefabUtility.SaveAsPrefabAsset(edit.prefab, ("Assets/Resources/Prefabs/Players/" + edit.Data.creatureName + ".prefab"));
                edit.prefabPath = ("Prefabs/Players/" + edit.Data.creatureName);
            }

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
            }
            if (edit.prefab)
            {
                if (GUILayout.Button("Edit Prefab"))
                {
                    AssetDatabase.OpenAsset(PrefabUtility.LoadPrefabContents(("Resources/Prefabs/Players/" + edit.Data.creatureName + ".prefab")));
                }
            }
        }
        GUILayout.FlexibleSpace();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Save"))
        {
            for (int i = 0; i < Editable.Count; i++)
            {
                manager.AddAsset(Editable[i], Editable[i].Data.creatureName);
            }
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
