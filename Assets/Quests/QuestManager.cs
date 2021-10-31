using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using System.IO;

namespace Questing
{
    // Manager holds ALL quests
    public class QuestManager : MonoBehaviour
    {
        public List<QuestData> quests;
        QuestData QuestD;
        string FilePath = "Assets/Quests/Quest.json";
        string data;
        int activeID;
        QuestData activeQuest;

        public void Init()
        {
            if (File.Exists(FilePath))
            {
                quests = new List<QuestData>();
                data = File.ReadAllText(FilePath);
                quests = JsonConvert.DeserializeObject<List<QuestData>>(data);
            }
        }

        public QuestData Get(int id)
        {
            return quests[id];
        }

        public QuestData getActiveQuest
        {
            get
            {
                return activeQuest;
            }
        }

        // Progresses through the list of objectives
        // Stepping throught the data structure whether that be tree, list, or graph depending on linearity
        public void Progress(Asset asset)
        {
            // Step through to next node if condition is met
            if (activeQuest.Eval(asset))
            {
                Complete();
            }
        }

        public void Complete()
        {
            // Add XP and reward from here
        }

        void Update()
        {

        }
    }
}
