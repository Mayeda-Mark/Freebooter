using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldNPCSpawner : MonoBehaviour
{
    [SerializeField] List<TownPortal> townPortals;
    [SerializeField] List<OverWorldNPC> NPCs;
    [SerializeField] float spawnDelay = 10f;
    [SerializeField] bool spawning = true;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        do{
            yield return StartCoroutine(SpawnShip());
        }
        while(spawning);
    }
    private IEnumerator SpawnShip() {
        int protalIndex = GetPortalIndex();
        int NPCIndex = GetNPCIndex();
        var newNPC = Instantiate(NPCs[NPCIndex], townPortals[protalIndex].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(spawnDelay);
    }
    private int GetPortalIndex() {
        return Random.Range(0, townPortals.Count);
    }
    private int GetNPCIndex() {
        return Random.Range(0, NPCs.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
