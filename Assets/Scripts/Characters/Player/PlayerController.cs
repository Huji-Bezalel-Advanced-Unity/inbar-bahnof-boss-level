using System;
using UnityEngine;

namespace Characters.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float cooldown = 0.5f;
        [SerializeField] private HealthController healthController;
        [SerializeField] private FlowerProjectile flowerPrefab;
        [SerializeField] private float speed = 5f;
        
        private HealthController bossHealth;
        private DateTime lastAttackTime = DateTime.UtcNow;
        private float energyLevel = 1;
        
        private void Awake()
        {
            healthController.Init(OnDeath);
        }

        private void OnDeath()
        {
            
        }

        private void Update()
        {
            TryMove();
            TryAttack();
            UpdateEnery();
        }

        private void UpdateEnery()
        {
            if (energyLevel > 0.05f)
            {
                energyLevel -= 0.0001f;
            }
        }

        private void TryAttack()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                var timePassed = DateTime.UtcNow - lastAttackTime;
                var isTimePassed = timePassed >= TimeSpan.FromSeconds(cooldown);
                
                if (isTimePassed) Shoot();
            }
        }

        private void Shoot()
        {
            var projectile = Instantiate(flowerPrefab, transform.position, Quaternion.identity);
            projectile.Init(bossHealth, healthController);
            
            lastAttackTime = DateTime.UtcNow;
        }

        private void TryMove()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            Vector2 movement = new Vector2(horizontal, vertical).normalized;
            transform.Translate(movement * (energyLevel * speed * Time.deltaTime));
        }

        public HealthController GetHealthControl()
        {
            return healthController;
        }

        public void setBossTarget(HealthController boss)
        {
            bossHealth = boss;
        }

        public void AddEnergy()
        {
            if ((energyLevel + 0.3f) < 1)
            {
                energyLevel += 0.3f;
            }
            else
            {
                energyLevel = 1;
            }
        }
    }
}