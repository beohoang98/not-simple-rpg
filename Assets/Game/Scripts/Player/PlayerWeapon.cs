using System.Collections.Generic;
using Game.Scripts.Constants;
using Game.Scripts.Controller.UI;
using Game.Scripts.Objects;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Scripts.Player
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerWeapon : MonoBehaviour
    {
        [SerializeField] private float emitPerSec = 5;
        private float _emitTimeCount = 0f;
        private bool _canShoot = true;

        public readonly List<Gun> Guns = new List<Gun>();
        private Gun _inHoverGun = null;
        private Vector2 _mousePos = Vector2.zero;
        private int _activeGunIndex = -1;

        public Gun ActiveGun { get; private set; } = null;

        private void Update()
        {
            _emitTimeCount += Time.deltaTime;
            if (_emitTimeCount > 1f / emitPerSec)
            {
                _emitTimeCount = 0;
                _canShoot = true;
            }

            _mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag(GameTags.Gun))
            {
                Gun gun = other.gameObject.GetComponent<Gun>();
                _inHoverGun = gun;
                InteractUI.Instance.SetText("Gun");
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(GameTags.Gun))
            {
                _inHoverGun = null;
                InteractUI.Instance.SetText();
            }
        }

        [UsedImplicitly]
        public void OnInteractWeapon(InputAction.CallbackContext context)
        {
            if (_inHoverGun == null) return;

            Guns.Add(_inHoverGun);
            _inHoverGun.EquipTo(this);
            _inHoverGun = null;
            InventoryUI.Instance.ReRender();

            if (Guns.Count == 1)
            {
                _activeGunIndex = 0;
                ActiveGun = Guns[0];
                ActiveGun.Using();
            }
        }

        [UsedImplicitly]
        public void OnSwitchWeapon(InputAction.CallbackContext context)
        {
            var len = Guns.Count;
            if (len <= 0) return;
            _activeGunIndex = (_activeGunIndex + 1) % len;
            ActiveGun?.StopUsing();
            ActiveGun = Guns[_activeGunIndex];
            ActiveGun.Using();
        }

        [UsedImplicitly]
        public void OnFired(InputAction.CallbackContext context)
        {
            if (!_canShoot) return;
            Vector2 direction = _mousePos - (Vector2) transform.position;
            ActiveGun?.Shoot(direction);
            _canShoot = false;
        }
    }
}