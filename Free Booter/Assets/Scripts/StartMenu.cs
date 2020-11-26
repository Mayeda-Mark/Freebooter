using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    [SerializeField] Canvas versionNotesCanvas;
    [SerializeField] GameObject newCanvas;
    [SerializeField] GameObject differentCanvas;
    [SerializeField] GameObject controlsCanvas;
    GameObject openCanvas;
    LevelLoader levelLoader;
    MusicManager music;
    private void Start()
    {
        music = FindObjectOfType<MusicManager>();
        music.ChangeTrack("MenuTrack");
        music.SetMaintainMusic(true);
        levelLoader = FindObjectOfType<LevelLoader>();
        newCanvas.SetActive(false);
        differentCanvas.SetActive(false);
        controlsCanvas.SetActive(false);
        versionNotesCanvas.gameObject.SetActive(false);
    }
    public void OpenVersionNotes()
    {
        versionNotesCanvas.gameObject.SetActive(true);
    }
    public void CloseVersionNotes()
    {
        versionNotesCanvas.gameObject.SetActive(false);
    }
    public void OpenNew()
    {
        newCanvas.SetActive(true);
        openCanvas = newCanvas;
    }
    public void OpenDifferent()
    {
        differentCanvas.SetActive(true);
        openCanvas = differentCanvas;
    }
    public void OpenControls()
    {
        controlsCanvas.SetActive(true);
        openCanvas = controlsCanvas;
    }
    public void BackToVersionNotes()
    {
        openCanvas.SetActive(false);
    }
    public void StartGame()
    {
        music.SetMaintainMusic(false);
        //music.StopMusic();
        levelLoader.LoadOverworld();
    }
    public void Options()
    {
        levelLoader.LoadOptions();
    }
    public void Quit()
    {
        Application.Quit();
    }
}
