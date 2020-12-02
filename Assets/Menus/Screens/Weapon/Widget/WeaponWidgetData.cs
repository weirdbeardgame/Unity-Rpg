using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponWidgetData : Widget
{
    WeaponData Weapon;

    public WeaponWidgetData()
    {
        Weapon = null;
    }

    public WeaponWidgetData(WeaponData Data)
    {
        Weapon = Data;
    }

    public override void Execute()
    {
        // Something like Equip in here. So, we could open the subscreen for whom to equip it to in here.
    }
}
