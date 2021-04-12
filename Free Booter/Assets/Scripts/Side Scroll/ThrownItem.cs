using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownItem : MonoBehaviour, IPooledObject
{
    public int itemIndex;
    private int damage;
    private int range;
    SidescrollItem ssItem;
    Rigidbody2D myRigidBody;
    Item item;
    // Start is called before the first frame update
    void Start()
    {
    }
    public void OnObjectSpawn()
    {
        item = FindObjectOfType<ItemDB>().GetItem(itemIndex);
        //print("I'm here! " + item.stats["SidescrollIndex"]);
        ssItem = FindObjectOfType<SidescrollItemDB>().GetSidescrollItem(item.stats["SidescrollIndex"]);
        myRigidBody = GetComponent<Rigidbody2D>();
        myRigidBody.velocity = new Vector2(0, 0);
        //print(ssItem.stats["Range"]);
        //myRigidBody.AddForce(new Vector2(ssItem.stats["Range"] * 10, 0));
        //myRigidBody.AddForce(myRigidBody.GetRelativeVector(Vector2.right * ssItem.stats["Range"] * 100));
        myRigidBody.velocity += (myRigidBody.GetRelativeVector(Vector2.up * ssItem.stats["Range"]));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
