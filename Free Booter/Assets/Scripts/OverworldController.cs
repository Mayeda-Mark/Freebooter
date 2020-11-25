using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldController : MonoBehaviour
{
    MusicManager music;
    // Start is called before the first frame update
    void Start()
    {
        music = FindObjectOfType<MusicManager>();
        music.ChangeTrack("OverworldDefault");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
