using System;
using System.Collections;
using System.Collections.Generic;
using Characters.General;
using UnityEngine;
using UnityEngine.Serialization;
using GamePlay.Projectiles;

namespace GamePlay.Enemies
{
    public class AIAttacker : MonoBehaviour
    {
        [SerializeField] private float cooldown = 3f;
        
        private Projectile projectilePrefab;
        private Transform managerPosition;
        private DateTime lastAttackTime = DateTime.UtcNow;
        private Boss projectileShooter;

        public void Init(Projectile projectileType, Transform position, Boss shooter)
        {
            projectilePrefab = projectileType;
            managerPosition = position;
            projectileShooter = shooter;
        }
        
        public void TryShoot(HealthController target)
        {
            var timePassed = DateTime.UtcNow - lastAttackTime;
            var isTimePassed = timePassed >= TimeSpan.FromSeconds(cooldown);
            if (isTimePassed && !projectileShooter.IsPlayerInShootingRange()) Shoot(target);
        }

        private void Shoot(HealthController target)
        {
            lastAttackTime = DateTime.UtcNow;
            var projectile = Instantiate(projectilePrefab, managerPosition.position, Quaternion.identity);
            projectile.Init(target, projectileShooter.GetHealthControl());
        }
    }
}
