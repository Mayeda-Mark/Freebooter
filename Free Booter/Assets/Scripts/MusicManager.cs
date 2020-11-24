using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioClip[] tracks;
    //[SerializeField] List<AudioClip> tracks = new List<AudioClip>();
    [SerializeField] AudioSource source;
    bool isPlaying = false;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        //source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeTrack(string track)
    {
        print("Called!");
        foreach(AudioClip clip in tracks)
        {
            if(clip.name == track)
            {
                source.clip = clip;
                source.Play();
            }
        }
    }
}
