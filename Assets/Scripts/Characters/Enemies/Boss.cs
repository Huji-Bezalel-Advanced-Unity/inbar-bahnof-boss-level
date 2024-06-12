using System;
using UnityEngine;

namespace Characters.Enemies
{
    public class Boss : BasicEnemy
    {
        [SerializeField] private AIAttacker attacker;
        [SerializeField] private PoisonProjectile projectilePrefab;

        private HealthController healthController;

        private void Awake()
        {
            healthController = GetComponent<HealthController>();
            if (healthController != null)
            {
                healthController.Init(OnDeath);
            }

            if (projectilePrefab != null && attacker != null)   
            {
                attacker.Init(projectilePrefab, transform, healthController);
            }
        }
        
        public HealthController GetHealthControl()
        {
            return healthController;
        }

        public override void TryShoot()
        {
            attacker.TryShoot(player.GetHealthControl());
            
            base.TryShoot();
        }

        public override void TryMove()
        {
            if (IsPlayerInRange()) return;

            Vector3 playerPos = player.transform.position;
            Vector3 direction = (playerPos - transform.position).normalized;
            transform.Translate(direction * (speed * Time.deltaTime));

            base.TryMove();
        }

        public override void OnDeath()
        {
            base.OnDeath();
        }

        public override void OnHit()
        {
            base.OnHit();
        }
    }
}