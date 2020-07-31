using System.Collections;
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

    public int SkillID;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void construct(Creature Caster, MessageType type, CommandType Type, int sID = -1)
    {
        Skills = FindObjectOfType<Skills>();

        SkillID = sID;
        BattleM = type;
        Sender = Caster;

        if (SkillID >= 0) // Remember attack is 0 
        {
            Skill = Skills.GetSkill(SkillID);
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
        Messenger = FindObjectOfType<Messaging>();
        Debug.Log("Sent Message");
        Messenger.Send(this, MessageType.BATTLE);
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
