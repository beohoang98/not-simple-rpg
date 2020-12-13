using Game.Scripts.Constants;
using Game.Scripts.Objects;
using Game.Scripts.Quest;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Enemy
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class EnemyBase : QuestTarget.Killable
    {
        [SerializeField] protected string enemyName = "Base";
        [SerializeField] protected float maxHealth;
        [SerializeField] protected Sprite staticSprite;
        [SerializeField] protected Animator animator;
        [SerializeField] protected Rigidbody2D rigid2D;
        [SerializeField] protected Collider2D[] mCollider2D;
        [SerializeField] protected Slider healthSlider;

        protected float Health;

        protected virtual void Start()
        {
            Health = maxHealth;
            healthSlider.maxValue = maxHealth;
            healthSlider.value = maxHealth;
        }

        protected virtual void OnDead()
        {
            Destroy(gameObject);
        }

        protected virtual void AfterTakeDamage(Projectile projectile)
        {
        }

        protected void OnTakeDamage(Projectile projectile)
        {
            Health -= projectile.Damage;
            healthSlider.value = Health;
            AfterTakeDamage(projectile);
            if (Health <= 0)
            {
                foreach (Collider2D col in mCollider2D) col.enabled = false;
                OnDead();
            }
        }

        private void OnCollisionEnter2D(Collision2D collision2D)
        {
            if (collision2D.collider.CompareTag(GameTags.Projectile))
            {
                Projectile projectile = collision2D.gameObject.GetComponent<Projectile>();
                if (projectile == null) return;
                OnTakeDamage(projectile);
                Destroy(projectile.gameObject);
            }
        }
    }
}