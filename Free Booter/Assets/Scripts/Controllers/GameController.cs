using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private Inventory inventory;
    void Start()
    {
        DontDestroyOnLoad(this);
        inventory = FindObjectOfType<Inventory>();
    }
    public void GiveStartingInventory()
    {
        inventory.GiveItem(0, 80);
        inventory.GiveItem(1, 50);
        inventory.GiveItem(2, 100);
        inventory.GiveItem(6, 1);
        inventory.GiveItem(17, 1);
    }
}
