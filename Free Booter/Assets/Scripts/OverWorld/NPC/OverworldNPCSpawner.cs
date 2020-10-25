using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldNPCSpawner : MonoBehaviour
{
    [SerializeField] List<TownPortal> townPortals;
    [SerializeField] List<OverWorldNPC> NPCs;
    [SerializeField] float spawnDelay = 10f;
    [SerializeField] bool spawning = true;
    GameObject overworldNPCParent;
    const string NPC_PARENT_NAME = "Overworld NPCs";
    // Start is called before the first frame update
    IEnumerator Start()
    {
        do{
            yield return StartCoroutine(SpawnShip());
        }
        while(spawning);
    }
    private IEnumerator SpawnShip() {
        CreateNPCParent();
        int protalIndex = GetPortalIndex();
        int NPCIndex = GetNPCIndex();
        var newNPC = Instantiate(NPCs[NPCIndex], townPortals[protalIndex].transform.position, Quaternion.identity);
        StartCoroutine(newNPC.GetComponent<OverWorldNPC>().Spawn());
        newNPC.transform.parent = overworldNPCParent.transform;
        yield return new WaitForSeconds(spawnDelay);
    }
    private int GetPortalIndex() {
        return Random.Range(0, townPortals.Count);
    }
    private int GetNPCIndex() {
        return Random.Range(0, NPCs.Count);
    }
    private void CreateNPCParent()
    {
        overworldNPCParent = GameObject.Find(NPC_PARENT_NAME);
        if(!overworldNPCParent)
        {
            overworldNPCParent = new GameObject(NPC_PARENT_NAME);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
