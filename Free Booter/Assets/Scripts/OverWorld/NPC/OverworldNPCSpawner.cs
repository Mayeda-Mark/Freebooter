using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldNPCSpawner : MonoBehaviour
{
    [SerializeField] List<TownPortal> townPortals;
    //[SerializeField] List<OverWorldNPC> NPCs;
    [SerializeField] string[] NPCs;
    [SerializeField] float spawnDelay = 10f;
    [SerializeField] bool spawning = true;
    GameObject overworldNPCParent;
    const string NPC_PARENT_NAME = "Overworld NPCs";
    Pooler pooler;
    [SerializeField] int Debug = default;
    // Start is called before the first frame update
    private void Awake()
    {
        pooler = FindObjectOfType<Pooler>();
    }
    IEnumerator Start()
    {
        do{
            yield return StartCoroutine(SpawnShip());
        }
        while(spawning);
    }
    private IEnumerator SpawnShip() {
        CreateNPCParent();
        int portalIndex = GetPortalIndex();
        int NPCIndex = GetNPCIndex();
        //Debug = NPCIndex;
        var newNPC = pooler.SpawnFromPool(NPCs[NPCIndex], townPortals[portalIndex].transform.position, Quaternion.identity);/*Instantiate(NPCs[NPCIndex], townPortals[portalIndex].transform.position, Quaternion.identity);*/
        StartCoroutine(newNPC.GetComponent<OverWorldNPC>().Spawn());
        newNPC.transform.parent = overworldNPCParent.transform;
        yield return new WaitForSeconds(spawnDelay);
    }
    private int GetPortalIndex() {
        return Random.Range(0, townPortals.Count);
    }
    private int GetNPCIndex() {
        return Random.Range(0, NPCs.Length/*Count*/);
    }
    private void CreateNPCParent()
    {
        overworldNPCParent = GameObject.Find(NPC_PARENT_NAME);
        if(!overworldNPCParent)
        {
            overworldNPCParent = new GameObject(NPC_PARENT_NAME);
        }
    }
    public void AddPortals(TownPortal[] portalsToAdd)
    {
        foreach(TownPortal portal in portalsToAdd)
        {
            townPortals.Add(portal);
        }
    }
}
