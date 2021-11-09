using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif
using System.IO;
using System;

public enum SceneTypes { MAIN, BATTLE }

// Realistically I need to add an editor for this shit since i'm serializing property data that's in the map

#if UNITY_EDITOR
[CustomEditor(typeof(JrpgSceneManager))]
class ContextSceneMenu : Editor
{
    static string filePath = "Assets/SceneIndex.json";

    static int sceneID = 0;
    // This seems to need a bit more... Elaboration to put it lightly. 
    // I need to find a way to make this specific instance the editor is grabbing appear in the Unity UI as a GameObject componet.
    // That, or I should use a scriptable object as the actual manager instance and let the editor fill in the rest where needed which seems more logical
    [SerializeField]
    static JrpgSceneManager Instance;

    static GameAssetManager assets = GameAssetManager.Instance;

    static JsonSerializerSettings settings = new JsonSerializerSettings
    {
        TypeNameHandling = TypeNameHandling.All,
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
    };

    public static void NewMainScene()
    {
        Scene s = new Scene();
        s = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Additive);
        MainScene data = new MainScene(s, s.name);
        Instance.scenes.Add(data);
        EditorSceneManager.SaveScene(s);
        //EditorSceneManager.MoveGameObjectToScene(Instance.selfRef, s);
        string serialize = JsonConvert.SerializeObject(Instance.scenes, settings);
        File.WriteAllText(filePath, serialize);
        sceneID += 1;
    }

    public static void NewBattleScene()
    {
        Scene s = new Scene();
        s = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Additive);
        BattleScene data = new BattleScene(s, s.name);
        Instance.scenes.Add(data);
        EditorSceneManager.SaveScene(s);
        //EditorSceneManager.MoveGameObjectToScene(Instance.selfRef, s);
        string serialize = JsonConvert.SerializeObject(Instance.scenes, settings);
        File.WriteAllText(filePath, serialize);
        sceneID += 1;
    }

    // What I need to do is A. Load data from Asset Manager
    // B. Check if Data is baddies and load from there.
    // Still on the fence as to whether I want scene data in the asset manager.
    public override void OnInspectorGUI()
    {
        Instance = (JrpgSceneManager)EditorGUILayout.ObjectField(Instance, typeof(JrpgSceneManager), true);
        if (GUILayout.Button("New Scene"))
        {
            NewMainScene();
        }
        if (GUILayout.Button("New Battle Scene"))
        {
            NewBattleScene();
        }
        base.OnInspectorGUI();
        //if (activeSceneData.Type == SceneTypes.BATTLE)
        //{
            //EditorGUILayout.Popup()
        //}
    }
}
#endif

// Below are SceneTypes. I like how the scenes all derive the same type but not sure about that being an interface...
[Serializable]
public class SceneInfo : PropertyAttribute
{
    public SceneTypes type;
    public virtual SceneTypes GetSceneType()
    {
        return type;
    }
}

public class MainScene : SceneInfo
{
    public Scene scene;
    public string sceneName;
    public MainScene(Scene s, string name)
    {
        sceneName = name;
        scene = s;
    }

    public override SceneTypes GetSceneType()
    {
        return type = SceneTypes.MAIN;
    }
}

public class BattleScene : SceneInfo
{
    string sceneName;
    public Scene battleScene;
    public List<Baddies> allowedEnemies;

    public BattleScene(Scene s, string name)
    {
        sceneName = name;
        battleScene = s;
        allowedEnemies = new List<Baddies>();
    }

    public override SceneTypes GetSceneType()
    {
        return type = SceneTypes.BATTLE;
    }
}

// This handles categories that may be applicable to only Jrpg types like having an explicit battle scene.
// Some games may battle on the current map or be action style 3D fighters IE. KH or Elder Scrolls
// While at the crux this engine is more then capable of that type of game given the use of flag system for story quests and the Dialogue trees.
// This Scene manager is meant for upwards of FFX style of game where there is a specific map or scene that is laid out for battle scenarios
[CreateAssetMenu]
public class JrpgSceneManager : ScriptableObject
{
    [SerializeField]
    public List<SceneInfo> scenes;
    string filePath;
    string jsonData;

    static JsonSerializerSettings settings = new JsonSerializerSettings
    {
        TypeNameHandling = TypeNameHandling.All,
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
    };

    JrpgSceneManager()
    {
        scenes = new List<SceneInfo>();
        Debug.Log("Construct");
    }

    private void OnEnable()
    {
        filePath = Application.dataPath + "/SceneIndex.json";
        scenes = new List<SceneInfo>();
    }

    void Transition(Scene s)
    {
        SceneManager.LoadScene(s.name);
    }

    // I think about adding a get function and keeping the lists as private in here.
    // I need to consider any other methods needed. And, what's the best way to save this data?




    // Update is called once per frame
    void Update()
    {
        
    }
}
