using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class UIstartManager : MonoBehaviour
    {
        [SerializeField] private GameObject _canvasFirst;

        void Start()
        {
            EventSystem.current.SetSelectedGameObject(_canvasFirst);
        }

        public void OnStartButtonClick()
        {
            SceneManager.LoadScene("Loader");
        }

        public void OnExitButtonClick()
        {
            #if UNITY_EDITOR
                        // If running in the Unity Editor, stop playing
                        UnityEditor.EditorApplication.isPlaying = false;
            #else
                            // If running as a built game, quit the application
                            Application.Quit();
            #endif
        }
    }
}