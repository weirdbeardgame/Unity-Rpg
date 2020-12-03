using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// The Initalizer of the Battle
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


    [SerializeField] string mapLoad = "BattleScene";
    [SerializeField] int index = 1;
    [SerializeField] int X = 0;
    [SerializeField] int Y = 0;

    int PreviousIndex;

    PlayerMovement move;


    // Start is called before the first frame update
    void Start()
    {
        move = FindObjectOfType<PlayerMovement>();
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

                //Menus.Initlaize();

                BattleObject.AddComponent<BattleItemMenu>();
                BattleObject.AddComponent<BattleSlots>();

                message = FindObjectOfType<Messaging>(); // Grab Messaging from states
                BattleObject.AddComponent<Enemies>();
                BattleObject.AddComponent<BattlePlayers>();

                batttleStartMessage = ScriptableObject.CreateInstance<gameStateMessage>();
                batttleStartMessage.construct(States.BATTLE, states.CurrrentFlag);

                message.Init();

                BattleObject.AddComponent<Skills>();
                BattleObject.AddComponent<CommandQueue>();

                battle.StartBattle(CurrentScene, BattleObject, PreviousIndex);

                yield return null;
            }

            Scene load = SceneManager.GetSceneByName(mapLoad);
            //SceneManager.MoveGameObjectToScene(other.gameObject, load);
        }
    }
}
