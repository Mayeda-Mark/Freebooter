using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject inside, outside, doors, bg = default;
    BoxCollider2D myCollider;
    private bool inDoors, inCooldown = false;
    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Player")) && !inCooldown)
        {
            if(Input.GetButtonDown("Enter") && !inDoors)
            {
                inside.SetActive(true);
                outside.SetActive(false);
                inDoors = true;
                DisappearDoors();
            }
            if(Input.GetButtonDown("Exit") && inDoors)
            {
                inside.SetActive(false);
                outside.SetActive(true);
                inDoors = false;
                ReappearDoors();
            }
        }
    }
    private void DisappearDoors()
    {
        var doorsAlpha = doors.GetComponent<SpriteRenderer>().color; 
        doorsAlpha.a = 0;
        doors.GetComponent<SpriteRenderer>().color = doorsAlpha;
    }
    private void ReappearDoors()
    {
        var doorsAlpha = doors.GetComponent<SpriteRenderer>().color;
        doorsAlpha.a = 1;
        doors.GetComponent<SpriteRenderer>().color = doorsAlpha;
    }
}
