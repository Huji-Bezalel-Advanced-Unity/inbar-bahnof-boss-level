using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters.General
{
    public class HealthController : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 100;

        private HealthUI healthUI;
        private float curHealth;
        private Action onDeathAction;

        public void Init(Action onDeath = null)
        {
            healthUI = FindObjectOfType<HealthUI>();

            onDeathAction = onDeath;
            curHealth = maxHealth;
            healthUI.Init(maxHealth);
        }

        public void TakeDamage(float damage)
        {
            curHealth -= damage;
            healthUI.SetHealth(curHealth);

            if (curHealth <= 0)
            {
                Die();
            }
        }

        public void Restart()
        {
            curHealth = maxHealth;
            healthUI.SetHealth(maxHealth);
        }

        private void Die()
        {
            onDeathAction?.Invoke();
        }
    }
}