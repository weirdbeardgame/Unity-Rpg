using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponWidgetData : Widget
{
    WeaponData weapon;

    public void Init(WeaponData data)
    {
        weapon = data;
    }

    public override void Execute()
    {
        // Something like Equip in here. So, we could open the subscreen for whom to equip it to in here.
    }
}
