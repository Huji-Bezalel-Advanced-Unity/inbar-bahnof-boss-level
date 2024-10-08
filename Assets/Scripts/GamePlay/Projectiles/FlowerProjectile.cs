using System.Collections;
using System.Collections.Generic;
using Characters;
using Characters.General;
using UnityEngine;


namespace GamePlay.Projectiles
{
    public class FlowerProjectile : Projectile
    {
        private Vector3 direction;

        public override void Init(HealthController target, HealthController shooter)    
        {
            base.Init(target, shooter);
            direction = (projectileTarget.transform.position - transform.position).normalized;
        }

        private void Update()
        {
            MoveToEnemy();
        }

        private void MoveToEnemy()
        {
            transform.Translate(direction * (speed * Time.deltaTime));
        }
    }
}