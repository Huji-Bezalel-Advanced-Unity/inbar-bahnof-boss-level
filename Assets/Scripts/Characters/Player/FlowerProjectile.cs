using System.Collections;
using System.Collections.Generic;
using Characters;
using UnityEngine;


namespace Characters.Player
{
    public class FlowerProjectile : Projectile
    {
        private Vector3 direction;

        public override void Init(HealthController target)
        {
            base.Init(target);
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