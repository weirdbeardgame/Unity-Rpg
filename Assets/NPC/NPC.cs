using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    NPCManager NpcM;
    NPCData NpcData;
    public int NpcID;
    StateMachine states;    
    DialogueManager Dialogue;
    public GameObject SpeakerProfile;

    private void Start()
    {
        Dialogue = FindObjectOfType<DialogueManager>();
        NpcM = FindObjectOfType<NPCManager>();
        states = FindObjectOfType<StateMachine>();
    }

    bool Collided;

    void ApplyNPC()
    {
        // When editing NPC's. Auto show the selected NPC ingame world
        NpcData = NpcM.NPC[NpcID];
        //GetComponent<SpriteRenderer>().sprite = Sprite.Create(NpcData.Texture, new Rect(0.0f, 0.0f, NpcData.Texture.width, NpcData.Texture.height), Vector2.one);
    }

    void Talk()
    {
        Dialogue.OpenDialogueBox(NpcData);
        Collided = false;
        return;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Collided = true;
        }

        else
        {
            Collided = false;
        }
    }

    void pollEvents()
    {
        for (int i = 0; i < NpcData.EventData.Count; i++)
        {
            if (NpcData.EventData[i].RequiredFlag == states.CurrrentFlag)
            {
                NpcData.EventData[i].Execute();
            }
        }
    }

    private void Update()
    {
        ApplyNPC();

        if (Input.GetButtonDown("Submit") && Collided )
        {
            pollEvents();
        }
    }
}
