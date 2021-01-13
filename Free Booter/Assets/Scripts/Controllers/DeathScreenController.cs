using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreenController : MonoBehaviour
{
    LevelLoader levelLoader;
    void Start()
    {
        FindObjectOfType<MusicManager>().ChangeTrack("Death");
        levelLoader = FindObjectOfType<LevelLoader>();
    }
    public void MainButton()
    {
        levelLoader.LoadMainMenu();
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Restart()
    {
        levelLoader.LoadOverworld();
    }
}
