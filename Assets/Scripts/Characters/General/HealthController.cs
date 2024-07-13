using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters.General
{
    public class HealthController : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private ParticleSystem _damageParticals;

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

        public void TakeDamage(float damage, Vector3 hitDirection)
        {
            curHealth -= damage;
            healthUI.SetHealth(curHealth);

            if (curHealth <= 0)
            {
                Die();
            }
            SpawnParticals(hitDirection);
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
        
        private void SpawnParticals(Vector3 attackDirection)
        {
            Quaternion direction = Quaternion.FromToRotation(Vector3.right, attackDirection);
            Instantiate(_damageParticals, transform.position, direction);
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