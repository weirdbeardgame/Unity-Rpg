using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum QuestEventType {DIALOGUE }

public class QuestEventData : ScriptableObject
{
    QuestEventType EventTypes;

    BinarySearchTree<DialogueMessage> Tree;

}
