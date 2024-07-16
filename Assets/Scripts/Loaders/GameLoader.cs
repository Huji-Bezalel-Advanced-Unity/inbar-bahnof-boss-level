using System;
using System.Collections;
using System.Collections.Generic;
using Characters;
using GamePlay.Enemies;
using GamePlay.Player;
using Managers;
using GamePlay;
using Loaders;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Loaders{
    public class GameLoader : MonoBehaviour
    {
        [SerializeField] private LoaderUI loaderUI;
        [SerializeField] private PlayerController playerPrefab;
        [SerializeField] private Boss bossPrefab;
        
        private PlayerController player;
        private Boss boss;
        private Camera mainCamera;
        
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(loaderUI.transform.root.gameObject);
            
            loaderUI.Init(100);
            LoadLogicManager();
        }
        
        private void LoadLogicManager()
        {
            new LogicManager(OnCoreManagersLoaded);
        }

        private void OnCoreManagersLoaded(bool isSuccess)
        {
            if (isSuccess)
            {
                loaderUI.AddProgress(50);
                LoadMainScene();
            }
            else
            {
                Debug.LogException(new Exception("Core Managers failed to load"));
            }
        }
        
        private void LoadMainScene()
        {
            loaderUI.AddProgress(10);
            SceneManager.sceneLoaded += OnMainSceneLoaded;
            SceneManager.LoadScene("MainGame");
        }

        private void OnMainSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            SceneManager.sceneLoaded -= OnMainSceneLoaded;
            loaderUI.AddProgress(20);
            
            LoadPlayer();
            LoadBoss();
            
            GameManager manager = FindObjectOfType<GameManager>();
            manager.Init(player, boss);
            
            mainCamera = Camera.main;
            if (mainCamera != null)
            {
                var cameraScript = mainCamera.GetComponent<SmoothOrthographicCameraFollow>();

                if (cameraScript != null)
                {
                    cameraScript.SetTarget(player.transform);
                }
            }

            EnergyUI energyUI = FindObjectOfType<EnergyUI>();
            if (energyUI != null)
            {
                energyUI.SetEnergyInPlayer(player);
                energyUI.Init(100);
            }

            OnLoadComplete();
        }

        private void LoadBoss()
        {
            if (bossPrefab == null)
            {
                Debug.LogError("Boss prefab is not assigned in the inspector.");
                return;
            }
            Vector2 randomPosition = Random.insideUnitCircle * 4;
            Vector2 position = (Vector2)Camera.main.transform.position + randomPosition;

            boss = Instantiate(bossPrefab, position, Quaternion.identity);
            boss.Init(player);
            player.setBossTarget(boss.GetHealthControl());
            loaderUI.AddProgress(10);
        }

        private void LoadPlayer()
        {
            if (playerPrefab == null)
            {
                Debug.LogError("Player prefab is not assigned in the inspector.");
                return;
            }
            player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            loaderUI.AddProgress(10);
        }

        private void OnLoadComplete()
        {
            Destroy(this.gameObject);
            Destroy(loaderUI.transform.root.gameObject);
        }
    }
}

