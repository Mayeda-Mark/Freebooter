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
    // Start is called before the first frame update
    void Start()
    {
		myRigidBody = GetComponent<Rigidbody2D>();
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
}
