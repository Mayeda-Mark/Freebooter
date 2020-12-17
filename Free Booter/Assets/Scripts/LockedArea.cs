using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedArea : MonoBehaviour
{
    [SerializeField] Collider2D borderCollider;
    [SerializeField] ParticleSystem fogOfWar;
    [SerializeField] TownPortal[] townPortals;
    OverworldNPCSpawner spawner;
    void Start()
    {
        spawner = FindObjectOfType<OverworldNPCSpawner>();
    }
    [System.Obsolete]
    public void UnlockArea()
    {
        fogOfWar.loop = false;
        borderCollider.gameObject.SetActive(false);
        spawner.AddPortals(townPortals);
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
