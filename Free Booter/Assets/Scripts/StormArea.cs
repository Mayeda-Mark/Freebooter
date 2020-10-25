using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormArea : MonoBehaviour
{
    public float maxWindSpeed = 5f;
    public float minWindSpeed = 2f;
    public int numClouds = 20;
    public float windDir;
    public float windSpeed;
    ParticleSystem rainMaker;
    public GameObject cloudPrefab;
    GameObject cloudParent;
    const string CLOUD_PARENT_NAME = "Clouds";
    Collider2D collider;
    Vector3 minBounds, maxBounds, cloudPosition;
    [SerializeField] Camera cam;
    int activeClouds = 0;
    WeatherArea parent;
    //AreaEffector2D areaEffector;
    void Start() {
        //rainMaker = GetComponent<ParticleSystem>();
        //var em = rainMaker.emission.rate;
        //em.mode = ParticleSystemCurveMode.Constant;
        //em.constantMax = maxIntensity;
        //em.constantMin = minIntensity;
        parent = GetComponentInParent<WeatherArea>();
        windDir = parent.GetWindDir();
        windSpeed = UnityEngine.Random.Range(minWindSpeed, maxWindSpeed);//parent.GetWindSpeed();
        collider = GetComponent<Collider2D>();
        minBounds = collider.bounds.min;
        maxBounds = collider.bounds.max;
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        //areaEffector = GetComponent<AreaEffector2D>();
        //areaEffector.forceAngle = windDir;
        //areaEffector.forceMagnitude = 0.25f/*windSpeed*/;
        /*if(windDir < 90)
        {
            areaEffector.forceAngle = (360f - (windDir - 90f));
        } else
        {
            areaEffector.forceAngle = windDir - 90;
        }*/
        //areaEffector.forceAngle = 0;

        //print(windSpeed);
        //this.transform.rotation.eulerAngles z = windDir;51044
        // FIGURE THIS OUT
    }

    // Update is called once per frame
    void Update() {
        SpawnClouds();
    }

    private void SpawnClouds()
    {
        for(int i = activeClouds; i < numClouds; i++)
        {
            RollCloudPosition();
            GameObject newCloud = Instantiate(cloudPrefab, cloudPosition, Quaternion.Euler(new Vector3(0, 0, windDir))) as GameObject;
            newCloud.transform.parent = this.transform;
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
    public float GetWindDir() { return windDir; }
    public float GetWindSpeed() { return windSpeed; }
}
