using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class WarriorMenu : MonoBehaviour, BattleMIface
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
        Add();
    }

    public void Open(Creature Opener)
    {

        Caster = Opener;

        // Below is the widgets for Warrior
        Instantiate(Widget);
        //Widget.GetComponent<Widget>().Initalize(0, "Attack");
        Widget.GetComponentInChildren<TextMeshPro>().text = "Attack";
        Menu.AddWidget(Widget);

        Instantiate(Widget);
        //Widget.GetComponent<Widget>().Initalize(1, "Item");
        Widget.GetComponentInChildren<TextMeshPro>().text = "Item";
        Menu.AddWidget(Widget);

        Instantiate(Widget);
        //Widget.GetComponent<Widget>().Initalize(2, "Skills");
        Widget.GetComponentInChildren<TextMeshPro>().text = "Skills";
        Menu.AddWidget(Widget);

        Debug.Log("Warrior Menu Open");


        Menu.IsOpened = true;
    }

    public void Open()
    {

    }

    public void Close()
    {
        Menu.Close();
    }

    void Attack()
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

    void SwordArts()
    {

    }

    public void Add()
    {
        Menu = FindObjectOfType<commandMenus>();
        Menu.AddMenu(JobSystem.WARRIOR, this);
    }

}