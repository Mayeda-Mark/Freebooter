﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float projectileSpeed = 5f;
    [SerializeField] float range = 10f;
    [SerializeField] float damage = 10f;
    Vector2 lastPosition;
    float distanceTravelled;
    Collider2D myCollider;
    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        lastPosition = transform.position;
        transform.Translate(Vector2.up * projectileSpeed * Time.deltaTime);
        distanceTravelled = CalculateDistanceTravelled();
        if(distanceTravelled >= range) {
            DestroyProjectile();
        }
    }
    private float CalculateDistanceTravelled() {
        return Vector2.Distance(lastPosition, transform.position) + distanceTravelled;
    }
    public void DestroyProjectile() {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider otherCollider) {
        if(otherCollider.GetType() == typeof(CapsuleCollider2D)) {
            otherCollider.GetComponent<Health>().DealDamage(damage);
            DestroyProjectile();
        }
        if(myCollider.IsTouchingLayers(LayerMask.GetMask("Land"))) {
            DestroyProjectile();
        }
    }
}
