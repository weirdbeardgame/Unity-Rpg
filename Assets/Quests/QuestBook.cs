using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Questing
{
    public class QuestBook : MonoBehaviour
    {
        public List<QuestData> obtainedQuests;
        QuestManager manager;
        public delegate QuestData activeQuest(QuestData quest);

        // Start is called before the first frame update
        void Start()
        {
            obtainedQuests = new List<QuestData>();
            // Assuming this will be seperate from the menu and attached to the scripts object
            manager = GetComponent<QuestManager>();
        }
        public void Give(QuestData cQuest)
        {
            if (!obtainedQuests.Contains(cQuest) && cQuest != null)
            {
                obtainedQuests.Add(cQuest);
            }
        }
        public void Activate(int QuestD)
        {
            Debug.Log("Active Quest: " + obtainedQuests[QuestD].questName);
        }

        /*****************************************************************
        * I'm not dropping the messaging system design. 
        * We do need a posted events for other systems like Npc or battle
        * I just think using C# events would be better
        ****************************************************************/
        public void Activate(QuestData questD)
        {
            //activeQuest q = new activeQuest(questD);
        }

        public void DeActivate(QuestData ID)
        {
            /*questMessage Active = new questMessage();
            activeQuest.questState = QuestState.NOT_ACTIVE;
            Active.construct(ID.QuestID, QuestState.NOT_ACTIVE);*/
        }
    }
}
