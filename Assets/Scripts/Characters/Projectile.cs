using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] protected int damage = 5;
        [SerializeField] protected float speed = 15f;
        [SerializeField] protected float deathTime = 2f;
        
        protected HealthController projectileTarget;
        
        public virtual void Init(HealthController target)
        {
            projectileTarget = target;
            StartCoroutine(Die());
        }
        
        private IEnumerator Die()
        {
            yield return new WaitForSeconds(deathTime);
            Destroy(gameObject);
        }
        
        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (IsTarget(other))
            {
                HandleHit();
            }
        }
        
        protected bool IsTarget(Collider2D other)
        {
            return other.gameObject == projectileTarget.gameObject;
        }

        protected virtual void HandleHit()
        {
            projectileTarget.TakeDamage(damage);
            StopCoroutine(Die());
            Destroy(gameObject);
        }
    }
}
