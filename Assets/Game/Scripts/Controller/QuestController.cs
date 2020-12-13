using Game.Scripts.Quest;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Scripts.Controller
{
    public class QuestController : MonoBehaviour
    {
        [CanBeNull] public QuestInfo ActiveQuest { get; set; }
    }
}