﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;

// Transition is looking for a data that no longer exists when the asset manager no more exists. Why does it no more exist?

[CustomEditor(typeof(Transition))]
class EnemySelect : Editor
{
    int count;
    int sceneIndex;

    List<string> names;
    List<Baddies> baddieList;

    GameAssetManager manager;
    Transition transition;

    [SerializeField]
    JrpgSceneManager scenes;

    List<SceneInfo> battleScenes;
    List<string> sceneNames;

    private void OnEnable()
    {
        Init();
    }

    public void Init()
    {
        names = new List<string>();

        baddieList = new List<Baddies>();

        manager = GameAssetManager.Instance;
        transition = (Transition)target;

        battleScenes = new List<SceneInfo>();
        transition.allowedMapData = new Dictionary<Scene, BattleScene>();

        if (manager.isFilled())
        {
            foreach(var asset in manager.Data)
            {
                if (asset.Value is Baddies)
                {
                    Baddies bad = (Baddies)asset.Value;
                    bad.prefab = AssetDatabase.LoadAssetAtPath<GameObject>(bad.prefabPath);

                    baddieList.Add(bad);
                    names.Add(bad.Data.creatureName);
                }
            }

            int maxScene = SceneManager.sceneCount;
            foreach(var scene in scenes.Scenes)
            {
                if (scene.type == SceneTypes.BATTLE)
                {
                    battleScenes.Add(scene);
                }
            }
            sceneNames = new List<string>();

            foreach(var scene in battleScenes)
            {
                sceneNames.Add(scene.sceneName);
            }
        }
    }

    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();
        scenes = (JrpgSceneManager)EditorGUILayout.ObjectField(scenes, typeof(JrpgSceneManager), true);
        if (scenes != null)
        {
            if (sceneNames == null)
            {
                Init();
            }
            if (transition.allowedMapData == null)
            {
                transition.allowedMapData = new Dictionary<Scene, BattleScene>();
            }
            if (GUILayout.Button("Add Scene"))
            {
                transition.allowedMapData.Add(scenes.ActiveScene.scene, new BattleScene());
            }
            if (transition.allowedMapData.ContainsKey(scenes.ActiveScene.scene))
            {
                transition.allowedMapData[scenes.ActiveScene.scene] = (BattleScene)scenes.Scenes[EditorGUILayout.Popup(sceneIndex, sceneNames.ToArray())];
            }
            EditorUtility.SetDirty(this);
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

    [SerializeField]
    JrpgSceneManager scenes;

    GameObject scripts;
    GameObject mainCamera;
    GameObject BattleObject;

    Camera battleCamera;

    //Should I use a shader or the animator componet for battle swirl animations?
    //Shader battleSwirl;
    //Animator battleSwirl;

    // ToDo replace with Scene implementation.
    [SerializeField]
    string mapLoad = "BattleScene";

    [SerializeField]
    public Dictionary<Scene, BattleScene> allowedMapData;

    Enemies enemies;

    int maxEnemies = 4;

    // Start is called before the first frame update
    void Start()
    {
        enemies = FindObjectOfType<Enemies>();
        manager = GameManager.Instance;
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
        //int baddieIndex = rand.Next(allowedEnemies[0], allowedEnemies[allowedEnemies.Count]); // This seems a grave misuse of enemy ID
        for (int i = 0; i < amount; i++)
        {
            // Spawn and add into BattleEnemies from here
            //BattleObject.GetComponent<BattleEnemies>().Insert(Instantiate<GameObject>(enemies.enemyData[allowedEnemies[baddieIndex]].prefab));
        }
    }

    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        MonoBehaviour.Destroy(other.gameObject);

        if (other.gameObject.tag == "Player")
        {
            //PreviousIndex = SceneManager.GetActiveScene().buildIndex;
            //CurrentScene = SceneManager.GetActiveScene(); // The previous scene we were on.

            //other.gameObject.transform.position = v2;

            AsyncOperation async = scenes.LoadSceneAsync(allowedMapData[SceneManager.GetActiveScene()]);

            while (!async.isDone)
            {
                yield return null;
            }

            AsyncOperation unload = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

            while (!unload.isDone)
            {
                BattleObject = GameObject.Find("Battle");
                scripts = GameObject.Find("Scripts");
                mainCamera = GameObject.Find("Main Camera");

                Menus = FindObjectOfType<commandMenus>();
                Menus.Initlaize();

                BattleObject.AddComponent<Skills>();
                battle = BattleObject.AddComponent<Battle>();

                BattleObject.AddComponent<BattleSlots>();
                BattleObject.AddComponent<CommandQueue>();
                BattleObject.AddComponent<BattleEnemies>();
                BattleObject.AddComponent<BattlePlayers>();
                BattleObject.AddComponent<BattleItemMenu>();

                selectEnemies();

                battle.StartBattle(SceneManager.GetActiveScene(), BattleObject, -1);

                yield return null;
            }

            Scene load = SceneManager.GetSceneByName(mapLoad);
            SceneManager.MoveGameObjectToScene(other.gameObject, load);
        }
    }
}
