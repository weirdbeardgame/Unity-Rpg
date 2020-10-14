using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace questing
{
    public class questBook : MonoBehaviour
    {
        public List<QuestData> Quests;

        QuestData ActiveQuest;

        // Start is called before the first frame update
        void Start()
        {
            Quests = new List<QuestData>();
        }

        public void Give(QuestData cQuest)
        {
            Quests.Add(cQuest);
        }

        public void Activate(int QuestD)
        {
            questMessage active = new questMessage();
            ActiveQuest = Quests[QuestD];
            ActiveQuest.QuestState = QuestState.IS_ACTIVE;
            active.construct(QuestD, QuestState.IS_ACTIVE);
            Debug.Log("Active Quest: " + Quests[QuestD].QuestName);

        }

        public void DeActivate(int ID)
        {
            questMessage Active = new questMessage();
            ActiveQuest.QuestState = QuestState.NOT_ACTIVE;
            Active.construct(ID, QuestState.NOT_ACTIVE);
        }

    }
}
