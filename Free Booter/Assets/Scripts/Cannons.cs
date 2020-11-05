using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannons : MonoBehaviour
{
    //[SerializeField] Dictionary<string, GameObject> projectiles = new Dictionary<string, GameObject>();
    [SerializeField] GameObject projectile, lCannon, rCannon;
    GameObject projectileParent;
    const string PROJECTILE_PARENT_NAME = "Projectiles";
    float rCannonRotation;
    Pooler pooler;
    void Start()
    {
        pooler = FindObjectOfType<Pooler>();
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
    public void FireRightCannon(string equippedProjectile) {
        GameObject newProjectile = pooler.SpawnFromPool(equippedProjectile, rCannon.transform.position, rCannon.transform.rotation);/*Instantiate(projectile, rCannon.transform.position, rCannon.transform.rotation) as GameObject;*/
        newProjectile.transform.parent = projectileParent.transform;
    }
    public void FireLeftCannon(string equippedProjectile) {
        GameObject newProjectile = pooler.SpawnFromPool(equippedProjectile, lCannon.transform.position, lCannon.transform.rotation)/*Instantiate(projectile, lCannon.transform.position, lCannon.transform.rotation) as GameObject*/;
        newProjectile.transform.parent = projectileParent.transform;
    }
}
