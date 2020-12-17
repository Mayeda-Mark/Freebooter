using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [System.Serializable]
    public class TableTrack
    {
        public string trackName;
        public AudioClip clip;
    }
    public List<TableTrack> tracks;
    //[SerializeField] List<AudioClip> tracks = new List<AudioClip>();
    /*[SerializeField]*/ AudioSource source;
    bool isPlaying = false;
    PlayerPrefsController prefs;
    bool maintainMusic;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        source = GetComponent<AudioSource>();
        source.volume = PlayerPrefsController.GetMasterVolume();
        //prefs = FindObjectOfType<PlayerPrefsController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeTrack(string track)
    {
        if(isPlaying && !maintainMusic)
        {
            source.Stop();
            isPlaying = false;
        }
        if(!maintainMusic)
        {
            var trackToFind = tracks.Find(index => index.trackName == track);
            source.clip = trackToFind.clip;
            print(trackToFind.trackName);
            source.Play();
            isPlaying = true;
        }
        /*foreach(AudioClip clip in tracks)
        {
            if(clip.name == track && !maintainMusic)
            {
                source.clip = clip;
                source.Play();
                isPlaying = true;
            }
        }*/
    }
    public void SetVolume(float volume)
    {
        source.volume = volume;
    }
    public void SetMaintainMusic(bool set) { maintainMusic = set; }
    public void StopMusic()
    {
        source.Stop();
        isPlaying = false;
        maintainMusic = false;
    }
}
