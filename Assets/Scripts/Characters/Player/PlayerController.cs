using System;
using UnityEngine;

namespace Characters.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float speed = 10f;
        [SerializeField] private HealthController healthController;
        [SerializeField] private FlowerProjectile flowerPrefab;
        
        private HealthController bossHealth;
        
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
        }

        private void TryAttack()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                var projectile = Instantiate(flowerPrefab, transform.position, Quaternion.identity);
                projectile.Init(bossHealth);
            }
        }

        private void TryMove()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            Vector2 movement = new Vector2(horizontal, vertical).normalized;
            transform.Translate(movement * (speed * Time.deltaTime));
        }

        public HealthController GetHealthControl()
        {
            return healthController;
        }

        public void setBossTarget(HealthController boss)
        {
            bossHealth = boss;
        }
    }
}