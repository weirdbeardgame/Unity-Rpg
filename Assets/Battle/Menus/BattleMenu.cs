using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleMenu : MonoBehaviour
{
    protected Skills skills;
    delegate void s(SkillData s);
    event s skillMessage;

    // Start is called before the first frame update
    void Start()
    {
        skills = FindObjectOfType<Skills>();
    }

    public virtual void Attack()
    {
        Debug.Log("Skill Added");
        skillMessage.Invoke(skills.GetSkill(0));
    }

    public void Item()
    {
        //Menu.Open(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
