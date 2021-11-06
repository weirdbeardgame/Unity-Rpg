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
class ContextSceneMenu : Editor
{
    static JrpgSceneManager Instance = JrpgSceneManager.Instance;
    static string filePath = Application.dataPath + "/SceneIndex.json";

    static int sceneID = 0;

    static JsonSerializerSettings settings = new JsonSerializerSettings
    {
        TypeNameHandling = TypeNameHandling.All,
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
    };

    [MenuItem("CONTEXT/unity/Add_Main_Scene", false, 0)]
    public static void MainScene(MenuCommand s)
    {
        //Instance.mainScenes.Add((Scene)s.context);
    }

    [MenuItem("CONTEXT/unity/Add_Battle_Scene", false, 0)]
    public static void BattleScene(MenuCommand s)
    {
        //Scene temp = (Scene)s;
        //Instance.battleScenes.Add(temp);
    }

    [MenuItem("Assets/New_Scene", false, 0)]
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

    [MenuItem("Assets/New_Battle_Scene", false, 0)]
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
}
#endif

// Below are SceneTypes. I like how the scenes all derive the same type but not sure about that being an interface...
public class SceneInfo : PropertyAttribute
{
    public SceneTypes type;
    public virtual SceneTypes GetSceneType()
    {
        return type;
    }
}

[Serializable]
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
public class JrpgSceneManager : MonoBehaviour
{
    public List<SceneInfo> scenes;
    public GameObject selfRef;

    string filePath;
    string jsonData;

    static JsonSerializerSettings settings = new JsonSerializerSettings
    {
        TypeNameHandling = TypeNameHandling.All,
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
    };

    JrpgSceneManager()
    {
        if (instance == null)
        {
            instance = this;
        }
        if (instance != null && instance != this)
        {
            instance = null;
        }
        scenes = new List<SceneInfo>();
        if (File.Exists(filePath))
        {
            jsonData = File.ReadAllText(filePath);
            scenes = JsonConvert.DeserializeObject<List<SceneInfo>>(filePath, settings);
        }
        Debug.Log("Construct");
    }

    private void Awake() {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        if (instance != null && instance != this)
        {
            instance = null;
        }
        filePath = Application.dataPath + "/SceneIndex.json";
        scenes = new List<SceneInfo>();
        if (File.Exists(filePath))
        {
            jsonData = File.ReadAllText(filePath);
            scenes = JsonConvert.DeserializeObject<List<SceneInfo>>(filePath, settings);
            Debug.Log("Awake");
        }
    }

    private static JrpgSceneManager instance;

    public static JrpgSceneManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new JrpgSceneManager();
            }
            return instance;
        }
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
