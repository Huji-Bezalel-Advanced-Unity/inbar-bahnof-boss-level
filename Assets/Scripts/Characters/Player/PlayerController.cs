using System;
using System.Collections;
using Characters.Enemies;
using Characters.General;
using UnityEngine;

namespace Characters.Player
{
    public class PlayerController : MonoBehaviour
    {
        private const float ENERGY_THREASHHOLD = 0.3f;
        private const float EnergyRemoveOnUpdate = 0.00001f;
        private const float EnergyRemoveOnShoot = 0.05f;
        
        [SerializeField] private float cooldown = 0.5f;
        [SerializeField] private HealthController healthController;
        [SerializeField] private FlowerProjectile flowerPrefab;
        [SerializeField] private float speed = 5f;
        [SerializeField] private EnergyUI energyUI;
        [SerializeField] private float bossRange = 8f; 

        
        private HealthController _bossHealth;
        private DateTime _lastAttackTime = DateTime.UtcNow;
        private float _energyLevel = 1;
        private float _energyToUpdate = 0;
        private bool _isEnabled = true;
        private bool _isTouchingBoss = false;
        
        private Vector3 _targetPosition;
        private bool _isMovingFromBoss = false;
        private float _moveSpeedForPoking = 10f;
        
        private void Awake()
        {
            healthController.Init(OnDeath);
        }

        private void OnDeath()
        {
            GameManager.instance.GameOver(false);
        }

        private void Update()
        {
            if (!_isEnabled) return;
            
            TryMove();
            TryAttack();
            TouchBossAttack();
            RemoveEnergy(EnergyRemoveOnUpdate);
        }

        private void TouchBossAttack()
        {
            if (!_isTouchingBoss) return;

            if (Input.GetButtonDown("Fire2"))
            {
                _bossHealth.TakeDamage(1f);
                _bossHealth.GetComponent<Boss>().AfterPoke();
                _isTouchingBoss = false;
            }
        }

        private void RemoveEnergy(float toRemove)    
        {
            if (_energyLevel - toRemove >= ENERGY_THREASHHOLD)
            {
                _energyLevel -= toRemove;
            }
            else
            {
                _energyLevel = ENERGY_THREASHHOLD;
            }

            _energyToUpdate += toRemove;
            if (_energyToUpdate >= 0.01f)
            {
                energyUI.RemoveProgress(Mathf.RoundToInt(_energyToUpdate * 100));
                _energyToUpdate = 0;
            }
        }

        private void TryAttack()
        {
            if (_isTouchingBoss) return;
            
            if (Input.GetButtonDown("Fire1"))
            {
                var timePassed = DateTime.UtcNow - _lastAttackTime;
                var isTimePassed = timePassed >= TimeSpan.FromSeconds(cooldown);
                
                if (isTimePassed) Shoot();
            }
        }

        private void Shoot()
        {
            var projectile = Instantiate(flowerPrefab, transform.position, Quaternion.identity);
            projectile.Init(_bossHealth, healthController);
            
            _lastAttackTime = DateTime.UtcNow;

            RemoveEnergy(EnergyRemoveOnShoot);
        }

        private void TryMove()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            Vector2 movement = new Vector2(horizontal, vertical).normalized;
            transform.Translate(movement * (_energyLevel * speed * Time.deltaTime));
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Boss"))
            {
                healthController.TakeDamage(0.1f);
                _isTouchingBoss = true;
            }
        }
        
        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Boss"))
            {
                _isTouchingBoss = false;
            }
        }

        public void GameOver()
        {
            _isEnabled = false;
        }
        
        public void Restart()
        {
            healthController.Restart();
            _energyLevel = 1;
            energyUI.AddProgress(100);
            _isEnabled = true;
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
            _bossHealth = boss;
        }

        public void AddEnergy(float toAdd)
        {
            if ((_energyLevel + toAdd) < 1)
            {
                _energyLevel += toAdd;
            }
            else
            {
                _energyLevel = 1;
            }

            if (energyUI != null) energyUI.AddProgress(Mathf.RoundToInt(toAdd * 100));
        }
    }
}