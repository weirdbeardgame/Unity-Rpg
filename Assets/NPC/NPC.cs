using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    NPCManager NpcM;
    NPCData NpcData;
    public int NpcID;
    GameManager manager;
    DialogueManager Dialogue;
    public GameObject SpeakerProfile;
    bool collided;

    private void Start()
    {
        manager = GameManager.Instance;
        Dialogue = FindObjectOfType<DialogueManager>();
        NpcM = FindObjectOfType<NPCManager>();
    }

    void ApplyNPC()
    {
        NpcData = NpcM.ToInit[NpcID - 1];
        NpcData.CurrentSpeaker = SpeakerProfile;
        NpcData.Construct(SpeakerProfile);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision)
        {
            Debug.Log("COLLIDED!");
            collided = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collided = false;
    }

    public void pollEvents()
    {
        Debug.Log("Current FLAG : " + manager.CurrrentFlag.Flag);
        Debug.Log("Current FLAG ID : " + manager.CurrrentFlag.ID);

        NPCEventData ToExecute;

        for (int i = 0; i < NpcData.EventData.Count; i++)
        {
            Debug.Log("FLAG : " + NpcData.EventData[i].RequiredFlag.Flag);
            Debug.Log("FLAG ID : " + NpcData.EventData[i].RequiredFlag.ID);

            if (NpcData.EventData[i].RequiredFlag.ID == manager.CurrrentFlag.ID)
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

        if (Input.GetButtonDown("Submit") && collided)
        {
            pollEvents();
        }
    }
}
