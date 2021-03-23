using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameObject ship, sideScroll;

    private Inventory inventory;
    //private LevelController level;
    private void Start()
    {
        ship = GetComponentInChildren<PlayerShipController>().gameObject;
        sideScroll = GetComponentInChildren<PlayerSidescrollController>().gameObject;
        inventory = GetComponentInChildren<Inventory>();
        StartNewLevel();
    }
    public void StartNewLevel()
    {
        if(FindObjectOfType<LevelController>().isSideScroll)
        {
            ship.SetActive(false);
            sideScroll.SetActive(true);
        }
        else
        {
            ship.SetActive(true);
            sideScroll.SetActive(false);
        }
    }
}
