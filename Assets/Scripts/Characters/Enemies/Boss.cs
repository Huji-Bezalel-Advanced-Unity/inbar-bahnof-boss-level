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
        [SerializeField] private float _secondPhaseSpeed = 3.5f;

        private HealthController _healthController;
        private int _phase = 1;
        private NavMeshAgent _agent;
        
        private Vector3 _targetPosition;
        private bool _isMovingFromPlayer = false;
        private float _moveSpeedForPoking = 10f;
        private float _pokeDistance = 5;

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

            _phase = 1;
            
            SetShootingDistanceToPlayer(1f);
            StartCoroutine(TryMove());
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
                if (!isEnabled || IsPlayerInRange())
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
                if (!isEnabled) yield return null;
                _agent.SetDestination(player.transform.position);
                yield return null;
            }
        }
        
        private IEnumerator MoveAwayFromPlayer()
        {
            while (_isMovingFromPlayer)
            {
                // Calculate the step size based on the speed and frame time
                float step = _moveSpeedForPoking * Time.deltaTime;

                // Move the player towards the target position
                transform.position = Vector3.MoveTowards(transform.position, _targetPosition, step);
                yield return null;
            }
        }
        
        private IEnumerator StopMovementFromPlayer()
        {
            yield return new WaitForSeconds(0.5f);
            _isMovingFromPlayer = false;
            StopCoroutine(MoveAwayFromPlayer());
            isEnabled = true;
        }
        
        public void AfterPoke()
        {
            isEnabled = false;
            Vector2 randomDirection = UnityEngine.Random.insideUnitCircle;
            _targetPosition = new Vector3(randomDirection.x, randomDirection.y, 0) * _pokeDistance;
            
            _isMovingFromPlayer = true;
            StartCoroutine(MoveAwayFromPlayer());
            StartCoroutine(StopMovementFromPlayer());
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
                AfterPoke();
                
                _phase = 0;
                StopCoroutine(MoveSecondPhase());
                
                _agent.isStopped = true;
                _agent.ResetPath();
                GameManager.instance.GameOver(true);
            }
        }
    }
}