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

        private float _lastParticalsSpawn = 0f;

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
            
            Quaternion direction = Quaternion.FromToRotation(Vector3.right, hitDirection);
            SpawnParticals(direction);
        }

        public void TakeDamage(float damage)
        {
            curHealth -= damage;
            healthUI.SetHealth(curHealth);

            if (curHealth <= 0)
            {
                Die();
            }
            SpawnParticals(Quaternion.identity);
        }
        
        private void SpawnParticals(Quaternion attackDirection)
        {
            if (Time.time -  _lastParticalsSpawn > 0.25f)
            {
                Instantiate(_damageParticals, transform.position, attackDirection);
                _lastParticalsSpawn = Time.time;
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