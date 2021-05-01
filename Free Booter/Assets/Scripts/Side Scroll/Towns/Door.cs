using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject inside, outside, doors, bg = default;
    BoxCollider2D myCollider;
    private bool inDoors, inCooldown = false;
    SpriteRenderer mySprite;
    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<BoxCollider2D>();
        mySprite = GetComponent<SpriteRenderer>();
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
        //ANIMATION TRIGGERS GO HERE
        var doorsAlpha = doors.GetComponent<SpriteRenderer>().color; 
        doorsAlpha.a = 0;
        doors.GetComponent<SpriteRenderer>().color = doorsAlpha;
        var frameAlpha = mySprite.color;
        frameAlpha.a = 0;
        mySprite.color = frameAlpha;
    }
    private void ReappearDoors()
    {
        //ANIMATION TRIGGERS GO HERE
        var doorsAlpha = doors.GetComponent<SpriteRenderer>().color;
        doorsAlpha.a = 1;
        doors.GetComponent<SpriteRenderer>().color = doorsAlpha;
        var frameAlpha = mySprite.color;
        frameAlpha.a = 1;
        mySprite.color = frameAlpha;
    }
}
