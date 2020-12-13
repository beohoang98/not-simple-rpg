using System.Collections.Generic;
using Game.Scripts.Player;
using UnityEngine;

namespace Game.Scripts.Controller.UI
{
    [DisallowMultipleComponent]
    public class InventoryUI : MonoBehaviour
    {
        public static InventoryUI Instance;

        [SerializeField] private PlayerWeapon playerWeapon;
        [SerializeField] private List<SpriteRenderer> weaponSlots;
        [SerializeField] private List<SpriteRenderer> itemSlots;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(this);
        }

        public void ReRender()
        {
            for (int i = 0, len = weaponSlots.Count, gunLen = playerWeapon.Guns.Count; i < len; ++i)
            {
                Vector2 size = weaponSlots[i].size;
                weaponSlots[i].sprite = i < gunLen ? playerWeapon.Guns[i].icon : null;
                weaponSlots[i].size = size;
            }
        }
    }
}