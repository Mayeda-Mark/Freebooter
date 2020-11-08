using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldNPCController : MonoBehaviour
{
    [SerializeField] List<TownPortal> townPortals = default;
    [SerializeField] Sprite[] damageSprites = default;
    [SerializeField] float spawnDistance = 20f;
    [SerializeField] float shipSpeed = 2f;
    [SerializeField] float turnSpeed = 50f;
    [SerializeField] float reloadTime = 1.5f;
    [SerializeField] float sinkTime = 10f;
    [SerializeField] int[] lootArray;
    [SerializeField] int maxLoot, minLoot;
    [SerializeField] string[] projectiles;
    string equippedItem;
    int lootQuantity;
    Item loot;
    int startingHealth;
    TownPortal targetPortal;
    Transform target;
    Rigidbody2D myRigidBody;
    Cannons myCannons;
    CapsuleCollider2D myHullCollider;
    bool shootLeft, attacking, playerInSights, lReload, rReload, lootable = false;
    bool isAlive = true;
    [SerializeField] BoxCollider2D landSpotter, lCollider;
    [SerializeField] CircleCollider2D visualRange, cannonRange;
    [SerializeField] EdgeCollider2D lCannon, rCannon;
    bool underWind = false;
    [SerializeField] Sails mySails;
    Health sailHealth;
    void Start()
    {
        sailHealth = mySails.sailHealth;
        lootQuantity = UnityEngine.Random.Range(minLoot, maxLoot);
        int lootIndex = UnityEngine.Random.Range(0, lootArray.Length);
        loot = FindObjectOfType<ItemDB>().GetItem(lootArray[lootIndex]);
        myCannons = GetComponent<Cannons>();
        myRigidBody = GetComponent<Rigidbody2D>();
        myHullCollider = GetComponent<CapsuleCollider2D>();
        SetTarget();
    }
    void Update()
    {
        if(isAlive) {
            Move(); 
            LookForPlayer();
        } else {
            SinkTimer();
        }
    }
    #region Movement
    /******************MOVING***************************/
    private void SetTarget() {
        targetPortal = RollTarget();
        target = targetPortal.transform;
        if(Vector2.Distance(transform.position, target.position) <= spawnDistance) {
            SetTarget();
        }
    }
    private TownPortal RollTarget() {
        int index = UnityEngine.Random.Range(0, townPortals.Count);
        return townPortals[index];
    }
    private void Move() {
        if(!CanSeeLand() || !GetComponent<OverWorldNPC>().IsVulnerable()){
            if(!attacking) {
                TurnTowardsTarget();
            }
        } if(CanSeeLand() && !CanSeeTown()) {
            TurnAwayFromLand();
        }
        float moveSpeed = shipSpeed * Time.deltaTime;
        float sailHealthSpeedCorrection = (float)sailHealth.GetHealth() / 100;
        transform.position += (transform.up * (moveSpeed * sailHealthSpeedCorrection + (moveSpeed / 3)));
    }
    private void TurnTowardsTarget() {
        Vector2 dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler( 0, 0, angle - 90f);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turnSpeed * Time.deltaTime);
    }
    #endregion
    #region Navigation
    /******************NAVIGATING***************************/
    private bool CanSeeTown() {
        return landSpotter.IsTouchingLayers(LayerMask.GetMask("Portals"));
    }
    private bool CanSeeLand() {
        return landSpotter.IsTouchingLayers(LayerMask.GetMask("Land", "Player", "NPC"));
    }
    private bool LandLeft() {
        return lCollider.IsTouchingLayers(LayerMask.GetMask("Land", "Player", "NPC"));
    }
    private void TurnAwayFromLand() {
        if(LandLeft()) {
            myRigidBody.rotation -= turnSpeed;
        } else {
            myRigidBody.rotation += turnSpeed;
        }
    }
    #endregion
    #region Combat
    /******************ATTACKING***************************/
    private void LookForPlayer() {
        if(GetComponent<OverWorldNPC>().IsNPCAttacker()){
            if(visualRange.IsTouchingLayers(LayerMask.GetMask("Player"))) {
                target = FindObjectOfType<PlayerShipController>().transform;
            } else {
            target = targetPortal.transform;
            }
            EngagePlayer();
        }
    }
    private void EngagePlayer() {
        if(cannonRange.IsTouchingLayers(LayerMask.GetMask("Player"))) {
            attacking = true;
            TurnToShoot();
            FireAtPlayer();
        } else {
            attacking = false;
            RollDirOfAttack();
        }
    }
    private void RollDirOfAttack(){
        int randomizer = UnityEngine.Random.Range(0, 10);
        if(randomizer % 2 == 1) {
            shootLeft = true;
        } else {
            shootLeft = false;
        }
    }
    private void TurnToShoot() {
        Vector2 dir = target.position - transform.position;
        float shootAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if(shootLeft) {
            shootAngle += 150;
        } else {
            shootAngle -= 30;
        }
        Quaternion rotation = Quaternion.Euler( 0, 0, shootAngle);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turnSpeed * Time.deltaTime);
    }
    private void FireAtPlayer() {
        if(lCannon.IsTouchingLayers(LayerMask.GetMask("Player")) && !lReload) {
            playerInSights = true;
            EquipProjectile();
            myCannons.FireLeftCannon(equippedItem);
            StartCoroutine("ReloadLeft");
            lReload = true;
        } else if(rCannon.IsTouchingLayers(LayerMask.GetMask("Player")) &&!rReload) {
            playerInSights = true;
            EquipProjectile();
            myCannons.FireRightCannon(equippedItem);
            StartCoroutine("ReloadRight");
            rReload = true;
        } else {
            playerInSights = false;
        }
    }

    private void EquipProjectile()
    {
        equippedItem = projectiles[UnityEngine.Random.Range(0, projectiles.Length)];
    }

    private IEnumerator ReloadLeft() {
        yield return new WaitForSeconds(reloadTime);
        lReload = false;
    }
    private IEnumerator ReloadRight() {
        yield return new WaitForSeconds(reloadTime);
        rReload = false;
    }
    #endregion
    #region Death
    /******************DEATH***************************/
    public void Death() {
        GetComponent<SpriteRenderer>().sprite = damageSprites[0];
        lootable = true;
        isAlive = false;
        DisableExtraColliders();
        gameObject.layer = 13;
        SinkTimer();
    }
    private void SinkTimer() {
        if(lootable) {
            sinkTime -= Time.deltaTime;
            if(sinkTime <= 0) {
                Destroy(gameObject);
            }
        }
    }
    private void DisableExtraColliders() {
        landSpotter.enabled = false;
        visualRange.enabled = false;
        cannonRange.enabled = false;
        lCollider.enabled = false;
        lCannon.enabled = false;
        rCannon.enabled = false;
        myHullCollider.isTrigger = true;
    }
    #endregion
    public Item GetLoot()        { return loot;         }
    public bool IsLootable()     { return lootable;     }
    public int GetLootQuantity() { return lootQuantity; }
    public void Kill() {
        Destroy(gameObject);
    }
    public void SetUnderWind(bool isUnderWind) => underWind = isUnderWind;
    public bool GetUnderWind() { return underWind; }
}
