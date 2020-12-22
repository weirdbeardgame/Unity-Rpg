using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MessageType { BATTLE, QUEST, INPUT, GAME_STATE, INVENTORY, COLLISION, SWITCH }

public class MessageData<T> // To Define Message Data. Not reciever
{
    public int ID;
    public MessageType MessageType;
    public T Data;
    public void Construct(int I, MessageType m, T dat)
    {
        ID = I;
        MessageType = m;
        Data = dat;
    }
}