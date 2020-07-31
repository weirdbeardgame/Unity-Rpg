using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace questing
{
    public class questBook : MonoBehaviour, questInterface
    {
        public List<QuestData> Quests;

        QuestData _ActiveQuest;

        public QuestData ActiveQuest
        {
            get
            {
                return _ActiveQuest;
            }


        }

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

            Quests[QuestD].Objectives[Quests[QuestD].ActiveObjective].State = QuestObjectiveState.ACTIVE;
            Quests[QuestD].QuestState = QuestState.IS_ACTIVE;
            active.construct(QuestD, QuestState.IS_ACTIVE);
            _ActiveQuest = Quests[QuestD];
            Debug.Log("Active Quest: " + Quests[QuestD].QuestName);

        }

        public void DeActivate(int ID)
        {
            questMessage Active = new questMessage();
            Quests[ID].QuestState = QuestState.NOT_ACTIVE;
            Active.construct(ID, QuestState.NOT_ACTIVE);
            _ActiveQuest = null;
        }

        public void Progress(int ID)
        {
            DeActivate(ID);
        }

        public QuestData GetQuestData(int ID)
        {
            return Quests[ID];
        }


        public string Description(int ID)
        {
            return Quests[ID].Description;
        }

        public void Complete(int ID)
        {
            //Quests[ID].Complete();
        }
    }
}