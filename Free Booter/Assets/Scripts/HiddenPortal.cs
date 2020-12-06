using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenPortal : MonoBehaviour
{
    //Collider2D collider;
    [SerializeField] string targetScene = default;
    private void Awake()
    {
        this.gameObject.SetActive(false);
    }
    void Start()
    {
        //collider = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Found it!");
    }
}
