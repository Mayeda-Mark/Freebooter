using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IPooledObject
{
    [SerializeField] float projectileSpeed = 5f;
    [SerializeField] float range;
    [SerializeField] int hullDamage;
    [SerializeField] int sailDamage;
    [SerializeField] int sideScrollDamage;
    [SerializeField] int itemIndex;
    [SerializeField] bool sideScroll;
    float distanceToClearShip = 0.2f;
    bool canDamage = false;
    /*Item*/Equipment item;
    Vector2 lastPosition;
    float distanceTravelled;
    Collider2D myCollider;
    // Start is called before the first frame update
    public void OnObjectSpawn()
    {
        canDamage = false;
        if(!sideScroll)
        {
            item = (Equipment)FindObjectOfType<ItemDB>().GetItem(itemIndex);
        }
        SetStats();
        myCollider = GetComponent<Collider2D>();
        distanceTravelled = 0;
    }
    void Update()
    {
        Move();
    }
    private void SetStats()
    {
        if(!sideScroll)
        {
            hullDamage = item.hullDamage;
            sailDamage = item.sailDamage;
        }
    }

    private void Move()
    {
        lastPosition = transform.position;
        transform.Translate(Vector2.up * projectileSpeed * Time.deltaTime);
        distanceTravelled = CalculateDistanceTravelled();
        if(distanceTravelled >= distanceToClearShip) {
            canDamage = true;
        }
        if(distanceTravelled >= range) {
            DestroyProjectile();
        }
        if(myCollider.IsTouchingLayers(LayerMask.GetMask("Land")) || myCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) {
            DestroyProjectile();
        }
    }
    private float CalculateDistanceTravelled() {
        return Vector2.Distance(lastPosition, transform.position) + distanceTravelled;
    }
    public void DestroyProjectile() {
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D otherCollider) {
        if(canDamage && otherCollider.GetType() == typeof(CapsuleCollider2D)) {
            if(!sideScroll)
            {
                otherCollider.GetComponent<Health>().DealDamage(hullDamage);
                otherCollider.GetComponentInChildren<Sails>().sailHealth.DealDamage(sailDamage);
            } else
            {
                if(otherCollider.GetComponent<SidescrollEnemy>() == null)
                {
                    otherCollider.GetComponent<Health>().DealDamage(sideScrollDamage);
                }
            }
            DestroyProjectile();
        }
    }
}
