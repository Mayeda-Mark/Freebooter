using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenPortal : MonoBehaviour
{
    //Collider2D collider;
    [SerializeField] string targetScene = default;
    //Inventory inventory;
    LevelLoader levelLoader;
    MusicManager musicManager;
    private void Awake()
    {
    }
    void Start()
    {
        musicManager = FindObjectOfType<MusicManager>();
        gameObject.SetActive(false);
        //inventory = FindObjectOfType<Inventory>();
        //inventory.GiveItem(2, 500);
        levelLoader = FindObjectOfType<LevelLoader>();
        //collider = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerShipController>() != null)
        {
            musicManager.StopMusic();
            levelLoader.LoadSceneByName(targetScene);
        }        
    }
}
