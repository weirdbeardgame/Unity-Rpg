using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SamuraiMenu : BattleMenu
{
    Skills skills;
    commandMenus Menu;

    private void Start()
    {
        skills = FindObjectOfType<Skills>();
        Menu = FindObjectOfType<commandMenus>();
    }

    public void Open()
    {
    }

    public void Close()
    {
        
    }

    void SwordArts()
    {
        
    }

    void Skills()
    {

    }
}
