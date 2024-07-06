using System;
using System.Collections;
using System.Collections.Generic;
using Characters.General;
using Characters.Player;
using UnityEngine;

namespace Characters.Enemies
{
    public class BasicEnemy : MonoBehaviour
    {
        [SerializeField] protected float speed = 3f;
        [SerializeField] protected float minDistanceToPlayer = 3f;
        [SerializeField] protected float minShootingDistanceToPlayer = 1f;
        
        protected PlayerController player;
        protected bool isEnabled = true;
        
        public virtual void Init(PlayerController playerGiven)
        {
            player = playerGiven;
        }

        protected virtual void Start()
        {
            StartCoroutine(TryMove());
        }

        private void Update()
        {
            if (!isEnabled) return;
            // TryMove();
            TryShoot();
        }
        
        public void GameOver()
        {
            isEnabled = false;
        }

        public virtual void Restart()
        {
            isEnabled = true;
        }

        protected virtual void TryShoot()
        {
            
        }

        protected virtual IEnumerator TryMove()
        {
            yield return null;
        }

        public virtual void OnDeath()
        {
            
        }

        protected void SetShootingDistanceToPlayer(float dis)
        {
            minShootingDistanceToPlayer = dis;
        }

        public bool IsPlayerInShootingRange()
        {
            return Vector3.Distance(transform.position, player.transform.position) < minShootingDistanceToPlayer;
        }
        
        public virtual bool IsPlayerInRange()
        {
            return Vector3.Distance(transform.position, player.transform.position) < minDistanceToPlayer;
        }
    }    
}

