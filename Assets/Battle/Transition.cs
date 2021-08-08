using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// The Initalizer of the Battle
// Needs a way to select which spawned enemies can appear per map!
using menu;

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

    [SerializeField]
    public List<Baddies> allowedEnemies;

    // Start is called before the first frame update
    void Start()
    {
        move = FindObjectOfType<PlayerMovement>();
    }

    /************************************************************************************************
    * TODO:
    * I want to loop through a random range set between 1 and 3.
    * This way I can determine count of enemies that will spawn.
    * I want to loop through a random range of enemies set as allowed based on map and active flags
    ************************************************************************************************/
    void selectEnemies()
    {
        var rand = new System.Random();
        int amount = rand.Next(1, 3);
        int baddieIndex = rand.Next(allowedEnemies[0].id, allowedEnemies[allowedEnemies.Count].id); // This seems a grave misuse of enemy ID
        for (int i = 0; i < amount; i++)
        {
            // Spawn and add into BattleEnemies from here
            BattleObject.GetComponent<BattleEnemies>().Insert(Instantiate<GameObject>(allowedEnemies[baddieIndex].BattlePrefab));
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

                battle = BattleObject.AddComponent<Battle>();
                Menus = FindObjectOfType<commandMenus>();
                Menus.Initlaize();

                message = FindObjectOfType<Messaging>(); // Grab Messaging from states

                BattleObject.AddComponent<Skills>();
                BattleObject.AddComponent<Enemies>();
                BattleObject.AddComponent<BattleSlots>();
                BattleObject.AddComponent<CommandQueue>();
                BattleObject.AddComponent<BattleEnemies>();
                BattleObject.AddComponent<BattlePlayers>();
                BattleObject.AddComponent<BattleItemMenu>();

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
