using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedArea : MonoBehaviour
{
    [SerializeField] Collider2D borderCollider;
    [SerializeField] ParticleSystem fogOfWar;
    [SerializeField] TownPortal[] townPortals;
    [SerializeField] WeatherDetector weather;
    OverworldNPCSpawner spawner;
    void Start()
    {
        spawner = FindObjectOfType<OverworldNPCSpawner>();
        //weather = GetComponentInChildren<WeatherDetector>();
        weather.gameObject.SetActive(false);
    }
    [System.Obsolete]
    public void UnlockArea()
    {
        fogOfWar.loop = false;
        borderCollider.enabled = false;
        spawner.AddPortals(townPortals);
        weather.gameObject.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerShipController>();
        if(player)
        {
            player.ExitLockedArea();
        }
    }
}
