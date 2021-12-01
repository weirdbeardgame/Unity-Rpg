using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Attack()
    {
        Debug.Log("Skill Added");
        //StartCoroutine(Menu.Target(skills.GetSkill(0)));
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
