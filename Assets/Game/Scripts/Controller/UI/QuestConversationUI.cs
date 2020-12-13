using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Game.Scripts.Controller.UI
{
    [RequireComponent(typeof(PlayerInput))]
    public class QuestConversationUI : MonoBehaviour
    {
        public static QuestConversationUI Instance;

        [SerializeField] private GameObject root;
        [SerializeField] private TextMeshProUGUI questName;
        [SerializeField] private TextMeshProUGUI questDescription;
        [SerializeField] private Image questImage;
        [SerializeField] private Button cancelBtn;
        [SerializeField] private Button acceptBtn;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(this);
        }

        private void Start()
        {
        }

        private void OnQuestAccept()
        {
        }

        private void OnQuestCancel()
        {
        }
    }
}