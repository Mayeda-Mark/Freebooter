using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreenController : MonoBehaviour
{
    LevelLoader levelLoader;
    void Start()
    {
        levelLoader = FindObjectOfType<LevelLoader>();
    }
    public void Main()
    {
        levelLoader.LoadMainMenu();
    }
    public void Quit()
    {
        Application.Quit();
    }
}
