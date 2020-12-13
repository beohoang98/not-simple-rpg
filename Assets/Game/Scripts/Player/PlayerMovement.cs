using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Scripts.Player
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class PlayerMovement : MonoBehaviour
    {
        private static readonly int VarX = Animator.StringToHash("x");
        private static readonly int VarY = Animator.StringToHash("y");
        private static readonly int VarMoving = Animator.StringToHash("moving");

        [SerializeField] private Animator animator;
        [SerializeField] private Rigidbody2D rigid2D;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private InputActionAsset inputActionAsset;
        [SerializeField] private float speed = 10f;

        private Vector2 _direction = Vector2.zero;
        private bool _isFreeMove = false;

        private void Start()
        {
            animator = GetComponent<Animator>();
            rigid2D = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();

            // _moveAction = inputActionAsset
            //     .FindActionMap(GameInputs.GameMap, true)
            //     .FindAction(GameInputs.Game.Move, true);
            // _moveAction.Enable();
            // _moveAction.started += OnMoving;
            // _moveAction.performed += OnMoving;
            // _moveAction.canceled += OnMoving;
        }

        private void Update()
        {
            if (!_isFreeMove) rigid2D.velocity = _direction * speed;
            animator.SetFloat(VarX, _direction.x);
            animator.SetFloat(VarY, _direction.y);
            spriteRenderer.flipX = rigid2D.velocity.x > 0;
        }

        [UsedImplicitly]
        public void OnMoving(InputAction.CallbackContext context)
        {
            Vector2 direction = context.ReadValue<Vector2>();
            if (context.canceled)
            {
                _direction = Vector2.zero;
                animator.SetBool(VarMoving, false);
                return;
            }

            animator.SetBool(VarMoving, true);
            _direction = direction.normalized;
        }

        public IEnumerator DisableMovementFor(float sec)
        {
            Debug.Log("Disable moving");
            _isFreeMove = true;
            yield return new WaitForSecondsRealtime(sec);
            _isFreeMove = false;
            Debug.Log("Enable moving");
            yield return null;
        }
    }
}