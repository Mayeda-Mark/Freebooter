using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public bool isSideScroll = default;
    public string levelMusic = default;
    public string ambiantNoise = default;
    //SoundManager sound;
    MusicManager music;
    // Start is called before the first frame update
    void Start()
    {
        //sound = FindObjectOfType<SoundManager>();

        music = FindObjectOfType<MusicManager>();
        if(isSideScroll)
        {
            Physics2D.gravity = new Vector2(0, -10);
        } else
        {
            Physics2D.gravity = new Vector2(0, 0);
        }
        if(levelMusic != null)
        {
            music.ChangeTrack(levelMusic);
        }
        if(ambiantNoise != null)
        {
            music.ChangeAmbianceTrackWithoutFade(ambiantNoise);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
