using System;
using System.Collections;
using Characters.General;
using UnityEngine;
using UnityEngine.AI;

namespace Characters.Enemies
{
    public class Boss : BasicEnemy
    {
        [SerializeField] private AIAttacker attacker;
        [SerializeField] private PoisonProjectile projectilePrefab;
        [SerializeField] private float _secondPhaseSpeed = 4f;

        private HealthController _healthController;
        private int _phase = 1;
        private NavMeshAgent _agent;

        protected void Awake()
        {
            _healthController = GetComponent<HealthController>();
            if (_healthController != null)
            {
                _healthController.Init(OnDeath);
            }

            if (projectilePrefab != null && attacker != null)   
            {
                attacker.Init(projectilePrefab, transform, this);
            }

            _agent = GetComponent<NavMeshAgent>();
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
            _agent.speed = _secondPhaseSpeed;
        }
        
        public override void Restart()
        {
            base.Restart();
            _healthController.Restart();
        }
        
        public HealthController GetHealthControl()
        {
            return _healthController;
        }

        protected override void TryShoot()
        {
            attacker.TryShoot(player.GetHealthControl());
            
            base.TryShoot();
        }

        protected override IEnumerator TryMove()
        {
            while (_phase == 1)
            {
                if (IsPlayerInRange())
                {
                    yield return null;
                }
                else
                {
                    Vector3 playerPos = player.transform.position;
                    Vector3 direction = (playerPos - transform.position).normalized;
                    transform.Translate(direction * (speed * Time.deltaTime));
                }
            }
        }

        private IEnumerator MoveSecondPhase()
        {
            while (_phase == 2)
            {
                _agent.SetDestination(player.transform.position);
                yield return null;
            }
        }

        public override void OnDeath()
        {
            base.OnDeath();
            if (_phase == 1)
            {
                Debug.Log("Boss Phase 2!");
                
                _healthController.Restart();
                _phase = 2;
                StopCoroutine(TryMove());
                SetShootingDistanceToPlayer(8f);
                StartCoroutine(MoveSecondPhase());
            }
            else
            {
                Debug.Log("Boss Died!");
                StopCoroutine(MoveSecondPhase());
            }
        }
    }
}