using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Events { DIALOGUE, CUTSCENE, FOLLOW, ADDITEM };

public interface EvInterface
{
    void Initalize();
    void Execute();
}