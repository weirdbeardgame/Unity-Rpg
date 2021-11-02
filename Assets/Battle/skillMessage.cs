﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMessage : ScriptableObject
{
    Messaging Messenger;
    Skills Skills;
    ActionIface Skill;
    MessageType BattleM;
    Creature Sender;

    object mess;

    // Start is called before the first frame update
    void Start()
    {
        Messenger = FindObjectOfType<Messaging>();
    }

    public void construct(Creature Caster, MessageType type, CommandType Type, int sID = -1)
    {
        Skills = FindObjectOfType<Skills>();

        BattleM = type;
        Sender = Caster;

        if (sID >= 0) // Remember attack is 0 
        {
            Skill = Skills.GetSkill(sID);
        }

        send();
    }

    public Creature getSender()
    {
        return Sender;
    }

    public MessageType GetMessageType()
    {
        return BattleM;
    }

    public void send()
    {
        Debug.Log("Sent Message");
        //Messenger.Send();
    }

    public ActionIface Data
    {
        get
        {
            return Skill;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

}
