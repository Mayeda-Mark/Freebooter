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
        myRigidBody = GetComponent<Rigidbody2D>();
        item = FindObjectOfType<ItemDB>().GetItem(itemIndex);
        ssItem = FindObjectOfType<SidescrollItemDB>().GetSidescrollItem(item.stats["SidescrollIndex"]);
    }
    public void OnObjectSpawn()
    {
        myRigidBody.AddForce(new Vector2(0, ssItem.stats["Range"]));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
