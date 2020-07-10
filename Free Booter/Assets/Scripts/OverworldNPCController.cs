using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldNPCController : MonoBehaviour
{
    [SerializeField] List<TownPortal> townPortals;
    [SerializeField] float spawnDistance = 20f;
    [SerializeField] float shipSpeed = 2f;
    [SerializeField] GameObject NPC;
    TownPortal target;
    // Start is called before the first frame update
    void Start()
    {
        SetTarget();
    }
    private void SetTarget() {
        target = RollTarget();
        if(Vector2.Distance(transform.position, target.transform.position) < spawnDistance) {
            target = RollTarget();
        }
    }
    private TownPortal RollTarget() {
        int index = Random.Range(0, townPortals.Count);
        return townPortals[index];
    }

    // Update is called once per frame
    void Update()
    {
        TurnTowardsTarget();
        Move();   
    }
    private void TurnTowardsTarget() {
        print("Target: " + target.transform.position);
        print("NPC: " + NPC.transform.position);
        Quaternion rotation = Quaternion.LookRotation(target.transform.position - NPC.transform.position, NPC.transform.TransformDirection(Vector3.up));
        NPC.transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
    }
    private void Move() {
        float moveSpeed = shipSpeed * Time.deltaTime;
        NPC.transform.position += transform.up * moveSpeed;
    }
}
