using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipController : MonoBehaviour
{
	Rigidbody2D myRigidBody;
    [SerializeField] float spinRate = 0.3f;
    [SerializeField] float fullSail = 3f;
    [SerializeField] float halfSail = 2f;
    [SerializeField] float quarterSail = 1f;
    [SerializeField] float shipSpeed = 0.3f;
    float stopped = 0f;
    int sails = 0;
    float shipRotation;
    float absShipRotation;
    // Start is called before the first frame update
    void Start()
    {
		myRigidBody = GetComponent<Rigidbody2D>();
        shipRotation = myRigidBody.rotation;
        
    }

    // Update is called once per frame
    void Update()
    {
        Turn();
        SetSails();
        Move();
    }
    private void Turn() {
        if(Input.GetKey("a")) {
            myRigidBody.rotation += spinRate;
            shipRotation += spinRate;
        }
        if(Input.GetKey("d")) {
            myRigidBody.rotation -= spinRate;
            shipRotation -= spinRate;
        }
        shipRotation = CorrectRotation(shipRotation);
        absShipRotation = FindAbsRotation(shipRotation);
        print(absShipRotation);
    }
    private float CorrectRotation(float rotation) {
        if(rotation >= 360) {
            rotation -= 360;
        }
        if(rotation <= -360) {
            rotation += 360;
        } return rotation;
    }
    private float FindAbsRotation(float rotation) {
        float absRotation;
        if(rotation >= 0) {
            absRotation = rotation - 360;
        } else {
            absRotation = rotation;
        }
        return Mathf.Abs(absRotation);
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
        float currentx = myRigidBody.transform.position.x;
        float currenty = myRigidBody.transform.position.y;
        Vector2 newPlayerPosition = new Vector2((moveSpeed * Mathf.Sin(absShipRotation)) + currentx, 
        (moveSpeed * Mathf.Cos(absShipRotation)) + currenty);
        myRigidBody.transform.position = newPlayerPosition;
    }
}
