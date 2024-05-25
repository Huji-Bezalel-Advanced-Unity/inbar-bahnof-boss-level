using System;
using System.Collections;
using System.Collections.Generic;
using Charachters;
using Managers;
using Loaders;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Loaders{
    public class GameLoader : MonoBehaviour
    {
        [SerializeField] private LoaderUI loaderUI;
        [SerializeField] private PlayerMovement playerPrefab;
        
        private PlayerMovement player;
        
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
            
            OnLoadComplete();
        }

        private void LoadPlayer()
        {
            if (playerPrefab == null)
            {
                Debug.LogError("Player prefab is not assigned in the inspector.");
                return;
            }
            player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            loaderUI.AddProgress(20);
        }

        private void OnLoadComplete()
        {
            Destroy(this.gameObject);
            Destroy(loaderUI.transform.root.gameObject);
        }
    }
}

