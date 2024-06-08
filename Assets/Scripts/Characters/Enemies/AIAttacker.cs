using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Characters.Enemies
{
    public class AIAttacker : MonoBehaviour
    {
        [SerializeField] private float cooldown = 3f;
        
        private Projectile projectilePrefab;
        private Transform managerPosition;
        private DateTime lastAttackTime = DateTime.UtcNow;
        

        public void Init(Projectile projectileType, Transform position)
        {
            projectilePrefab = projectileType;
            managerPosition = position;
        }
        
        public void TryShoot(HealthController target)
        {
            var timePassed = DateTime.UtcNow - lastAttackTime;
            var isTimePassed = timePassed >= TimeSpan.FromSeconds(cooldown);
            
            if (isTimePassed) Shoot(target);
        }

        private void Shoot(HealthController target)
        {
            lastAttackTime = DateTime.UtcNow;
            var projectile = Instantiate(projectilePrefab, managerPosition.position, Quaternion.identity);
            projectile.Init(target);
        }
    }
}
