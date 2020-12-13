using Game.Scripts.Constants;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Scripts.Controller.UI
{
    [DisallowMultipleComponent]
    public class InteractUI : MonoBehaviour
    {
        public static InteractUI Instance;

        [SerializeField] private TextMeshProUGUI keyLabel;
        [SerializeField] private TextMeshProUGUI descriptionLabel;
        [SerializeField] private InputActionAsset inputActionAsset;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(Instance);
        }

        private void Start()
        {
            InputAction interactAction = inputActionAsset.FindAction(GameInputs.Game.Interact);
            var keyName = interactAction.bindings[0].ToDisplayString();
            keyLabel.SetText(keyName);
            SetText();
        }

        public void SetText(string des = null)
        {
            var isShow = des != null;
            keyLabel.gameObject.SetActive(isShow);
            descriptionLabel.SetText(des);
            descriptionLabel.gameObject.SetActive(isShow);
        }
    }
}