using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    StateMachine state;
    List<Flags> flags;

    GameAssetManager assetManager;

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManager();
            }
            return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        assetManager = GameAssetManager.Instance;
        state = GetComponent<StateMachine>();
        flags = new List<Flags>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
