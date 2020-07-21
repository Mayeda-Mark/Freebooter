using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShipController : MonoBehaviour
{
	Rigidbody2D myRigidBody;
    [SerializeField] float spinRate = 0.3f;
    [SerializeField] float fullSail = 3f;
    [SerializeField] float halfSail = 2f;
    [SerializeField] float quarterSail = 1f;
    [SerializeField] float shipSpeed = 0.3f;
    [SerializeField] Cannons myCannons;
    [SerializeField] Text healthText;
    [SerializeField] Text bootyText;
    CapsuleCollider2D myCollider;
    Health myHealth;
    float stopped = 0f;
    int sails = 0;
    int booty = 0;
    // Start is called before the first frame update
    void Start()
    {
        myHealth = GetComponent<Health>();
        myCollider = GetComponent<CapsuleCollider2D>();
		myRigidBody = GetComponent<Rigidbody2D>();
        //myCannons = GetComponent<Cannons>();
        UpdateBootyDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        Turn();
        SetSails();
        Move();
        FireCannon();
        UpdateHealthDisplay();
    }
    private void FireCannon()
    {
        if(Input.GetKeyUp(KeyCode.RightArrow)) {
            myCannons.FireRightCannon();
        }
        if(Input.GetKeyUp(KeyCode.LeftArrow)) {
            myCannons.FireLeftCannon();
        }
    }
    private void Turn() {
        if(Input.GetKey("a")) {
            myRigidBody.rotation += spinRate;
        }
        if(Input.GetKey("d")) {
            myRigidBody.rotation -= spinRate;
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
        float moveSpeed = (shipSpeed * sails) * Time.deltaTime;
        myRigidBody.transform.position += transform.up * moveSpeed;
    }
    private void OnTriggerEnter2D(Collider2D otherCollider) {
        OverworldNPCController lootableShip = otherCollider.GetComponent<OverworldNPCController>();
        if(lootableShip && lootableShip.IsLootable()) {
            booty += lootableShip.GetLoot();
            UpdateBootyDisplay();
            lootableShip.Kill();
        }
    }
    private void UpdateHealthDisplay() {
        healthText.text = myHealth.GetHealth().ToString();
    }
    private void UpdateBootyDisplay() {
        bootyText.text = booty.ToString();
    }
}
