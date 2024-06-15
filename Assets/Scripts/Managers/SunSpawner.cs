using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class SunSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject sunPrefab;
        [SerializeField] private float spawnInterval = 5f;
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
            // Get the screen dimensions
            float screenX = Random.Range(0f, Screen.width);
            float screenY = Random.Range(0f, Screen.height);

            // Convert screen position to world position
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(
                new Vector3(screenX, screenY, mainCamera.nearClipPlane));
            return worldPosition;
        }
    }
}