using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class WarriorMenu : BattleMIface
{
    public GameObject Widget;
    Skills skills;
    commandMenus Menu;
    Creature Caster;
    SkillMessage message;
    TextMeshPro DamageDisplay;
    Inputs Input;

    private void Start()
    {
        skills = FindObjectOfType<Skills>();
        Menu = FindObjectOfType<commandMenus>();
    }

    public void Open()
    {

    }

    public override void Close()
    {
        Menu.Close();
    }

/*
    void Attack()
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