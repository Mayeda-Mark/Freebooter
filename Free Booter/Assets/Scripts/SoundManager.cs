using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioClip[] sounds;
    AudioSource source;
    private void Start()
    {
        DontDestroyOnLoad(this);
        source = GetComponent<AudioSource>();
    }
    public void PlaySound(string sound)
    {
        foreach(AudioClip clip in sounds)
        {
            if(clip.name == sound)
            {
                source.clip = clip;
                source.Play();
            }
        }
    }
}
