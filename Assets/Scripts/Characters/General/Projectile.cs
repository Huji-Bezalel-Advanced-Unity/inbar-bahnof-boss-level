using System;
using System.Collections;
using System.Collections.Generic;
using Characters.General;
using UnityEngine;

namespace Characters.General
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] protected int damage = 5;
        [SerializeField] protected float speed = 10f;
        [SerializeField] protected float deathTime = 2f;

        protected Vector3 _startPosition;
        protected HealthController projectileTarget;
        protected HealthController projectileShooter;
        
        public virtual void Init(HealthController target, HealthController shooter)
        {
            projectileTarget = target;
            projectileShooter = shooter;
            _startPosition = transform.position;
            StartCoroutine(Die());
        }
        
        protected IEnumerator Die()
        {
            yield return new WaitForSeconds(deathTime);
            Destroy(gameObject);
        }
        
        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (IsTheWantedTarget(other))
            {
                HandleHit();
            }
            else if(IsTarget(other))
            {
                StopCoroutine(Die());
                Destroy(gameObject);
            }
        }
        
        protected bool IsTarget(Collider2D other)
        {
            return other.gameObject != projectileShooter.gameObject;
        }

        protected virtual void HandleHit()
        {
            // Calculate the hit direction
            Vector3 hitDirection = (projectileTarget.transform.position - _startPosition).normalized;
            projectileTarget.TakeDamage(damage, new Vector3(hitDirection.x, hitDirection.y, 0));
            
            StopCoroutine(Die());
            Destroy(gameObject);
        }
        
        protected bool IsTheWantedTarget(Collider2D other)
        {
            return other.gameObject == projectileTarget.gameObject;
        }
    }
}
