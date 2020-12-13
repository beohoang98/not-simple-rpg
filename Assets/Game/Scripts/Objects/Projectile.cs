using System;
using Game.Scripts.Constants;
using UnityEngine;

namespace Game.Scripts.Objects
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rigid2D;
        [SerializeField] private float speed = 10f;
        [SerializeField] private float lifetime = 1f;

        public float Damage { get; set; }

        private void Start()
        {
            Destroy(gameObject, lifetime);
        }

        public void SetDirection(Vector2 dir)
        {
            rigid2D.velocity = dir.normalized * speed;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.CompareTag(GameTags.Wall)) Destroy(gameObject);
        }
    }
}