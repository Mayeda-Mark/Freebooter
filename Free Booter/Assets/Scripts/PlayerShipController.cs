using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShipController : MonoBehaviour
{
	Rigidbody2D myRigidBody;
    #region Movement Vars
    [SerializeField] float spinRate = 0.3f;
    [SerializeField] float fullSail = 3f;
    [SerializeField] float halfSail = 2f;
    [SerializeField] float quarterSail = 1f;
    [SerializeField] float shipSpeed = 0.3f;
    #endregion
    [SerializeField] float reloadTime = 0.5f;
    float rReloadTime, lReloadTIme;
    bool rReload, lReload = false;
    [SerializeField] float lootTime = 3f;
    [SerializeField] float lootTimer;
    bool underWind = false;
    bool looting = false;
    ItemDB itemDb;
    Item equippedItem;
    [SerializeField] Cannons myCannons;
    [SerializeField] Text healthText;
    [SerializeField] Text bootyText;
    Inventory shipInventory;
    CapsuleCollider2D myCollider;
    [SerializeField]Sails mySails;
    Health myHealth;
    Health sailHealth;
    float stopped = 0f;
    int sails = 0;
    bool lightFog, heavyFog = false;
    Animator animator;
    private void Awake()
    {
        itemDb = FindObjectOfType<ItemDB>();
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        shipInventory = GetComponent<Inventory>();
        ResetLootTimer();
        myHealth = GetComponent<Health>();
        myCollider = GetComponent<CapsuleCollider2D>();
		myRigidBody = GetComponent<Rigidbody2D>();
        rReloadTime = reloadTime;
        lReloadTIme = reloadTime;
        UpdateBootyDisplay();
        sailHealth = mySails.sailHealth;
    }

    void Update()
    {
        Turn();
        SetSails();
        Move();
        FireCannon();
        UpdateBootyDisplay();
        LootCountdown();
        ReloadCountdowns();
    }
    private void FireCannon()
    {
        if(Input.GetKeyUp(KeyCode.RightArrow)) {
            if(!rReload && shipInventory.CanFireCannon()) {
                myCannons.FireRightCannon(equippedItem.itemName);
                rReload = true;
                shipInventory.DecreaseQuantity(equippedItem.id, 1);
            }
        }
        if(Input.GetKeyUp(KeyCode.LeftArrow)) {
            if(!lReload && shipInventory.CanFireCannon()) {
                myCannons.FireLeftCannon(equippedItem.itemName);
                lReload = true;
                shipInventory.DecreaseQuantity(equippedItem.id, 1);
            }
        }
    }
    private void ReloadCountdowns() {
        if(rReload) {
            rReloadTime -= Time.deltaTime;
            if(rReloadTime <= 0) {
                rReloadTime = reloadTime;
                rReload = false;
            }
        }
        if (lReload) {
            lReloadTIme -= Time.deltaTime;
            if(lReloadTIme <= 0) {
                lReloadTIme = reloadTime;
                lReload = false;
            }
        }
    }
    public void EquipItem(string projectile)
    {
        equippedItem = itemDb.GetItem(projectile);
    }
    private void Turn() {
        if(Input.GetKey("a")) {
            myRigidBody.rotation += spinRate * Time.deltaTime;
        }
        if(Input.GetKey("d")) {
            myRigidBody.rotation -= spinRate * Time.deltaTime;
        }
    }
    private void SetSails() {
        if(Input.GetKeyDown("w")){
            sails ++;
        }
        if(Input.GetKeyDown("s")) {
            sails--;
        }
        sails = Mathf.Clamp(sails, 0, 3);
    }
    private void Move() {
        float moveSpeed = 0f;
        if( sails > 0)
        {
            moveSpeed = ((shipSpeed * sails * GetSailHealth()) + shipSpeed) * Time.deltaTime;
        }
        else
        {

        }
        myRigidBody.transform.position += transform.up * moveSpeed;
    }
    internal void ExitLockedArea()
    {
        myRigidBody.rotation += 180;
    }
    public void MoveFromWind(float windDir, float windSpeed)
    {
        float xComponent = Mathf.Cos(windDir * Mathf.PI / 180) * windSpeed;
        float yComponent = Mathf.Sin(windDir * Mathf.PI / 180) * windSpeed;
        Vector2 windVelocity = new Vector2(xComponent, yComponent);
        myRigidBody.velocity = windVelocity;
    }
    private float GetSailHealth()
    {
        return sailHealth.GetHealth() / 100; //START HERE!
    }
    //private void OnTriggerEnter2D(Collider2D otherCollider) {
    //    OverworldNPCController lootableShip = otherCollider.GetComponent<OverworldNPCController>();
    //    if(lootableShip && lootableShip.IsLootable() && otherCollider is CapsuleCollider2D) {
    //        looting = true;
    //    }
    //}
    private void OnTriggerStay2D(Collider2D otherCollider) {
        OverworldNPCController lootableShip = otherCollider.GetComponent<OverworldNPCController>();
        if (lootableShip && lootableShip.IsLootable() && otherCollider is CapsuleCollider2D)
        {
            looting = true;
        }
        //LootCountdown();
        if(lootTimer <= 0 && otherCollider is CapsuleCollider2D) {
            /*shipInventory.GiveItem(lootableShip.GetLoot().id, lootableShip.GetLootQuantity());*/
                lootableShip.GiveLoot();
                lootableShip.Kill();
                ResetLootTimer();
                looting = false;
            }
    }
    private void LootCountdown() {
        if(looting && myCollider.IsTouchingLayers(LayerMask.GetMask("Loot"))) {
            lootTimer -= Time.deltaTime;
        } else {
            ResetLootTimer();
            looting = false;
        }
    }
    //private void onExitTrigger2D(Collider2D otherCollider) {
    //    looting = false;
    //    ResetLootTimer();
    //}
    private void ResetLootTimer() {
        lootTimer = lootTime;
    }
    private void UpdateBootyDisplay() {
        bootyText.text = "" + shipInventory.GetTotalGold().ToString() + " Gold";
    }
    public void SetUnderWind(bool isUnderWind) => underWind = isUnderWind;
    public bool GetUnderWind() { return underWind; }
    internal void Kill()
    {
        StartCoroutine(LoadDeath());
    }

    IEnumerator LoadDeath()
    {
        yield return new WaitForSeconds(3f);
        FindObjectOfType<LevelLoader>().LoadDeathScreen();
    }

    public void SetWeatherState(string state, bool trueFalse)
    {
        animator.SetBool(state, trueFalse);
    }
}
