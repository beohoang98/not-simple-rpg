using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Scripts.Controller
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Game/Controller/Menu")]
    [RequireComponent(typeof(Animator))]
    public class MenuController : MonoBehaviour
    {
        private static readonly int VarShow = Animator.StringToHash("show");

        [SerializeField] private Button returnButton;
        [SerializeField] private Button retryButton;
        [SerializeField] private Button quitButton;
        [SerializeField] private Animator animator;
        [SerializeField] private InputActionAsset inputActionAsset;

        private InputActionMap _gameActionMap;
        private bool _show = false;

        private void Start()
        {
            animator = GetComponent<Animator>();
            _gameActionMap = inputActionAsset.FindActionMap("Game");

            returnButton.onClick.AddListener(OnReturnClicked);
            retryButton.onClick.AddListener(OnRetryClicked);
            quitButton.onClick.AddListener(OnQuitClicked);

            InputAction escAction = inputActionAsset
                .FindActionMap("UI")
                .FindAction("Cancel");
            escAction.started += OnEscapePressed;
            escAction.Enable();
        }

        public void Open(bool show = true)
        {
            _show = show;
            animator.SetBool(VarShow, show);
            if (show)
            {
                _gameActionMap.Disable();
            }
            else
            {
                _gameActionMap.Enable();
            }
        }

        private void OnEscapePressed(InputAction.CallbackContext context)
        {
            Open(!_show);
        }

        private void OnReturnClicked()
        {
            Open(false);
        }

        private void OnRetryClicked()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().path);
        }

        private void OnQuitClicked()
        {
            Application.Quit();
        }
    }
}