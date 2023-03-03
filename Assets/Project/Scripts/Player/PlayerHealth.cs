using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    private int _currentHealth;

    private void Start()
    {
        _currentHealth = maxHealth;
    }

    public void LoseHealth()
    {
        _currentHealth--;
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
