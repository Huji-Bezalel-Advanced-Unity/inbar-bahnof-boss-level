using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] protected int damage = 5;
        [SerializeField] protected float speed = 20f;
        [SerializeField] protected float deathTime = 2f;
        
        public virtual void Init(HealthController target)
        {
            
        }
    }
}
