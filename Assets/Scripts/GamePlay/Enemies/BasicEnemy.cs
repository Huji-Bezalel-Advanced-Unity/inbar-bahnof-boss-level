using System;
using System.Collections;
using System.Collections.Generic;
using Characters.General;
using GamePlay.Player;
using UnityEngine;

namespace GamePlay.Enemies
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
            TryShoot();
        }
        
        public void GameOver()
        {
            isEnabled = false;
        }

        public virtual void Restart()
        {
            isEnabled = true;
            StartCoroutine(TryMove());
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

