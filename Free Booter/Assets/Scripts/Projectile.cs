using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float projectileSpeed = 5f;
    [SerializeField] float range = 10f;
    Vector2 lastPosition;
    float distanceTravelled;
    // Start is called before the first frame update
    void Start()
    {
        
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
}
