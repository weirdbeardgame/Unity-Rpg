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
        NpcData = NpcM.NPC[NpcID - 1];
        NpcData.CurrentSpeaker = SpeakerProfile;
    }

    void Talk()
    {
        //Dialogue.OpenDialogueBox(NpcData);
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Collided = false;
        }
    }

    void pollEvents()
    {
        states = FindObjectOfType<StateMachine>();
        Debug.Log("Current FLAG : " + states.CurrrentFlag.Flag);
        Debug.Log("Current FLAG ID : " + states.CurrrentFlag.ID);

        QuestEventData ToExecute;

        for (int i = 0; i < NpcData.EventData.Count; i++)
        {
            Debug.Log("FLAG : " + NpcData.EventData[i].RequiredFlag.Flag);
            Debug.Log("FLAG ID : " + NpcData.EventData[i].RequiredFlag.ID);

            if (NpcData.EventData[i].RequiredFlag.ID == states.CurrrentFlag.ID)
            {
                ToExecute = NpcData.EventData[i];
                Debug.Log("Event Executed");
                ToExecute.Execute(SpeakerProfile);
                NpcData.EventData.RemoveAt(i);
                return;
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
