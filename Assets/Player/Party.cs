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

    Player PlayerObject;

    string FilePath = "Assets/Player/Actors.json";

    JsonSerializerSettings settings = new JsonSerializerSettings
    {
        TypeNameHandling = TypeNameHandling.All
    };

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);

        if (File.Exists(FilePath))
        {
            PlayerData = File.ReadAllText(FilePath);
            PartyMembers = JsonConvert.DeserializeObject<List<Player>>(PlayerData);
        }

        for (int i = 0; i < PartyMembers.Count; i++)
        {
            PartyMembers[i].createWeaponSlots();
        }
    }


    void Update()
    {

    }


}

