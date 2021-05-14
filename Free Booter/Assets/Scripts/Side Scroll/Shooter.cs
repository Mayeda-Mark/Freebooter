using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    Pooler pooler;
    private void Start()
    {
        pooler = FindObjectOfType<Pooler>();
    }
    public void Shoot(Equipment item)
    {
        print(transform.localRotation);
        pooler.SpawnFromPool(item.itemName, transform.position, /*this*/transform.localRotation);
    }
    //public void 
}
