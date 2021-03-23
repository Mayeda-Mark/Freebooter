using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    bool isPaused = false;
    LevelLoader levelLoader;
    private void Awake()
    {
        levelLoader = FindObjectOfType<LevelLoader>();
    }
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        PauseListener();
    }
    private void PauseListener()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(!isPaused);
            isPaused = !isPaused;
            if (isPaused)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
    }
    public void Restart()
    {
        levelLoader.LoadOverworld();
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void MainButton()
    {
        levelLoader.LoadMainMenu();
    }
}
