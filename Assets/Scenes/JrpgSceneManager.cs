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
    static int sceneID = 0;
    // This seems to need a bit more... Elaboration to put it lightly. 
    // I need to find a way to make this specific instance the editor is grabbing appear in the Unity UI as a GameObject componet.
    // That, or I should use a scriptable object as the actual manager instance and let the editor fill in the rest where needed which seems more logical
    [SerializeField]
    static JrpgSceneManager Instance;

    static GameAssetManager assets = GameAssetManager.Instance;

    static List<string> names;
    private void OnEnable() {
        Instance = (JrpgSceneManager)target;
        names = new List<string>();
        if (Instance.Scenes != null)
        {
            foreach(var scene in Instance.Scenes)
            {
                names.Add(scene.Value.sceneName);
            }
        }
    }

    public static void NewMainScene()
    {
        Scene s = new Scene();
        s = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Additive);
        EditorSceneManager.SaveScene(s);
        MainScene data = new MainScene(s, s.name);
        Instance.Scenes.Add(s.name, data);
        names.Add(s.name);
        //EditorSceneManager.MoveGameObjectToScene(Instance.selfRef, s);
    }

    public static void NewBattleScene()
    {
        Scene s = new Scene();
        s = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Additive);
        EditorSceneManager.SaveScene(s);
        BattleScene data = new BattleScene(s, s.name);
        Instance.Scenes.Add(s.name, data);
        names.Add(s.name);
        //EditorSceneManager.MoveGameObjectToScene(Instance.selfRef, s);
    }

    // What I need to do is A. Load data from Asset Manager
    // B. Check if Data is baddies and load from there.
    // Still on the fence as to whether I want scene data in the asset manager.
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("New Scene"))
        {
            NewMainScene();
        }
        if (GUILayout.Button("New Battle Scene"))
        {
            NewBattleScene();
        }
        base.OnInspectorGUI();
        // Oops! names was cleared by accident. Hack Fix that shit!
        if (names.Count <= 0 && Instance.Scenes.Count > 0)
        {
            foreach(var scene in Instance.Scenes)
            {
                names.Add(scene.Value.sceneName);
            }
        }
        if (names.Count > 0)
        {
            EditorGUI.BeginChangeCheck();
            int index = EditorGUILayout.Popup(sceneID, names.ToArray());
            Instance.ActiveScene = Instance.Scenes[names[index]];
            if (EditorGUI.EndChangeCheck())
            {
                EditorSceneManager.OpenScene(Instance.ActiveScene.scenePath);
            }
        }
    }
}
#endif

// Below are SceneTypes. I like how the scenes all derive the same type but not sure about that being an interface...
[Serializable]
public class SceneInfo : PropertyAttribute
{
    public SceneTypes type;
    public string sceneName;
    public string scenePath;
    public Scene scene;

    public virtual SceneTypes GetSceneType()
    {
        return type;
    }
}

public class MainScene : SceneInfo
{
    public MainScene()
    {
        scenePath = "Default";
        sceneName = "Default";
    }
    public MainScene(Scene s, string name)
    {
        scene = s;
        sceneName = name;
        scenePath = scene.path;
    }

    public override SceneTypes GetSceneType()
    {
        return type = SceneTypes.MAIN;
    }
}

public class BattleScene : SceneInfo
{
    public List<Baddies> allowedEnemies;

    public BattleScene()
    {
        scenePath = "Default";
        sceneName = "Default";
    }
    public BattleScene(Scene s, string name)
    {
        scene = s;
        sceneName = name;
        scenePath = scene.path;
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
    private Dictionary<string, SceneInfo> scenes;
    string filePath;
    string jsonData;
    SceneInfo activeScene;

    static JsonSerializerSettings settings = new JsonSerializerSettings
    {
        TypeNameHandling = TypeNameHandling.All,
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
    };

    JrpgSceneManager()
    {
        scenes = new Dictionary<string, SceneInfo>();
        Debug.Log("Construct");
    }

    private void OnEnable()
    {
        filePath = Application.dataPath + "/SceneIndex.json";
        scenes = new Dictionary<string, SceneInfo>();
    }

    public void LoadScene(SceneInfo scene, LoadSceneMode mode = LoadSceneMode.Single)
    {
        activeScene = scene;
        SceneManager.LoadScene(scene.scenePath, mode);
    }

    public AsyncOperation LoadSceneAsync(SceneInfo scene, LoadSceneMode mode = LoadSceneMode.Single)
    {
        activeScene = scene;
        return SceneManager.LoadSceneAsync(scene.scenePath, mode);
    }

    public void LoadScene(string scene, LoadSceneMode mode = LoadSceneMode.Single)
    {
        activeScene = scenes[scene];
        SceneManager.LoadScene(scenes[scene].scenePath, mode);
    }

    public AsyncOperation LoadSceneAsync(string scene, LoadSceneMode mode = LoadSceneMode.Single)
    {
        activeScene = scenes[scene];
        return SceneManager.LoadSceneAsync(scenes[scene].scenePath, mode);
    }

    public SceneInfo ActiveScene
    {
        get
        {
            return activeScene;
        }
        #if UNITY_EDITOR
        set
        {
            activeScene = value;
        }
        #endif
    }

    // I think about adding a get function and keeping the lists as private in here.
    // I need to consider any other methods needed. And, what's the best way to save this data?
    public Dictionary<string, SceneInfo> Scenes
    {
        get
        {
            if (scenes == null)
            {
                scenes = new Dictionary<string, SceneInfo>();
            }
            return scenes;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
