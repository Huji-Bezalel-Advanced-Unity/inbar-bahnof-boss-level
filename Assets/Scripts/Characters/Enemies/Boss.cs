using System;
using Characters.General;
using UnityEngine;

namespace Characters.Enemies
{
    public class Boss : BasicEnemy
    {
        [SerializeField] private AIAttacker attacker;
        [SerializeField] private PoisonProjectile projectilePrefab;

        private HealthController _healthController;

        private void Awake()
        {
            _healthController = GetComponent<HealthController>();
            if (_healthController != null)
            {
                _healthController.Init(OnDeath);
            }

            if (projectilePrefab != null && attacker != null)   
            {
                attacker.Init(projectilePrefab, transform, _healthController);
            }
        }
        
        public void Restart()
        {
            _healthController.Restart();
        }
        
        public HealthController GetHealthControl()
        {
            return _healthController;
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