using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters.Enemies
{
    public class BasicEnemy : MonoBehaviour
    {
        [SerializeField] protected float speed = 3f;
        [SerializeField] protected float minDistanceToPlayer = 5f;
        
        protected PlayerMovement player;
        
        public virtual void Init(PlayerMovement playerGiven)
        {
            player = playerGiven;
        }
        
        private void Update()
        {
            TryMove();
            TryShoot();
        }

        public virtual void TryShoot()
        {
            
        }

        public virtual void TryMove()
        {
            
        }

        public virtual void OnDeath()
        {
            
        }
        
        public virtual void OnHit()
        {
            
        }
        
        public virtual bool IsPlayerInRange()
        {
            return Vector3.Distance(transform.position, player.transform.position) < minDistanceToPlayer;
        }
    }    
}

