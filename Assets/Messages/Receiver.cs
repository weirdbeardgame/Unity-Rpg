using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReceiver
{
    void Subscribe();
    void Receive(object message);
    void Unsubscribe();
}