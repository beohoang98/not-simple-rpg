using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Controller.UI
{
    [DisallowMultipleComponent]
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private Slider healthSlider;
        [SerializeField] private TextMeshProUGUI currentHealthLabel;
        [SerializeField] private TextMeshProUGUI maxHealthLabel;

        public void OnHealthChange(int health, int maxHealth)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = health;
            currentHealthLabel.SetText(health.ToString());
            maxHealthLabel.SetText($"/{maxHealth.ToString()}");
        }
    }
}