using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum QuestEventType {DIALOGUE, CUTSCENE } // Should cutscenes be a seperate system?

public class QuestEventData : ScriptableObject
{
    QuestEventType EventTypes;

    BinarySearchTree<DialogueMessage> Tree;

}
