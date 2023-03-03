using UnityEngine;

namespace Project.Scripts.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private int maxHealth;
        [SerializeField] private PlayerHealthUI _ui;
        private PlayerActions _actions;
        private int _currentHealth;

        private void Start()
        {
            _currentHealth = maxHealth;
            _actions = GetComponent<PlayerActions>();
        }

        public void LoseHealth()
        {
            _currentHealth--;
            _ui.LoseOneHealth(_currentHealth);
            _actions.OnHurt?.Invoke();
            if (_currentHealth < 1)
            {
                Die();
            }
        }

        private void Die()
        {
            Debug.Log("You DEAD");
        }
    }
}
