using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] int currentSceneIndex;
    [SerializeField] int timeToWait = 6;
    void Start()
    {
        DontDestroyOnLoad(this);
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 0)
        {
            StartCoroutine(LoadStartMenu());
        }
    }
    IEnumerator LoadStartMenu()
    {
        yield return new WaitForSeconds(timeToWait);
        LoadNextScene();
    }
    public void LoadNextScene()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
    public void GameOver()
    {
        SceneManager.LoadScene("Start Screen");
    }
    public void RestartLevel()
    {
        Time.timeScale = 1;
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    internal void LoadOverworld()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Overworld_test");
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Start");
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void LoadOptions()
    {
        SceneManager.LoadScene("Options");
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    internal void LoadDeathScreen()
    {
        SceneManager.LoadScene("Death");
    }
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
