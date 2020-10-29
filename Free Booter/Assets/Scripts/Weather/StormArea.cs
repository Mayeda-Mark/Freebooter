using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormArea : MonoBehaviour, IPooledObject
{
    public float maxWindSpeed = 5f;
    public float minWindSpeed = 2f;
    public int numClouds = 20;
    public float windDir;
    public float windSpeed;
    ParticleSystem fogMachine, rainMaker;
    //public GameObject cloudPrefab;
    GameObject cloudParent;
    const string CLOUD_PARENT_NAME = "Clouds";
    Collider2D collider;
    Vector3 minBounds, maxBounds, cloudPosition;
    Camera cam;
    int activeClouds = 0;
    WeatherArea parent;
    List<GameObject> clouds = new List<GameObject>();
    [SerializeField] String cloudTag;
    Pooler pooler;
    public void OnObjectSpawn() {
        pooler = FindObjectOfType<Pooler>();
        rainMaker = GetComponentInChildren<ParticleSystem>();
        fogMachine = GetComponent<ParticleSystem>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        windSpeed = UnityEngine.Random.Range(minWindSpeed, maxWindSpeed);
        collider = GetComponent<Collider2D>();
        if(collider != null)
        {
            minBounds = collider.bounds.min;
            maxBounds = collider.bounds.max;
        }
    }
    public void SetUpParent()
    {
        parent = GetComponentInParent<WeatherArea>();
        if (rainMaker != null)
        {
            rainMaker.Stop();
            var rainMain = rainMaker.main;
            rainMain.duration = parent.GetWeatherTimer() - 1.5f;
            rainMaker.Play();
        }
        if (fogMachine != null)
        {
            fogMachine.Stop();
            var fogMain = fogMachine.main;
            fogMain.duration = parent.GetWeatherTimer() - 1.5f;
            fogMachine.Play();
        }
        windDir = parent.GetWindDir();
    }
    void Update() {
        SpawnClouds();
    }

    private void SpawnClouds()
    {
        for(int i = activeClouds; i < numClouds; i++)
        {
            print(cloudTag);
            RollCloudPosition();
            GameObject newCloud = pooler.SpawnFromPool(cloudTag, cloudPosition, Quaternion.Euler(new Vector3(0, 0, windDir))); /*Instantiate(cloudPrefab, cloudPosition, Quaternion.Euler(new Vector3(0, 0, windDir))) as GameObject;*/
            newCloud.transform.parent = this.transform;
            newCloud.GetComponent<Cloud>().SetUpParent();
            clouds.Add(newCloud);
            activeClouds++;
        }
    }

    private void RollCloudPosition() 
    {
        cloudPosition = new Vector3(UnityEngine.Random.Range(minBounds.x, maxBounds.x), UnityEngine.Random.Range(minBounds.y, maxBounds.y), 0);
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
    public void Kill()
    {
        if(clouds.Count > 0)
        {
            for(int i = 0; i < clouds.Count; i++)
            {
                clouds[i].GetComponent<Cloud>().Kill();
            }
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
