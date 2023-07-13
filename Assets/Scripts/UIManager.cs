using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _scoreText;
    Player _player;
    [SerializeField]
    Transform _lifeSprites;
    int _lifeCount;
    bool _isFading = false;
    [SerializeField]
    float _fadeSpeed;
    bool _gameOver = false;
    [SerializeField]
    GameObject _gameOverPanel;
    [SerializeField]
    Image _gameOverImage;
    [SerializeField]
    TMP_Text _gameOverText;
    [SerializeField]
    GameObject _quitRestart;
    [SerializeField]
    GameObject _settingsMenu;
    GameManager _gameManager;
    SpawnManager _spawnManager;
    [SerializeField]
    Transform _countdown;
    [SerializeField]
    Vector3 _startSize = new Vector3(0f, 0f, 0f);
    [SerializeField]
    Vector3 _endSize = new Vector3(9.9f, 9.9f, 9.9f);
    [SerializeField]
    float _duration = 1f;


    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _player = GameObject.Find("Player").GetComponent<Player>();
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        StartCoroutine(CountdownStart());
    }

    // Update is called once per frame
    void Update()
    {
        Pause();
    }

    public void PlayerScoreUpdate(int newPlayerScore)
    {
        _scoreText.text = "Score: " + newPlayerScore.ToString();
    }

    public void PlayerLifeUpdate(int currentPlayerLife)
    {
        _lifeCount = currentPlayerLife;

        if (_lifeCount == 2)
        {
            _lifeSprites.GetChild(2).gameObject.SetActive(false);
        }
        if (_lifeCount == 1)
        {
            _lifeSprites.GetChild(1).gameObject.SetActive(false);
        }
        if (_lifeCount == 0)
        {
            _lifeSprites.GetChild(0).gameObject.SetActive(false);
            ShowGameOver();
        }
    }

    void ShowGameOver()
    {
        _gameOver = true;
        _gameManager.GameOver();
        _gameOverPanel.SetActive(true);
        StartCoroutine(FadeIn(_gameOverImage, _gameOverText, _quitRestart));
    }

    IEnumerator FadeIn(Image panel, TMP_Text text, GameObject quitRestart)
    {
        if (_isFading == false)
        {
            _isFading = true;
            panel.gameObject.SetActive(true);
            text.gameObject.SetActive(true);
            Color panelColor = panel.color;
            Color textColor = text.color;
            while (panelColor.a < 1 || textColor.a < 1)
            {
                panelColor.a += _fadeSpeed * Time.deltaTime;
                textColor.a += _fadeSpeed * Time.deltaTime;
                panel.color = panelColor;
                text.color = textColor;
                yield return null;
            }
            quitRestart.SetActive(true);
            _isFading = false;
        }
        
    }

    void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _gameOver == false)
        {
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                _settingsMenu.SetActive(false);
                _gameManager.MusicPause(false);
            }
            else
            {
                Time.timeScale = 0;
                _settingsMenu.SetActive(true);
                _gameManager.MusicPause(true);
            }
        }
    }

    public void ExitPauseMenu()
    {
        Time.timeScale = 1;
        _settingsMenu.SetActive(false);
        _gameManager.MusicPause(false);
    }

    IEnumerator CountdownStart()
    {
        _countdown.GetChild(0).gameObject.SetActive(true);

        RectTransform rectTransform = _countdown.GetChild(0).GetComponent<RectTransform>();
        rectTransform.localScale = _startSize;

        float elapsedTime = 0f;
        while (elapsedTime < _duration)
        {
            elapsedTime += Time.deltaTime;
            rectTransform.localScale = Vector3.Lerp(_startSize, _endSize, elapsedTime / _duration);
            yield return null;
        }

        _countdown.GetChild(0).gameObject.SetActive(false);

        StartCoroutine(Countdown2());
    }

    IEnumerator Countdown2()
    {
        _countdown.GetChild(1).gameObject.SetActive(true);

        RectTransform rectTransform = _countdown.GetChild(1).GetComponent<RectTransform>();
        rectTransform.localScale = _startSize;

        float elapsedTime = 0f;
        while (elapsedTime < _duration)
        {
            elapsedTime += Time.deltaTime;
            rectTransform.localScale = Vector3.Lerp(_startSize, _endSize, elapsedTime / _duration);
            yield return null;
        }

        _countdown.GetChild(1).gameObject.SetActive(false);

        StartCoroutine(Countdown1());
    }

    IEnumerator Countdown1()
    {
        _countdown.GetChild(2).gameObject.SetActive(true);

        RectTransform rectTransform = _countdown.GetChild(2).GetComponent<RectTransform>();
        rectTransform.localScale = _startSize;

        float elapsedTime = 0f;
        while (elapsedTime < _duration)
        {
            elapsedTime += Time.deltaTime;
            rectTransform.localScale = Vector3.Lerp(_startSize, _endSize, elapsedTime / _duration);
            yield return null;
        }

        _countdown.GetChild(2).gameObject.SetActive(false);

        StartCoroutine(CountdownGo());
    }

    IEnumerator CountdownGo()
    {
        _countdown.GetChild(3).gameObject.SetActive(true);

        RectTransform rectTransform = _countdown.GetChild(3).GetComponent<RectTransform>();
        rectTransform.localScale = _startSize;

        float elapsedTime = 0f;
        while (elapsedTime < _duration)
        {
            elapsedTime += Time.deltaTime;
            rectTransform.localScale = Vector3.Lerp(_startSize, _endSize, elapsedTime / _duration);
            yield return null;
        }

        _countdown.GetChild(3).gameObject.SetActive(false);

        _spawnManager.StartSpawning();
        _player.StartPlayerMovement();
    }
}
