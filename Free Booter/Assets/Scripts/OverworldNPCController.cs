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
    CapsuleCollider2D MyHullCollider;
    [SerializeField] BoxCollider2D landSpotter, lCollider;
    
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        MyHullCollider = GetComponent<CapsuleCollider2D>();
        SetTarget();
    }
    private void SetTarget() {
        target = RollTarget();
        print(Vector2.Distance(transform.position, target.transform.position));
        if(Vector2.Distance(transform.position, target.transform.position) <= spawnDistance) {
            SetTarget();
        }
    }
    private TownPortal RollTarget() {
        print("rollTarget Called");
        int index = Random.Range(0, townPortals.Count);
        return townPortals[index];
    }

    void Update()
    {
        Move();   
    }
    private void TurnTowardsTarget() {
        Vector2 dir = target.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler( 0, 0, angle - 90f);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turnSpeed * Time.deltaTime);
    }
    private void Move() {
        if(!CanSeeLand() || !GetComponent<OverWorldNPC>().isVulnerable()){
            TurnTowardsTarget();
        } if(CanSeeLand() && !CanSeeTown()) {
            TurnAwayFromLand();
        }
        float moveSpeed = shipSpeed * Time.deltaTime;
        transform.position += transform.up * moveSpeed;
    }
    private bool CanSeeTown() {
        return landSpotter.IsTouchingLayers(LayerMask.GetMask("Portals"));
    }
    private bool CanSeeLand() {
        return landSpotter.IsTouchingLayers(LayerMask.GetMask("Land"));
    }
    private bool LandLeft() {
        return lCollider.IsTouchingLayers(LayerMask.GetMask("Land"));
    }
    private void TurnAwayFromLand() {
        if(LandLeft()) {
            myRigidBody.rotation -= turnSpeed;
        } else {
            myRigidBody.rotation += turnSpeed;
        }
    }
}
