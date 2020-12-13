using System;
using Game.Scripts.Constants;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts.Player
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 100;
        private PlayerMovement _movement;
        private Rigidbody2D _rigidbody2D;
        private int _health;

        [Serializable]
        public class OnHealthChangeEvent : UnityEvent<int, int>
        {
        }

        public OnHealthChangeEvent onHealthChangeEvent;

        private void Start()
        {
            _health = maxHealth;
            onHealthChangeEvent.Invoke(_health, maxHealth);
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _movement = GetComponent<PlayerMovement>();
        }

        private void TakeDamage(float damage)
        {
            _health = Mathf.FloorToInt(_health - damage);
            onHealthChangeEvent.Invoke(_health, maxHealth);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.CompareTag(GameTags.Fox))
            {
                StartCoroutine(_movement.DisableMovementFor(2));
                TakeDamage(10);
            }
        }
    }
}