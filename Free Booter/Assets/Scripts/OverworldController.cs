
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldController : MonoBehaviour
{
    MusicManager music;
    [SerializeField] GameObject pauseMenu;
    bool isPaused = false;
    LevelLoader levelLoader;
    // Start is called before the first frame update
    void Start()
    {
        levelLoader = FindObjectOfType<LevelLoader>();
        music = FindObjectOfType<MusicManager>();
        music.ChangeTrack("OverworldDefault");
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        PauseListener();
    }
    private void PauseListener()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(!isPaused);
            isPaused = !isPaused;
            if(isPaused)
            {
                Time.timeScale = 0;
            } else
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
    /*public void Main()
    {
        levelLoader.LoadMainMenu();
    }*/
}
