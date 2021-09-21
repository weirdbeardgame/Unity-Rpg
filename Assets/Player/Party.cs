﻿using System.Collections;
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

        manager = GameAssetManager.Instance;
        if (manager.isFilled())
        {
            foreach(var asset in manager.Data)
            {
                if (asset.Value is Player)
                {
                    PartyMembers.Add((Player)asset.Value);
                }
            }
        }
    }


    void Update()
    {

    }


}

