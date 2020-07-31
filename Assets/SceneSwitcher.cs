using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] string mapLoad = "BattleScene";
    [SerializeField] int index = 1;
    [SerializeField] int X = 0;
    [SerializeField] int Y = 0;

    PlayerMovement move;

    Vector3 v3;
    // Start is called before the first frame update
    void Start()
    {
        move = FindObjectOfType<PlayerMovement>();
    }

    IEnumerator OnTriggerEnter2D( Collider2D other)
    {
        v3.x = X;
        v3.y = Y;

        if (other.gameObject.tag == "Player")
        {
            move.canMove = false;

            Scene currentScene = SceneManager.GetActiveScene();
            other.gameObject.transform.position = v3; 

            AsyncOperation async = SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);

            while (!async.isDone)
            {
                yield return null;
            }

            Scene load = SceneManager.GetSceneByName(mapLoad);
            SceneManager.MoveGameObjectToScene(other.gameObject, load);
            SceneManager.UnloadSceneAsync(currentScene);
        }
    }


    public void switchScene()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
 