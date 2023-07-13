using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    AudioSource _startButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGame()
    {
        StartCoroutine(LoadStart());
    }

    private IEnumerator LoadStart()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }

    public void StartButtonSound()
    {
        _startButton.Play();
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }

}
