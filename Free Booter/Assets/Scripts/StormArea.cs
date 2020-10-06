using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormArea : MonoBehaviour
{
    public int maxIntensity = 300;
    public int minIntensity = 200;
    public int numClouds = 20;
    public float windDir;
    public float windIntensity;
    ParticleSystem rainMaker;
    public GameObject cloudPrefab;
    GameObject cloudParent;
    const string CLOUD_PARENT_NAME = "Clouds";
    Collider2D collider;
    Vector3 minBounds, maxBounds, cloudPosition;
    [SerializeField] Camera cam;
    int activeClouds = 0;
    void Start() {
        rainMaker = GetComponent<ParticleSystem>();
        var em = rainMaker.emission.rate;
        em.mode = ParticleSystemCurveMode.Constant;
        em.constantMax = maxIntensity;
        em.constantMin = minIntensity;
        collider = GetComponent<Collider2D>();
        windDir = UnityEngine.Random.Range(0f, 306f);
        minBounds = collider.bounds.min;
        maxBounds = collider.bounds.max;
        //this.transform.rotation.eulerAngles z = windDir;51044
    }

    // Update is called once per frame
    void Update() {
        SpawnClouds();
    }

    private void SpawnClouds()
    {
        //CreateCloudParent();
        for(int i = activeClouds/*cloudParent.transform.childCount*/; i < numClouds; i++)
        {
            RollCloudPosition();
            GameObject newCloud = Instantiate(cloudPrefab, cloudPosition, Quaternion.identity) as GameObject;
            newCloud.transform.parent = this.transform;
            //activeClouds.Add(newCloud);
            activeClouds++;
        }
    }

    private void RollCloudPosition() 
    {
        cloudPosition = new Vector2(UnityEngine.Random.Range(minBounds.x, maxBounds.x), UnityEngine.Random.Range(minBounds.y, maxBounds.y));
        Vector3 viewPos = cam.WorldToViewportPoint(cloudPosition);
        if(viewPos.x < 1.05 && viewPos.x > -0.05 && viewPos.y < 1.05 && viewPos.y > -0.05)
        {
            RollCloudPosition(); 
            return;
        }
    }

    private void CreateCloudParent()
    {
        cloudParent = GameObject.Find(CLOUD_PARENT_NAME);
        if(!cloudParent)
        {
            cloudParent = new GameObject(CLOUD_PARENT_NAME);
        }
    }
    public Vector3 GetMinBounds() { return minBounds; }
    public Vector3 GetMaxBounds() { return maxBounds; }
    public Camera GetCamera()     { return cam; }
    public void RemoveCLoud()
    {
        activeClouds--;
    }
}
