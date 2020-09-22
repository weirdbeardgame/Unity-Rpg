using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public int NpcId;
    public SpeakerData Speaker;
    public NPCData NpcData;
    NPCManager NpcM;

    DialogueManager Dialogue;

    private void Start()
    {
        Dialogue = FindObjectOfType<DialogueManager>();
        NpcM = FindObjectOfType<NPCManager>();
    }

    bool Collided;

    void ApplyNPC()
    {
        // When editing NPC's. Auto show the selected NPC ingame world
        NpcData = NpcM.NPC[NpcId];
        //GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(NpcData.SpritePath);
    }

    void Talk()
    {
        Dialogue.Talk(NpcData);
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


    private void Update()
    {
        ApplyNPC();

        if (Input.GetButtonDown("Submit") && Collided )
        {
            Talk();
        }
    }
}
