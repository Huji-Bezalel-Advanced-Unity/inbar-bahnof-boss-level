using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class SunSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject sunPrefab;
        [SerializeField] private float spawnInterval = 5f;
        
        [SerializeField] private float leftBorder = -10f;
        [SerializeField] private float rightBorder = 10f;
        [SerializeField] private float bottomBorder = -5f;
        [SerializeField] private float topBorder = 5f;
        
        private Camera mainCamera;

        private void Start()
        {
            mainCamera = Camera.main;
            StartCoroutine(SpawnSunRoutine());
        }

        private IEnumerator SpawnSunRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(spawnInterval);
                SpawnSun();
            }
        }

        private void SpawnSun()
        {
            if (sunPrefab == null || mainCamera == null)
            {
                Debug.LogWarning("SunPrefab or MainCamera is not assigned.");
                return;
            }

            Vector3 randomPosition = GetRandomPositionInView();
            Instantiate(sunPrefab, randomPosition, Quaternion.identity);
        }

        private Vector3 GetRandomPositionInView()
        {
            // Get the screen dimensions within the borders and the camera view
            float minX = Mathf.Max(leftBorder, mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane)).x);
            float maxX = Mathf.Min(rightBorder, mainCamera.ViewportToWorldPoint(new Vector3(1, 0, mainCamera.nearClipPlane)).x);
            float minY = Mathf.Max(bottomBorder, mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane)).y);
            float maxY = Mathf.Min(topBorder, mainCamera.ViewportToWorldPoint(new Vector3(0, 1, mainCamera.nearClipPlane)).y);

            // Randomize position within these constraints
            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);
            
            // Return the world position with the same z-position as the camera's near clip plane
            return new Vector3(randomX, randomY, 0);
        }

        public void StopSpawn()
        {
            StopCoroutine(SpawnSunRoutine());
        }

        public void RestartSpawn()
        {
            StartCoroutine(SpawnSunRoutine());
        }
    }
}