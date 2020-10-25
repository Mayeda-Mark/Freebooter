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
    ParticleSystem fogMachine;
    public GameObject cloudPrefab;
    GameObject cloudParent;
    const string CLOUD_PARENT_NAME = "Clouds";
    Collider2D collider;
    Vector3 minBounds, maxBounds, cloudPosition;
    Camera cam;
    int activeClouds = 0;
    WeatherArea parent;
    List<GameObject> clouds = new List<GameObject>();
    void Start() {
        parent = GetComponentInParent<WeatherArea>();
        windDir = parent.GetWindDir();
        windSpeed = UnityEngine.Random.Range(minWindSpeed, maxWindSpeed);
        collider = GetComponent<Collider2D>();
        minBounds = collider.bounds.min;
        maxBounds = collider.bounds.max;
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
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
            clouds.Add(newCloud);
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
    public void Kill()
    {
        fogMachine = GetComponent<ParticleSystem>();
        if(fogMachine !=null)
        {
            StartCoroutine("FadeOut");
        } else
        {
            foreach(GameObject cloud in clouds)
            {
                cloud.GetComponent<Cloud>().Kill();
            }
        }
    }
    IEnumerator FadeOut(float aValue, float aTime)
    {
        foreach(GameObject cloudObj in clouds)
        {
            SpriteRenderer cloudSprite = cloudObj.GetComponent<SpriteRenderer>() ;
        }
        yield return null;
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
