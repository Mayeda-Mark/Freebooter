using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannons : MonoBehaviour
{
    [SerializeField] GameObject projectile, lCannon, rCannon;
    GameObject projectileParent;
    const string PROJECTILE_PARENT_NAME = "Projectiles";
    // Start is called before the first frame update
    float rCannonRotation;
    void Start()
    {
        CreateProjectileParent();
    }
    private void CreateProjectileParent()
    {
        projectileParent = GameObject.Find(PROJECTILE_PARENT_NAME);
        if (!projectileParent)
        {
            projectileParent = new GameObject(PROJECTILE_PARENT_NAME);
        }
    }

    // Update is called once per frame
    void Update()
    {
        FireCannon();
    }

    private void FireCannon()
    {
        if(Input.GetKeyUp(KeyCode.RightArrow)) {
            print("Fire right cannon!");
            GameObject newProjectile = Instantiate(projectile, rCannon.transform.position, rCannon.transform.rotation) as GameObject;
            newProjectile.transform.parent = projectileParent.transform;
        }
        if(Input.GetKeyUp(KeyCode.LeftArrow)) {
            print("Fire right cannon!");
            GameObject newProjectile = Instantiate(projectile, lCannon.transform.position, lCannon.transform.rotation) as GameObject;
            newProjectile.transform.parent = projectileParent.transform;
        }
    }
}
