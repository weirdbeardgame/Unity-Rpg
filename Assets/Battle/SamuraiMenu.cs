using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SamuraiMenu : MonoBehaviour, BattleMIface
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

        // Below is the widgets for Samurai
        Widget = Instantiate(Widget);
        //Widget.GetComponent<Widget>().Initalize(Attack, "Attack");
        Widget.GetComponentInChildren<TextMeshPro>().text = "Attack";
        Menu.AddWidget(Widget);

        Widget = Instantiate(Widget);
        //Widget.GetComponent<Widget>().Initalize(Item, "Item");
        Widget.GetComponentInChildren<TextMeshPro>().text = "Item";
        Menu.AddWidget(Widget);

        Widget = Instantiate(Widget);
        //Widget.GetComponent<Widget>().Initalize(Skills, "Skills");
        Widget.GetComponentInChildren<TextMeshPro>().text = "Skills";
        Menu.AddWidget(Widget);

        Debug.Log("Samurai Menu Open");

        Menu.IsOpened = true;

        return;

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

    public void Add()
    {
        Menu = FindObjectOfType<commandMenus>();
        Menu.AddMenu(JobSystem.SAMURAI, this);
    }
}
