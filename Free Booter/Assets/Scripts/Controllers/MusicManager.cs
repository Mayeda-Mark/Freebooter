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
    public List<TableTrack> musicTracks = new List<TableTrack>();
    public List<TableTrack> ambianceTracks = new List<TableTrack>();
    //[SerializeField] List<AudioClip> tracks = new List<AudioClip>();
    /*[SerializeField]*/
    public AudioSource musicAudioSource;
    public AudioSource ambianceAudioSource;
    bool isPlaying = false;
    PlayerPrefsController prefs;
    bool maintainMusic;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        //musicSource = GetComponent<AudioSource>();
        musicAudioSource.volume = PlayerPrefsController.GetMasterVolume();
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
            musicAudioSource.Stop();
            isPlaying = false;
        }
        if(!maintainMusic)
        {
            var trackToFind = musicTracks.Find(index => index.trackName == track);
            musicAudioSource.clip = trackToFind.clip;
            print(trackToFind.trackName);
            musicAudioSource.Play();
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
        musicAudioSource.volume = volume;
    }
    public void SetMaintainMusic(bool set) { maintainMusic = set; }
    public void StopMusic()
    {
        musicAudioSource.Stop();
        isPlaying = false;
        maintainMusic = false;
    }
    public void ChangeAmbianceTrackWithoutFade(string tag)
    {
        var clipToPlay = ambianceTracks.Find(tracks => tracks.trackName == tag);
        ambianceAudioSource.clip = clipToPlay.clip;
        //ambianceAudioSource.volume = clipToPlay.volume;
        ambianceAudioSource.Play();
    }

    public void ChangeAmbianceTrackForState(string state)
    {
        StartCoroutine(FadeOutAndPlayNextAmbianceTrack(state));
    }

    public IEnumerator FadeOutAndPlayNextAmbianceTrack(string state)
    {
        float currentTIme = 0;
        float start = ambianceAudioSource.volume;
        while (currentTIme < 2f)
        {
            currentTIme += Time.deltaTime;
            ambianceAudioSource.volume = Mathf.Lerp(start, 0, currentTIme / 2f);
            if (ambianceAudioSource.volume == 0)
            {
                ambianceAudioSource.Stop();
                ChangeAmbianceTrackWithoutFade(state);
                //musicAudioSource.volume = start;
                //audioSource.Play();
            }
            yield return null;
        }
        yield break;
    }
}
