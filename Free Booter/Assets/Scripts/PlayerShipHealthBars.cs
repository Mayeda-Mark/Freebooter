using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipHealthBars : MonoBehaviour
{
    [SerializeField] GameObject hullHealth;
    [SerializeField] GameObject sailHealth;
    [SerializeField] Health playerHullHealth;
    [SerializeField] Health playerSailsHealth;
    Vector3 hullLocalScale;
    Vector3 sailLocalScale;
    // Start is called before the first frame update
    void Start()
    {
        hullLocalScale = playerHullHealth.transform.localScale;
        sailLocalScale = playerSailsHealth.transform.localScale;
        //playerHealth = FindObjectOfType<PlayerShipController>().GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        hullLocalScale.x = ((float)playerHullHealth.GetHealth() / 100f);
        hullHealth.transform.localScale = hullLocalScale;
        sailLocalScale.x = ((float)playerSailsHealth.GetHealth() / 100f);
        sailHealth.transform.localScale = sailLocalScale;
    }
}
