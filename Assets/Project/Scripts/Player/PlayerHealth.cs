using UnityEngine;

namespace Project.Scripts.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private int maxHealth;
        [SerializeField] private PlayerHealthUI ui;
        private PlayerActions _actions;
        private int _currentHealth;

        public void StartHealth()
        {
            ui.gameObject.SetActive(true);
            _currentHealth = maxHealth;
            _actions = GetComponent<PlayerActions>();
        }

        public void LoseHealth()
        {
            _currentHealth--;
            ui.LoseOneHealth(_currentHealth);
            _actions.OnHurt?.Invoke();
            if (_currentHealth < 1)
            {
                Die();
                ui.EndGameScreen();
            }
        }

        private void Die()
        {
            _actions.OnDead?.Invoke();
        }
    }
}
