using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

public class Party : MonoBehaviour
{
    string PlayerData;
    public List<Player> PartyMembers;

    GameAssetManager manager;

    Player PlayerObject;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);

        manager = GetComponent<GameAssetManager>();
        if (manager.isFilled() > 0)
        {
            foreach(var asset in manager.Data)
            {
                if (asset.Value.indexedType == AssetType.PLAYER)
                {
                    Player pTemp = (Player)asset.Value.Data;
                    PartyMembers.Add(pTemp);
                }
            }
        }
    }


    void Update()
    {

    }


}

