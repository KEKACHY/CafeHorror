using System.Collections.Generic;
using UnityEngine;

public class PauseGameUI : MonoBehaviour
{
    [SerializeField] private GameObject _gameUI;
    [SerializeField] private GameObject _pauseUI;
    [SerializeField] private PlayerController _playerController;
    private AudioSource[] audioSources;
    private bool isPaused = false;

    private void Start()
    {
        audioSources = FindObjectsOfType<AudioSource>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        _gameUI.SetActive(true);
        _pauseUI.SetActive(false);
        Time.timeScale = 1f; 
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _playerController.enabled = true;
        isPaused = false;
        foreach (AudioSource source in audioSources)
        {
            if (source != null)
                source.UnPause();
        }
    }

    void Pause()
    {
        _gameUI.SetActive(false);
        _pauseUI.SetActive(true);
        Time.timeScale = 0f; 
        Cursor.lockState = CursorLockMode.None; 
        Cursor.visible = true;
        _playerController.enabled = false;
        isPaused = true;
        foreach (AudioSource source in audioSources)
        {
            if (source != null)
                source.Pause();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
        // Если тестируем в редакторе:
        // UnityEditor.EditorApplication.isPlaying = false;
    }
}
