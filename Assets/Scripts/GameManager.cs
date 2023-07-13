using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    AudioSource _audioSource;
    [SerializeField]
    AudioClip _levelMusic;
    [SerializeField]
    AudioClip _bossMusic;
    [SerializeField]
    AudioClip _gameOverMusic;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _levelMusic;
        _audioSource.Play();
    }

    public void GameOver()
    {
        _audioSource.clip = _gameOverMusic;
        _audioSource.loop = false;
        _audioSource.Play();
    }

    public void RestartLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);

        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void MusicPause(bool pause)
    {
        if (pause == true)
        {
            _audioSource.Pause();
        }
        if (pause == false)
        {
            _audioSource.Play();
        }
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }
}
