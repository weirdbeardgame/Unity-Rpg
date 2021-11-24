using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using Newtonsoft.Json;

#if UNITY_EDITOR
using UnityEditor;

// Transition is looking for a data that no longer exists when the asset manager no more exists. Why does it no more exist?

[CustomEditor(typeof(Transition))]
class EnemySelect : Editor
{
    int count;
    int sceneIndex;
    int enemySelected;

    List<string> names;
    List<Baddies> baddieList;
    List<Baddies> toSet;
    List<int> index;

    GameAssetManager manager;
    Transition transition;
    JrpgSceneManager scenes;

    List<BattleScene> battleScenes;
    List<string> sceneNames;
    string path = "Assets/Transition.json";
    string jsonData;

    bool isInit;

    private void OnEnable()
    {
        Init();
    }

    static JsonSerializerSettings settings = new JsonSerializerSettings
    {
        TypeNameHandling = TypeNameHandling.All,
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
    };

    public void Init()
    {
        names = new List<string>();

        baddieList = new List<Baddies>();

        manager = GameAssetManager.Instance;
        scenes = FindObjectOfType<JrpgSceneManager>();
        transition = (Transition)target;

        battleScenes = new List<BattleScene>();
        transition.allowedMapData = new Dictionary<string, BattleScene>();

        if (manager.isFilled())
        {
            foreach(var asset in manager.Data)
            {
                if (asset.Value is Baddies)
                {
                    Baddies bad = (Baddies)asset.Value;
                    bad.prefab = AssetDatabase.LoadAssetAtPath<GameObject>(bad.path);

                    baddieList.Add(bad);
                    names.Add(bad.Data.creatureName);
                }
            }
        }

        if (File.Exists(path))
        {
            jsonData = File.ReadAllText(path);

            // This is the partial result of Direct Cast causing issue. When abstracted through asset Manager. Shit works
            transition.allowedMapData = JsonConvert.DeserializeObject<Dictionary<string, BattleScene>>(jsonData, settings);
        }

        index = new List<int>();

        isInit = true;
    }

    private void loadScenes()
    {
        foreach (var scene in scenes.Scenes)
        {
            if (scene.type == SceneTypes.BATTLE && scene is BattleScene)
            {
                battleScenes.Add((BattleScene)scene);
            }
        }

        sceneNames = new List<string>();
        foreach (var scene in battleScenes)
        {
            sceneNames.Add(scene.sceneName);
        }
    }

    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();
        if (scenes == null || !isInit)
        {
            Init();
        }
        if (scenes != null && isInit)
        {
            if (sceneNames == null || sceneNames.Count == 0)
            {
                loadScenes();
            }
            if (GUILayout.Button("Add Scene"))
            {
                if (transition.allowedMapData == null)
                {
                    transition.allowedMapData = new Dictionary<string, BattleScene>();
                }
                else
                {
                    transition.allowedMapData.Add(scenes.ActiveScene.sceneName, new BattleScene());
                }
            }
            if (transition.allowedMapData.ContainsKey(scenes.ActiveScene.sceneName))
            {
                if (index.Count <= 0 && transition.allowedMapData[scenes.ActiveScene.sceneName].allowedEnemies.Count > 0)
                {
                    for(int i = 0; i < transition.allowedMapData[scenes.ActiveScene.sceneName].allowedEnemies.Count; i++)
                    {
                        index.Add(transition.allowedMapData[scenes.ActiveScene.sceneName].allowedEnemies[i]);
                    }
                }
                BattleScene bScene = battleScenes[EditorGUILayout.Popup(sceneIndex, sceneNames.ToArray())];
                transition.allowedMapData[scenes.ActiveScene.sceneName] = bScene;
                // Need to list enemy names to set which enemy can be fought on battle map.
                if (GUILayout.Button("Add Enemy"))
                {
                    count += 1;
                    if (transition.allowedMapData[scenes.ActiveScene.sceneName].allowedEnemies == null)
                    {
                        transition.allowedMapData[scenes.ActiveScene.sceneName].allowedEnemies = new List<int>();
                    }
                    // Need to add custom list editor. Right now i'd have a list of indexes...
                    if (transition.allowedMapData[scenes.ActiveScene.sceneName].allowedEnemies.Count < count)
                    {
                        transition.allowedMapData[scenes.ActiveScene.sceneName].allowedEnemies.Add(new int());
                        index.Add(new int());
                    }
                }

                for (int i = 0; i < transition.allowedMapData[scenes.ActiveScene.sceneName].allowedEnemies.Count; i++)
                {
                    // I can print from names. It's valid and has data. Index is what's broken for some reason
                    index[i] = EditorGUILayout.Popup(index[i], names.ToArray());
                    transition.allowedMapData[scenes.ActiveScene.sceneName].allowedEnemies[i] = baddieList[index[i]].id;
                }

                // Write back to Battle Scene Asset. Write Dictionary index to file.
                if (GUILayout.Button("Save"))
                {
                    manager.WriteBack(transition.allowedMapData[scenes.ActiveScene.sceneName].sceneName, transition.allowedMapData[scenes.ActiveScene.sceneName]);
                    // Need to write the scenes that are associated here. Perhaps use a tree structure?
                    string save = JsonConvert.SerializeObject(transition.allowedMapData, settings);
                    File.WriteAllText(path, save);
                }
            }
        }
    }
};
#endif

// The Initalizer of the Battle.
// Needs a way to select which spawned enemies can appear per map!
// One issue is that i've been combining two things that need to be seperate...
// This should be a global class that should handle current map screen.

public class Transition : MonoBehaviour
{
    Battle battle;
    Party playerParty;
    commandMenus Menus;
    GameManager manager;

    JrpgSceneManager scenes;

    [SerializeField]
    GameObject scripts;

    [SerializeField]
    GameObject BattleObject;

    Camera battleCamera;

    SceneInfo previous;

    //Should I use a shader or the animator componet for battle swirl animations?
    //Shader battleSwirl;
    //Animator battleSwirl;

    // ToDo replace with Scene implementation.
    [SerializeField]
    string path = "Assets/Transition.json";
    string jsonData;

    public Dictionary<string, BattleScene> allowedMapData;

    Enemies enemies;

    int maxEnemies = 4;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.Instance;
        scenes = FindObjectOfType<JrpgSceneManager>();
        if (File.Exists(path))
        {
            jsonData = File.ReadAllText(path);
            allowedMapData = JsonConvert.DeserializeObject<Dictionary<string, BattleScene>>(jsonData);
        }
    }

    /***********************************************************************************************************************************
    * TODO:
    * I want to loop through a random range set between 1 and 3.
    * This way I can determine count of enemies that will spawn.
    * I want to loop through a random range of enemies set as allowed based on map and active flags
    * The allowed enemies should differ per change of maps and set flags. If the players farther along they should face harder enemies!
    ***********************************************************************************************************************************/
    void selectEnemies()
    {
        var rand = new System.Random();
        int amount = rand.Next(1, 3);

        // Grab Active SceneInfo and grab baddie indexes in the battleScene. Select from random baddie id's and load into battle scene.

        for (int i = 0; i < amount; i++)
        {
            int index = rand.Next(allowedMapData[previous.sceneName].allowedEnemies.Count);
            // Spawn and add into BattleEnemies from here
            BattleObject.GetComponent<BattleEnemies>().Insert(enemies.enemyData[allowedMapData[previous.sceneName].allowedEnemies[index]]);
        }
    }

    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        previous = scenes.ActiveScene;
        MonoBehaviour.Destroy(other.gameObject);

        enemies = scripts.GetComponent<Enemies>();

        DontDestroyOnLoad(this);

        if (other.gameObject.tag == "Player")
        {
            // Something else is causing the scene to fail to load
            AsyncOperation async = scenes.LoadSceneAsync(allowedMapData[scenes.ActiveScene.sceneName], LoadSceneMode.Additive);

            while (!async.isDone)
            {
                yield return null;
            }

            AsyncOperation unload = scenes.UnloadSceneAsync(previous);

            while (!unload.isDone)
            {
                BattleObject = Instantiate(BattleObject);
                Menus = FindObjectOfType<commandMenus>();

                battle = BattleObject.GetComponent<Battle>();

                selectEnemies();

                BattleObject.GetComponent<BattlePlayers>().Init(BattleObject);
                battle.StartBattle(previous, BattleObject);

                yield return null;
            }

            SceneManager.MoveGameObjectToScene(BattleObject, SceneManager.GetSceneByName(allowedMapData[previous.sceneName].sceneName));
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(allowedMapData[previous.sceneName].sceneName));

            gameObject.SetActive(false);
        }
    }

    // Should this be in Battle.CS?
    void AddExp()
    {
        // After Battle. Add EXP. Load scene to show EXP and Levels. And send player back to the previous scene.
    }
}
