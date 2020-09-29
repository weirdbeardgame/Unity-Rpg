using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace questing
{
    public enum QuestObjectiveType { KILL, COLLECT }

    public enum QuestObjectiveState { ACTIVE, NON_ACTIVE, COMPLETED }

    public class ItemToCollect
    {
        public int RequiredAmount;
        public int ItemID;
        public ItemData Item;
    }

    public class QuestObjective : ScriptableObject, IReceiver
    {
        public string Name;
        public string Description;

        public Flags Flag;

        private QuestObjectiveType _Type;
        private QuestObjectiveState _State;

        Messaging messaging;

        public int ObjectiveID = 0;

        [System.NonSerialized]
        public int AmountCollected = 0;
        [System.NonSerialized]
        public int AmountToKill = 0;

        public List<ItemToCollect> RequiredItems;

        int MaxAmount = 0;

        Creature toKill;
        Enemies killData;

        public QuestObjectiveState State
        {
            get
            {
                return _State;
            }

            set
            {
                _State = value;
            }
        }

        public QuestObjectiveType Type
        {
            get
            {
                return _Type;
            }

            set
            {
                _Type = value;
            }
        }


        public void Receive(object message)
        {
            if (message is InventoryMessage)
            {
                InventoryMessage temp = (InventoryMessage)message;
                if (temp.getItem() == RequiredItems[temp.GetID()].Item)
                {
                    AmountCollected++;
                }
                Complete();
            }
        }

        public void Subscribe()
        {
            messaging.Subscribe(MessageType.INVENTORY, this);
        }

        public void Unsubscribe()
        {
            messaging.Unsubscribe(MessageType.INVENTORY, this);
        }
        Flags Complete()
        {
            if (_State == QuestObjectiveState.ACTIVE)
            {
                switch (_Type)
                {
                    case QuestObjectiveType.COLLECT:
                        if (RequiredItems[0].RequiredAmount == MaxAmount)
                        {
                            Debug.Log("Quest Completed");
                            _State = QuestObjectiveState.COMPLETED;
                            return Flag;
                        }
                        break;

                    case QuestObjectiveType.KILL:
                        if (AmountToKill == MaxAmount)
                        {
                            _State = QuestObjectiveState.COMPLETED;
                            return Flag;
                        }
                        break;
                }
            }

            return null;
        }
    }
}