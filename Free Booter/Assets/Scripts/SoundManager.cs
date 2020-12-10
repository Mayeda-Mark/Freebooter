using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [System.Serializable]
    public class SoundTable
    {
        public string tag;
        public AudioClip clip;
    }
    [SerializeField] List<SoundTable> sounds;
    AudioSource source;
    private void Start()
    {
        DontDestroyOnLoad(this);
        source = GetComponent<AudioSource>();
    }
    public void PlaySound(string sound)
    {
        var tableSound = sounds.Find(index => index.tag == sound);
        source.clip = tableSound.clip;
        source.Play();
        /*foreach(AudioClip clip in sounds)
        {
            if(clip.name == sound)
            {
                source.clip = clip;
                source.Play();
            }
        }*/
    }
}
