using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBars : MonoBehaviour
{
    [SerializeField] bool sidescroll;
    [SerializeField] GameObject hullHealth;
    [SerializeField] GameObject sailHealth;
    [SerializeField] GameObject sideScrollBar;
    [SerializeField] Health playerHullHealth;
    [SerializeField] Health playerSailsHealth;
    [SerializeField] Health sideScrollHealth;
    Vector3 hullLocalScale;
    Vector3 sailLocalScale;
    Vector3 sideScrollLocalScale;
    void Start()
    {
        if(!sidescroll)
        {
            hullLocalScale = playerHullHealth.transform.localScale;
            sailLocalScale = playerSailsHealth.transform.localScale;
        } else
        {
            sideScrollLocalScale = sideScrollBar.transform.localScale;
        }
    }
    void Update()
    {
        if(!sidescroll)
        {
            hullLocalScale.x = ((float)playerHullHealth.GetHealth() / 100f);
            if (playerHullHealth.GetHealth() <= 0)
            {
                hullLocalScale.x = 0;
            }
            hullHealth.transform.localScale = hullLocalScale;
            sailLocalScale.x = ((float)playerSailsHealth.GetHealth() / 100f);
            if (playerSailsHealth.GetHealth() <= 0)
            {
                sailLocalScale.x = 0;
            }
            sailHealth.transform.localScale = sailLocalScale;
        }
        else
        {
            sideScrollLocalScale.x = ((float)sideScrollHealth.GetHealth() / 100f);
            if(sideScrollHealth.GetHealth() <= 0)
            {
                sideScrollLocalScale.x = 0;
            }
            sideScrollBar.transform.localScale = sideScrollLocalScale;
        }
    }
}
