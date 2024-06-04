using System;
using UnityEngine;

namespace Characters
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 10f;

        private void Update()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            Vector2 movement = new Vector2(horizontal, vertical).normalized;
            transform.Translate(movement * (speed * Time.deltaTime));
        }
    }
}