using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BattleMIface
{
    void Add();
    void Open();
    void Close();
    void Open(Creature opener);

}
