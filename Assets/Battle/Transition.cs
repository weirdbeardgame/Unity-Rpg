using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;

// Transition is looking for a data that no longer exists when the asset manager no more exists. Why does it no more exist?

[CustomEditor(typeof(Transition))]
class EnemySelect : Editor
{
    List <Baddies> baddieList;
    List<string> names;
    int count;
    GameAssetManager manager;
    bool isInit;
    Transition transition;
    TransitionData transitionData;
    Dictionary<Scene, TransitionData> allowedMapDataEdit;
    private void OnEnable()
    {
        Init();
    }

    public void Init()
    {
        baddieList = new List<Baddies>();
        names = new List<string>();
        manager = GameAssetManager.Instance;
        transition = (Transition)target;
        allowedMapDataEdit = new Dictionary<Scene, TransitionData>();
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
        }
        isInit = true;
    }

    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();
        if (GUILayout.Button("Add Scene"))
        {
            transitionData = new TransitionData();
            transitionData.Create();
            allowedMapDataEdit.Add(SceneManager.GetActiveScene(), transitionData);
        }
        if (transitionData)
        {
            SerializedObject obj = new SerializedObject(transitionData);
            SerializedProperty list = serializedObject.FindProperty("allowedEnemies");
            EditorGUILayout.PropertyField(list);
        }
        EditorUtility.SetDirty(this);
    }
};
#endif

public class TransitionData : ScriptableObject
{
    Scene currentScene;
    // The Scene to transport to. I wonder if I should have a custom Scene class to handle Scene categories like Battle Scene or Main Scene
    public Scene battleScene;

    public List<Asset> allowedEnemies;

    // This should only happen once!
    // I have this function instead of the constructor as ScriptableObject shouldn't call new.
    // This is to ensure the data in the class is being properly initalized
    public void Create()
    {
        // I can either have this or somekind of Dictionary design
        currentScene = SceneManager.GetActiveScene();
        allowedEnemies = new List<Asset>();
    }
}

// The Initalizer of the Battle
// Needs a way to select which spawned enemies can appear per map!
// One issue is that i've been combining two things that need to be seperate...
// This should be a global class that should handle current map screen.
public class Transition : MonoBehaviour
{
    GameManager manager;
    Party playerParty;
    Battle battle;
    commandMenus Menus;
    GameObject scripts;
    GameObject mainCamera;
    GameObject BattleObject;
    Camera battleCamera;

    // Should I use a shader or the animator componet for battle swirl animations?
    //Shader battleSwirl;
    //Animator battleSwirl;

    // ToDo replace with Scene implementation.
    [SerializeField]
    string mapLoad = "BattleScene";

    [SerializeField]
    public Dictionary<Scene, TransitionData> allowedMapData;

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

            AsyncOperation async = SceneManager.LoadSceneAsync(allowedMapData[SceneManager.GetActiveScene()].battleScene.name);

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
