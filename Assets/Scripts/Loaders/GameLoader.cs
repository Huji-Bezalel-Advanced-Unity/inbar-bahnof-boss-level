using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Loaders;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Loaders{
    public class GameLoader : MonoBehaviour
    {
        [SerializeField] private LoaderUI loaderUI;
        
        private void Start()
        {
            // DontDestroyOnLoad(gameObject);
            // DontDestroyOnLoad(loaderUI.transform.root.gameObject);
            
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
            loaderUI.AddProgress(50);
            SceneManager.LoadScene("MainGame");
        }
    }
}

