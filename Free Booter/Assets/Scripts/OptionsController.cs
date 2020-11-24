using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OptionsController : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    //[SerializeField] Slider difficultySlider;
    [SerializeField] float defaultVolume = 0.6f;
    //[SerializeField] float defaultDifficulty = 0.6f;
    [SerializeField] Text volumeDisplay;
    //[SerializeField] Text difficultyDisplay;

    // Use this for initialization
    void Start()
    {
        volumeSlider.value = PlayerPrefsController.GetMasterVolume();
        //difficultySlider.value = PlayerPrefsController.GetMasterDifficulty();
        //DisplayDifficulty();
        DisplayVolume();
    }

    // Update is called once per frame
    void Update()
    {
        /*var musicPlayer = FindObjectOfType<MusicPlayer>();
        if (musicPlayer)
        {
            musicPlayer.SetVolume(volumeSlider.value);
        }
        else
        {
            Debug.LogWarning("No music player found!");
        }*/
        //DisplayDifficulty();
        DisplayVolume();
    }
    public void SaveAndExit()
    {
        PlayerPrefsController.SetMasterVolume(volumeSlider.value);
        //PlayerPrefsController.SetDifficulty(difficultySlider.value);
        FindObjectOfType<LevelLoader>().LoadMainMenu();
    }
    public void SetToDefaults()
    {
        volumeSlider.value = defaultVolume;
        //difficultySlider.value = defaultDifficulty;
    }
    /*private void DisplayDifficulty()
    {
        int difficultyInt = Mathf.RoundToInt(difficultySlider.value * 10);
        difficultyDisplay.text = difficultyInt.ToString();
    }*/
    private void DisplayVolume()
    {
        int volumeInt = Mathf.RoundToInt(volumeSlider.value * 100);
        volumeDisplay.text = volumeInt.ToString();
    }
}
