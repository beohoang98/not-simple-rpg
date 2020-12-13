using Game.Scripts.Player;
using Game.Scripts.Quest;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Scripts.Objects
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CircleCollider2D))]
    public class Gun : QuestTarget.Collectable
    {
        [SerializeField] public Sprite icon;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private CircleCollider2D mCollider2D;
        [SerializeField] private float damage = 5f;
        [SerializeField] [CanBeNull] private AudioSource fireSoundSource;
        private PlayerWeapon _owner = null;
        private bool _isActive = false;

        public bool IsOwned => _owner != null;
        public bool IsActive => _isActive;

        private void Update()
        {
            // if (_isActive)
            // {
            //     transform.position = _owner.transform.position;
            // }
        }

        public void EquipTo(PlayerWeapon playerWeapon)
        {
            _owner = playerWeapon;
            mCollider2D.enabled = false;
            transform.parent = playerWeapon.gameObject.transform;
            transform.localPosition = Vector3.zero;
            StopUsing();
        }

        public void Drop()
        {
            _owner = null;
            mCollider2D.enabled = true;
            transform.parent = null;
            gameObject.SetActive(true);
        }

        public void Using()
        {
            _isActive = true;
            gameObject.SetActive(true);
        }

        public void StopUsing()
        {
            _isActive = false;
            gameObject.SetActive(false);
            fireSoundSource?.Stop();
        }

        public void Shoot(Vector2 direction)
        {
            GameObject bulletObject = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Projectile bulletProjectile = bulletObject.GetComponent<Projectile>();
            if (bulletProjectile == null) return;
            bulletProjectile.Damage = damage;
            bulletProjectile.SetDirection(direction);
            fireSoundSource?.Play();
        }
    }
}