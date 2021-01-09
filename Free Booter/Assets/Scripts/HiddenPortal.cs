using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenPortal : MonoBehaviour
{
    //Collider2D collider;
    [SerializeField] string targetScene = default;
    Inventory inventory;
    private void Awake()
    {
        gameObject.SetActive(false);
        inventory = FindObjectOfType<Inventory>();
    }
    void Start()
    {
        inventory.GiveItem(2, 500);
        //collider = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
}
