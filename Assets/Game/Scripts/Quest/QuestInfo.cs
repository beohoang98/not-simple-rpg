using UnityEngine;

namespace Game.Scripts.Quest
{
    [CreateAssetMenu(fileName = "Quest_01", menuName = "Game/Quest", order = 0)]
    public class QuestInfo : ScriptableObject
    {
        public string questName;
        public string description;
        public QuestType type;
        public string[] scanObjectTags;
        public int requiredAmount = 0;
        private int _currentAmount = 0;

        public int CurrentAmount
        {
            get => _currentAmount;
            set
            {
                _currentAmount = value;
                if (_currentAmount >= value) CompleteEvent?.Invoke(this);
            }
        }

        public delegate void OnCompleteEvent(QuestInfo questInfo);

        public OnCompleteEvent CompleteEvent { get; set; }
    }

    public enum QuestType
    {
        Kill = 0,
        Collect,
        Learning
    }
}