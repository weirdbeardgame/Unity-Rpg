using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using menu;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(Transition))]
class EnemySelect : Editor
{
    List <Baddies> baddieList;
    List<string> names;
    List<int> index;
    int count;
    GameAssetManager manager;
    Transition transition;
    bool isInit;

    public void Init()
    {
        baddieList = new List<Baddies>();
        names = new List<string>();
        index = new List<int>();
        transition = FindObjectOfType<Transition>();
        manager = FindObjectOfType<GameAssetManager>();
        manager.Init();
        if (manager.isFilled() > 0)
        {
            foreach(var asset in manager.Data)
            {
                if (asset.Value.indexedType == AssetType.ENEMY)
                {
                    Baddies bad = (Baddies)asset.Value.Data;
                    bad.Prefab = AssetDatabase.LoadAssetAtPath<GameObject>(bad.prefabPath);
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
        if (!isInit)
        {
            Init();
        }
        GUILayout.Label("Add Enemies");
        if (GUILayout.Button("Add Enemy"))
        {
            count += 1;
        }
        for (int i = 0; i < count; i++)
        {
            index.Add(new int());
            index[i] = EditorGUILayout.Popup(index[i], names.ToArray());
            transition.AddBaddies(count, i, baddieList[index[i]]);
        }
    }
};
#endif

// The Initalizer of the Battle
// Needs a way to select which spawned enemies can appear per map!
public class Transition : MonoBehaviour
{
    Scene CurrentScene;
    StateMachine states;
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
    int index = 1;
    [SerializeField]
    int X = 0;
    [SerializeField]
    int Y = 0;
    int PreviousIndex;
    PlayerMovement move;

    List<Baddies> allowedEnemies;

    public void AddBaddies(int count, int index, Baddies bad)
    {
        if (allowedEnemies == null)
        {
            allowedEnemies = new List<Baddies>();
        }
        else if (allowedEnemies.Count < count)
        {
            allowedEnemies.Add(new Baddies());
        }
        allowedEnemies[index] = bad;
    }

    // Start is called before the first frame update
    void Start()
    {
        move = FindObjectOfType<PlayerMovement>();
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
        int baddieIndex = rand.Next(allowedEnemies[0].id, allowedEnemies[allowedEnemies.Count].id); // This seems a grave misuse of enemy ID
        for (int i = 0; i < amount; i++)
        {
            // Spawn and add into BattleEnemies from here
            BattleObject.GetComponent<BattleEnemies>().Insert(Instantiate<GameObject>(allowedEnemies[baddieIndex].Prefab));
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
                states = FindObjectOfType<StateMachine>();

                Destroy(states.GetComponent<MenuManager>());

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

                batttleStartMessage = new gameStateMessage();
                batttleStartMessage.construct(States.BATTLE, states.CurrrentFlag);

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
