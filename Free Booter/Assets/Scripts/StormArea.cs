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
    void Start() {
        rainMaker = GetComponent<ParticleSystem>();
        var em = rainMaker.emission.rate;
        em.mode = ParticleSystemCurveMode.Constant;
        em.constantMax = maxIntensity;
        em.constantMin = minIntensity;
        windDir = UnityEngine.Random.Range(0f, 306f);
        //this.transform.rotation.eulerAngles z = windDir;51044
    }

    // Update is called once per frame
    void Update() {
        SpawnClouds();
    }

    private void SpawnClouds()
    {
        CreateCloudParent();
        for(int i = cloudParent.transform.childCount; i < numClouds; i++)
        {
            Transform cloudPosition = RollCloudPosition();
        }
    }

    private Transform RollCloudPosition() // START HERE - GET A POSITION WITHIN THE STORM AREA THAT ISN'T WITHIN THE CAMERA'S FIELD OF VIEW
    {
        throw new NotImplementedException();
    }

    private void CreateCloudParent()
    {
        cloudParent = GameObject.Find(CLOUD_PARENT_NAME);
        if(!cloudParent)
        {
            cloudParent = new GameObject(CLOUD_PARENT_NAME);
        }
    }
}
