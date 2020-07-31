using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface senderInterface
{
    Creature getCreature();
    void send(object message);

}