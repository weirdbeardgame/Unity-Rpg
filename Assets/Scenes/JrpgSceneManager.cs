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

    private void OnEnable() {
        Instance = (JrpgSceneManager)target;
        Instance.names = new List<string>();
        if (Instance.Scenes != null)
        {
            foreach(var scene in Instance.Scenes)
            {
                Instance.names.Add(scene.sceneName);
            }
        }
    }

    public static void NewMainScene()
    {
        Scene s = new Scene();
        s = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Additive);
        EditorSceneManager.SaveScene(s);
        MainScene data = new MainScene(s, s.name);
        Instance.Scenes.Add(data);
        Instance.names.Add(s.name);
        //EditorSceneManager.MoveGameObjectToScene(Instance.selfRef, s);
    }

    public static void NewBattleScene()
    {
        Scene s = new Scene();
        s = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Additive);
        EditorSceneManager.SaveScene(s);
        BattleScene data = new BattleScene(s, s.name);
        Instance.Scenes.Add(data);
        Instance.names.Add(s.name);
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
        if (Instance.names.Count <= 0)
        {
            foreach(var scene in Instance.Scenes)
            {
                Instance.names.Add(scene.sceneName);
            }
        }
        if (Instance.names.Count > 0)
        {
            EditorGUI.BeginChangeCheck();
            sceneID = EditorGUILayout.Popup(sceneID, Instance.names.ToArray());
            Instance.ActiveScene = Instance.Scenes[sceneID];
            if (EditorGUI.EndChangeCheck())
            {
                EditorSceneManager.OpenScene(Instance.ActiveScene.scenePath);
            }
        }
        EditorUtility.SetDirty(Instance);
    }
}
#endif

// Below are SceneTypes
[Serializable]
public abstract class SceneInfo : Asset
{
    public SceneTypes type;
    public string sceneName;
    public string scenePath;
    public object scene;

    public virtual SceneTypes GetSceneType()
    {
        return type;
    }
}

// Should this be serializable?
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
        type = SceneTypes.MAIN;
    }

    public override Asset CreateAsset()
    {
        // Assume we already have the path.

        return this;
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
        allowedEnemies = new List<Baddies>();
        type = SceneTypes.BATTLE;
    }
    public BattleScene(SceneInfo s)
    {
        scene = s.scene;
        sceneName = s.sceneName;
        scenePath = s.scenePath;
        type = s.type;
        allowedEnemies = new List<Baddies>();
    }
    public BattleScene(Scene s, string name)
    {
        scene = s;
        sceneName = name;
        type = SceneTypes.BATTLE;
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
// This Scene manager is meant for upwards of FFX style of game where there is a specific map or scene that is laid out for battle scenarios.
[CreateAssetMenu, Serializable]
public class JrpgSceneManager : ScriptableObject
{
    [SerializeReference]
    private List<SceneInfo> scenes;

    public List<string> names;

    string filePath;
    string jsonData;
    SceneInfo activeScene;

    static JsonSerializerSettings settings = new JsonSerializerSettings
    {
        TypeNameHandling = TypeNameHandling.All,
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
    };

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

    public void LoadScene(string sceneID, LoadSceneMode mode = LoadSceneMode.Single)
    {
        foreach(var scene in scenes)
        {
            if (scene.sceneName == sceneID)
            {
                activeScene = scene;
                SceneManager.LoadScene(scene.scenePath, mode);
            }
        }
    }

    public AsyncOperation LoadSceneAsync(string sceneID, LoadSceneMode mode = LoadSceneMode.Single)
    {
        foreach(var scene in scenes)
        {
            if (scene.sceneName == sceneID)
            {
                activeScene = scene;
                return SceneManager.LoadSceneAsync(scene.scenePath, mode);
            }
        }
        return null;
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
        #else
        private set
        {
            activeScene = value;
        }
        #endif
    }

    // I think about adding a get function and keeping the lists as private in here.
    // I need to consider any other methods needed. And, what's the best way to save this data?
    public List<SceneInfo> Scenes
    {
        get
        {
            if (scenes == null)
            {
                scenes = new List<SceneInfo>();
            }
            return scenes;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
