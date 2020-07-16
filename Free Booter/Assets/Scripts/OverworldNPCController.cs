using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldNPCController : MonoBehaviour
{
    [SerializeField] List<TownPortal> townPortals;
    [SerializeField] float spawnDistance = 20f;
    [SerializeField] float shipSpeed = 2f;
    [SerializeField] float turnSpeed = 50f;
    [SerializeField] GameObject NPC;
    TownPortal target;
    Rigidbody2D myRigidBody;
    
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        SetTarget();
    }
    private void SetTarget() {
        target = RollTarget();
        if(Vector2.Distance(transform.position, target.transform.position) <= spawnDistance) {
            target = RollTarget();
        }
    }
    private TownPortal RollTarget() {
        int index = Random.Range(0, townPortals.Count);
        return townPortals[index];
    }

    void Update()
    {
        TurnTowardsTarget();
        Move();   
    }
    private void TurnTowardsTarget() {
        Vector2 dir = target.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler( 0, 0, angle - 90f);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turnSpeed * Time.deltaTime);
    }
    private void Move() {
        float moveSpeed = shipSpeed * Time.deltaTime;
        transform.position += transform.up * moveSpeed;
    }
}
