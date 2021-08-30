using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using System.IO;

namespace questing
{
    // Manager holds ALL quests
    public class QuestManager : MonoBehaviour
    {
        public List<QuestData> quests;
        QuestData QuestD;
        Messaging Messenger;
        string FilePath = "Assets/Quests/Quest.json";
        string data;
        int activeID;
        QuestData activeQuest;

        // Start is called before the first frame update
        void Start()
        {
            Initialize();
        }

        void Initialize()
        {
            Messenger = FindObjectOfType<Messaging>();
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

        public void Progress() // progresses through the list of objectives
        {

        }

        public void Complete()
        {

        }

        void Update()
        {

        }
    }
}
