using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;

    private int curHealth;
    private Action onDeathAction;

    public void Init(Action onDeath = null)
    {
        onDeathAction = onDeath;
        curHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        curHealth -= damage;

        if (curHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        onDeathAction?.Invoke();
    }
}