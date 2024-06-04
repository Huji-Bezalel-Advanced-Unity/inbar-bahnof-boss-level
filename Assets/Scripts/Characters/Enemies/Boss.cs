using UnityEngine;

namespace Characters.Enemies
{
    public class Boss : BasicEnemy
    {
        public override void TryShoot()
        {
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