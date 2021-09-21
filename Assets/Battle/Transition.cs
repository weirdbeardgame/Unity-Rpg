using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using menu;

#if UNITY_EDITOR
using UnityEditor;

// Transition is looking for a data that no longer exists when the asset manager no more exists. Why does it no more exist?

[CustomEditor(typeof(Transition))]
class EnemySelect : Editor
{
    List <Baddies> baddieList;
    List<string> names;
    List<int> index;
    int count;
    GameAssetManager manager;
    bool isInit;

    Transition transition;

    private void OnEnable()
    {
        Init();
    }

    public void Init()
    {
        baddieList = new List<Baddies>();
        names = new List<string>();
        index = new List<int>();
        transition = (Transition)target;
        manager = GameAssetManager.Instance;
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
        GUILayout.Label("Add Enemies");
        if (GUILayout.Button("Add Enemy"))
        {
            count += 1;
        }
        for (int i = 0; i < count; i++)
        {
            index.Add(new int());
            index[i] = EditorGUILayout.Popup(index[i], names.ToArray());
            if (GUI.changed)
            {
                transition.AllowedEnemies.Add(index[i]); // It really seems like it's serializing a light refrence to what exists in the asset manager rather then making a wholenother copy
            }
        }

        EditorUtility.SetDirty(this);
    }
};
#endif

// The Initalizer of the Battle
// Needs a way to select which spawned enemies can appear per map!
[System.Serializable]
public class Transition : MonoBehaviour
{
    Scene CurrentScene;
    GameManager manager;
    Party playerParty;
    Battle battle;
    Messaging message;
    gameStateMessage batttleStartMessage;
    commandMenus Menus;
    Vector2 v2;
    GameObject scripts;
    GameObject mainCamera;
    GameObject BattleObject;
    Camera battleCamera;
    [SerializeField]
    string mapLoad = "BattleScene";
    [SerializeField] 
    int index = 0;
    int PreviousIndex;
    PlayerMovement move;
    private List<int> allowedEnemies;
    Enemies enemies;

    int maxEnemies = 4;


    // This doesn't seem wrong. GameAssetManager isn't always loaded and data disappears?
    public List<int> AllowedEnemies
    {
        set
        {
            if (allowedEnemies.Count < maxEnemies)
            {
                allowedEnemies = value;
                index += 1;
            }
            else
            {
                allowedEnemies[index] = value[index];
            }
        }
        get
        {
            return allowedEnemies;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        move = FindObjectOfType<PlayerMovement>();
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
        int baddieIndex = rand.Next(allowedEnemies[0], allowedEnemies[allowedEnemies.Count]); // This seems a grave misuse of enemy ID
        for (int i = 0; i < amount; i++)
        {
            // Spawn and add into BattleEnemies from here
            BattleObject.GetComponent<BattleEnemies>().Insert(Instantiate<GameObject>(enemies.enemyData[allowedEnemies[baddieIndex]].prefab));
        }
    }

    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        MonoBehaviour.Destroy(other.gameObject);

        if (other.gameObject.tag == "Player")
        {
            move.canMove = false;

            PreviousIndex = SceneManager.GetActiveScene().buildIndex;
            CurrentScene = SceneManager.GetActiveScene(); // The previous scene we were on.

            //other.gameObject.transform.position = v2;

            AsyncOperation async = SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);

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

                message = FindObjectOfType<Messaging>(); // Grab Messaging from states

                BattleObject.AddComponent<Skills>();
                battle = BattleObject.AddComponent<Battle>();
                BattleObject.AddComponent<BattleSlots>();
                BattleObject.AddComponent<CommandQueue>();
                BattleObject.AddComponent<BattleEnemies>();
                BattleObject.AddComponent<BattlePlayers>();
                BattleObject.AddComponent<BattleItemMenu>();

                selectEnemies();

                message.Enqueue(batttleStartMessage);
                message.Init();
                selectEnemies();

                battle.StartBattle(CurrentScene, BattleObject, PreviousIndex);

                yield return null;
            }

            Scene load = SceneManager.GetSceneByName(mapLoad);
            SceneManager.MoveGameObjectToScene(other.gameObject, load);
        }
    }
}
