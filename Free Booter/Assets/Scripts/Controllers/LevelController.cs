using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public bool isSideScroll, isMenu = default;
    public string levelMusic = null;
    public string ambiantNoise = null;
    private Inventory inventory;
    /*public Transform startingLocation;
    private Player player;*/
    MusicManager music;
    MapController map;
    void Start()
    {
        music = FindObjectOfType<MusicManager>();
        FindObjectOfType<Inventory>().StartNewLevel();
        inventory = FindObjectOfType<Inventory>();
        //player = FindObjectOfType<Player>();
        /*startingLocation = GetComponentInChildren<Transform>();
        player.StartNewLevel(startingLocation);*/
        if(isSideScroll)
        {
            Physics2D.gravity = new Vector2(0, -10);
        } else if(!isSideScroll)
        {
            Physics2D.gravity = new Vector2(0, 0);
            map = FindObjectOfType<MapController>();
            if(map != null)
            {
                FindObjectOfType<MapController>().UnlockAllMapsInInventory();
            }
        }
        if(isMenu)
        {
            inventory.DisableUIs();
        }
        else if(!isMenu)
        {
            inventory.EnableUIs();
        }
        if(levelMusic != "")
        {
            music.StopMusic();
            music.ChangeTrack(levelMusic);
        }
        if(ambiantNoise != "")
        {
            music.ChangeAmbianceTrackWithoutFade(ambiantNoise);
        } else
        {
            print("Blarg!");
        }
    }
}
