using System;
using UnityEngine;

namespace Characters
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float speed = 10f;
        [SerializeField] private HealthController healthController;
        
        private void Awake()
        {
            healthController.Init(OnDeath);
        }

        private void OnDeath()
        {
            
        }

        private void Update()
        {
            TryMove();
            // TryAttack();
        }

        private void TryMove()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            Vector2 movement = new Vector2(horizontal, vertical).normalized;
            transform.Translate(movement * (speed * Time.deltaTime));
        }

        public HealthController GetHealthControl()
        {
            return healthController;
        }
    }
}