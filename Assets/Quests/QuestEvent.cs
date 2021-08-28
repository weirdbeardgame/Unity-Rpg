using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestEventType { DIALOGUE, CUTSCENE }

public class QuestEvent : MonoBehaviour
{
    StateMachine state;
    QuestEventType events;
    DialogueManager dialogue;
    BinarySearchTree<DialogueMessage> dialogueTree;
    Flags flagToSet;
    private void Start()
    {
    }

    public void Execute()
    {
        switch(events)
        {
            case QuestEventType.DIALOGUE:
                state = GetComponent<StateMachine>();
                dialogue = GetComponent<DialogueManager>();
                state.State = States.DIALOGUE;
                dialogue.OpenDialogueBox(dialogueTree);
                break;
            case QuestEventType.CUTSCENE:
                state = GetComponent<StateMachine>();
                state.State = States.CUTSCENE;
                break;
        }
    }

    private void Update()
    {

    }
}
