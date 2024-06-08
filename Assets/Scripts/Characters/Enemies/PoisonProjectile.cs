using System;
using System.Collections;
using System.Collections.Generic;
using Characters;
using UnityEngine;

public class PoisonProjectile : Projectile
{
    private HealthController projectileTarget;
    private Vector3 direction;
    
    public override void Init(HealthController target)
    {
        projectileTarget = target;
        direction = (projectileTarget.transform.position - transform.position).normalized;
        StartCoroutine(Die());
        
        base.Init(target);
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(deathTime);
        Destroy(gameObject);
    }

    private void Update()
    {
        MoveToPlayer();
    }

    private void MoveToPlayer()
    {
        transform.Translate(direction * (speed * Time.deltaTime));

        // var playerPos = projectileTarget.transform.position;
        // var direction = (projectileTarget.transform.position - transform.position).normalized;
        // transform.Translate(direction * (speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == projectileTarget.gameObject)
        {
            projectileTarget.TakeDamage(damage);
            StopCoroutine(Die());
            Destroy(gameObject);
        }
    }
}
