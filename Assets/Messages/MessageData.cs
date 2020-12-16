using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MessageType { BATTLE, QUEST, INPUT, GAME_STATE, INVENTORY, COLLISION, SWITCH }

public class MessageData<T> // To Define Message Data. Not reciever
{
    int ID;
    MessageType MessageType;
    T Data;
}