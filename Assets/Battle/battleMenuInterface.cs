using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMIface
{
    void Close();
    void Open(Creature opener)
    {
        // Grab job type and go from there to decide where this should call. Also, should this really be in here? Or in the Menu's it allows to inherit it.
    }

}
