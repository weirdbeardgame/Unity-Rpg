using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionMessage : ScriptableObject
{
    public int CollidedID;
    Messaging Messenger;

    public void construct(int ID)
    {
        Messenger = FindObjectOfType<Messaging>();
        CollidedID = ID;
        Messenger.Send(this, MessageType.COLLISION);
    }

}
