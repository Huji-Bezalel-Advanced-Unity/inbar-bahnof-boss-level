using System;
using System.Collections;
using System.Collections.Generic;
using GamePlay.Enemies;
using GamePlay.Player;
using Loaders;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private TextMeshProUGUI _finalText;
    [SerializeField] private SunSpawner _sunSpawner;
    [SerializeField] private GameObject _firstButtonChosen;

    private PlayerController _playerController;
    private Boss _boss;
    
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject); // Destroy any duplicate instances
        }

        
        gameOverCanvas.SetActive(false);
    }

    public void Init(PlayerController player, Boss boss)    
    {
        _playerController = player;
        _boss = boss;
    }

    public void GameOver(bool won)
    {
        _sunSpawner.StopSpawn();
        _playerController.GameOver();
        _boss.GameOver();
        gameOverCanvas.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_firstButtonChosen);

        if (won)
        {
            _finalText.text = "You Won!";
        }
        else
        {
            _finalText.text = "You Lost!";
        }
    }

    public void RestartGame()
    {
        gameOverCanvas.SetActive(false);
        _playerController.Restart();
        _boss.Restart();
        _sunSpawner.RestartSpawn();
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
