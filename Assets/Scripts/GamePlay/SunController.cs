using System;
using System.Collections;
using System.Collections.Generic;
using GamePlay.Player;
using UnityEngine;

namespace Characters
{
    public class SunController : MonoBehaviour
    {
        private const float LIVE_DURATION = 4f;
        
        [SerializeField] private float _dissolveTime = 1f;

        private SpriteRenderer _sprite;
        private Material _material;
        

        private int _disolveAmount = Shader.PropertyToID("_DissolveAmount");

        private void Start()
        {
            _sprite = GetComponent<SpriteRenderer>();
            _material = _sprite.material;
            _material.SetFloat(_disolveAmount, 0);
            
            StartCoroutine(Die());
        }

        private IEnumerator Vanish()
        {
            float elapsedTime = 0f;
            while (elapsedTime < _dissolveTime)
            {
                elapsedTime += Time.deltaTime;
                float lerpedDissolve = Mathf.Lerp(0f, 1.1f, (elapsedTime / _dissolveTime));
                
                _material.SetFloat(_disolveAmount, lerpedDissolve);

                yield return null;
            }
            Destroy(gameObject);
        }

        private IEnumerator Die()
        {
            yield return new WaitForSeconds(LIVE_DURATION);
            StartCoroutine(Vanish());
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                PlayerController player = other.GetComponent<PlayerController>();
                player.AddEnergy(0.3f);
                StopCoroutine(Die());
                StartCoroutine(Vanish());
            }
        }
    }
}