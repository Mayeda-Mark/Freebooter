using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject ship, sideScroll;

    private Inventory inventory;
    //private LevelController level;
    private void Start()
    {
        int numPlayerCOntrollers = FindObjectsOfType<Player>().Length;
        if(numPlayerCOntrollers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
        /*ship = GetComponentInChildren<PlayerShipController>().gameObject;
        sideScroll = GetComponentInChildren<PlayerSidescrollController>().gameObject;*/
        inventory = GetComponentInChildren<Inventory>();
        //StartNewLevel();
    }
    public void StartNewLevel(Transform startingLocation)
    {
        if(FindObjectOfType<LevelController>().isSideScroll)
        {
            ship.SetActive(false);
            sideScroll.SetActive(true);
            sideScroll.transform.position = startingLocation.position
                ;
        }
        else
        {
            ship.SetActive(true);
            sideScroll.SetActive(false);
            ship.transform.position = startingLocation.position;
        }
    }
}
