using UnityEngine;

namespace Game.Scripts.Quest
{
    public abstract class QuestTarget : MonoBehaviour
    {
        public abstract class Collectable : QuestTarget
        {
            public delegate void OnCollectedEvent(Collectable collectable);

            public OnCollectedEvent OnCollected;
        }

        public abstract class Killable : QuestTarget
        {
            public delegate void OnKilledEvent(Killable mThis);

            public OnKilledEvent OnKilled;
        }
    }
}