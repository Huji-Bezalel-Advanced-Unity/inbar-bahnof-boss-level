using System;
using Characters.General;
using UnityEngine;

namespace Characters.Player
{
    public class PlayerController : MonoBehaviour
    {
        private const float ENERGY_THREASHHOLD = 0.3f;
        
        [SerializeField] private float cooldown = 0.5f;
        [SerializeField] private HealthController healthController;
        [SerializeField] private FlowerProjectile flowerPrefab;
        [SerializeField] private float speed = 5f;
        [SerializeField] private EnergyUI energyUI;
        
        private HealthController bossHealth;
        private DateTime lastAttackTime = DateTime.UtcNow;
        private float energyLevel = 1;
        private float energyToUpdate = 0;
        
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
            RemoveEnergy(0.00005f);
        }

        private void RemoveEnergy(float toRemove)    
        {
            if (energyLevel - toRemove >= ENERGY_THREASHHOLD)
            {
                energyLevel -= toRemove;
            }
            else
            {
                energyLevel = ENERGY_THREASHHOLD;
            }

            energyToUpdate += toRemove;
            if (energyToUpdate >= 0.01f)
            {
                energyUI.RemoveProgress(Mathf.RoundToInt(energyToUpdate * 100));
                energyToUpdate = 0;
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

            RemoveEnergy(0.1f);
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

        public void SetEnergyUI(EnergyUI energy)
        {
            energyUI = energy;
        }

        public void setBossTarget(HealthController boss)
        {
            bossHealth = boss;
        }

        public void AddEnergy(float toAdd)
        {
            if ((energyLevel + toAdd) < 1)
            {
                energyLevel += toAdd;
            }
            else
            {
                energyLevel = 1;
            }

            if (energyUI != null) energyUI.AddProgress(Mathf.RoundToInt(toAdd * 100));
        }
    }
}