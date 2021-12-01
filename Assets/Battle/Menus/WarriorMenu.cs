using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class WarriorMenu : BattleMenu
{
    Skills skills;
    commandMenus Menu;
    TextMeshPro DamageDisplay;
    Inputs Input;

    private void Start()
    {
        skills = FindObjectOfType<Skills>();
        Menu = FindObjectOfType<commandMenus>();
    }

  /*void Attack()
    {
        Debug.Log("Skill Added");
        StopAllCoroutines();
    }

    void Item()
    {
        Menu.Open(0);
    }

    void Skills()
    {

    }

    void SwordArts()
    {

    }

    public void Add()
    {
        Menu = FindObjectOfType<commandMenus>();
        Menu.AddMenu(JobSystem.WARRIOR, this);
    }
*/
}