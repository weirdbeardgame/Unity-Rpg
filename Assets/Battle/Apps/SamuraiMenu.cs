using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SamuraiMenu : BattleMIface
{
    Skills skills;
    commandMenus Menu;
    SkillMessage message;

    private void Start()
    {
        skills = FindObjectOfType<Skills>();
        Menu = FindObjectOfType<commandMenus>();
    }

    public override void Open()
    {
        // Would involve looking for widgets?
        appID = ((int)job);
        appName = "SamuraiMenu";
    }

    public override void Close()
    {
        
    }

    /*void Attack()
    {
        Debug.Log("Skill Added");

        StopAllCoroutines();
        Menu.ResetInput();

        StartCoroutine(Menu.Target(skills.GetSkill(0)));
    }

    void Item()
    {
        Menu.Open(0);
    }

    void Skills()
    {

    }

    public void Add()
    {
        Menu = FindObjectOfType<commandMenus>();
        Menu.AddMenu(JobSystem.SAMURAI, this);
    }*/
}
