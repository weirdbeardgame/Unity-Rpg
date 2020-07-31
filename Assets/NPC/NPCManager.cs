using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using System.Collections;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
//using UnityEngine.Experimental.Rendering.Universal;

public class NPCManager : MonoBehaviour, IReceiver
{
    List<NPCData> _Data;
    NPCData _Initalizer;
    string filePath = "Assets/NPC.json";
    string NPCData;

    int MapID;
    Vector2 position;
    bool Collided;

    CollisionMessage _Collided;

    Queue<object> Inbox; // The Receiver

    Sprite NPCSprite;

    DialogueManager manager;

    Messaging _Messenger;

    public List<NPCData> NPC
    {
        get
        {
            return _Data;
        }
    }

    void Start()
    {
        Initalize();
        Subscribe();
    }

    static JsonSerializerSettings settings = new JsonSerializerSettings
    {
        TypeNameHandling = TypeNameHandling.All
    };

    // This is runtime style NPC's
    void Initalize()
    {
        if (File.Exists(filePath))
        {
            _Data = new List<NPCData>();
            _Initalizer = new NPCData();
            Inbox = new Queue<object>();
            NPCData = File.ReadAllText(filePath);
            _Data = JsonConvert.DeserializeObject<List<NPCData>>(NPCData, settings);
        }

        CreateNPCS();
    }

    public void CreateNPCS()
    {
        MapID = SceneManager.GetActiveScene().buildIndex;

        for (int i = 0; i < _Data.Count; i++)
        {
            if (_Data[i].MapID == MapID)
            {
                _Data[i].NPC = new GameObject(_Data[i].NpcName);

                NPCSprite = Resources.Load<Sprite>(_Data[i].SpritePath);

                _Data[i].NPC.AddComponent<SpriteRenderer>();
                //_Data[i].NPC.AddComponent<Light2D>();
                _Data[i].NPC.GetComponent<SpriteRenderer>().sprite = NPCSprite;
                _Data[i].NPC.GetComponent<SpriteRenderer>().sortingOrder = 1;
                //_Data[i].NPC.GetComponent<Light2D>().lightType = Light2D.LightType.Point;


                position.x = _Data[i].X;
                position.y = _Data[i].Y;

                _Data[i].NPC.AddComponent<RectTransform>();
                _Data[i].NPC.GetComponent<RectTransform>().localPosition = position;

                _Data[i].NPC.AddComponent<Rigidbody2D>();
                _Data[i].NPC.GetComponent<Rigidbody2D>().gravityScale = 0;

                _Data[i].NPC.AddComponent<BoxCollider2D>();
                _Data[i].NPC.GetComponent<BoxCollider2D>().isTrigger = true;

                _Data[i].NPC.AddComponent<Collision>();
                _Data[i].NPC.GetComponent<Collision>().Initalize(_Data[i].NpcID);

                _Data[i].NPC.AddComponent<NPCMovement>();
                _Data[i].NPC.GetComponent<NPCMovement>().Initalize(_Data[i].Directions);

                _Data[i].NPC.AddComponent<Interact>();
                _Data[i].NPC.GetComponent<Interact>().Message = _Data[i].NpcID;
            }
        }
    }

    /*void checkQuest()
   {
       questMessage message = (questMessage)inbox.Dequeue();

       if (message.getstate() == QuestState.IS_ACTIVE)
       {
           if (QID == message.getID())
           {
               // Does NPC have event or quest?
           }
       }
   }*/

    void Talk()
    {
        // What I'd need to do first is detect if there was a collission with an NPC's collider.

        for (int i = 0; i < _Data.Count; i++)
        {
            if (_Collided.CollidedID == _Data[i].NpcID)
            {
                if (_Data[i].Flag == QuestFlag.HAS_QUEST)
                {

                }
            }

        }
    }

    public void Subscribe()
    {
        _Messenger = FindObjectOfType<Messaging>();
        _Messenger.Subscribe(MessageType.QUEST, this); // there's going to be alot of things subscribed too in here.
        _Messenger.Subscribe(MessageType.COLLISION, this);
    }

    public void Receive(object message)
    {
        Inbox.Enqueue(message);
    }

    public void Unsubscribe()
    {
        _Messenger.Unsubscribe(MessageType.QUEST, this);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (Inbox.Count > 0 && Inbox.Peek() is CollisionMessage)
        {
            _Collided = (CollisionMessage)Inbox.Dequeue();
        }

        if (Input.GetButtonDown("Submit") && _Collided != null)
        {
            Talk();
        }

        /*if ((_Data[0].Flag & QuestFlag.HAS_QUEST) > 0)
          {
              //checkQuest(); // This is Ref's prefered style ie. Constant polling of player's quest state
              // This will get more complex later to cover what part of the quest we're in and if they have an event for that part etc.
          }*/
    }
}
