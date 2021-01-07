using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherArea : MonoBehaviour, IPooledObject
{
    [SerializeField] Collider2D myCollider;
    [SerializeField] String[] conditionTags;
    float weatherTimer, currentWindDir, currentWindSpeed;
    bool playing = true;
    bool isWindy, playerInArea, tansitioningFromFog = false;
    public bool hasStarted = false;
    GameObject currentConditions;
    float transitionTimer = 10.0f;
    Pooler pooler;
    string currentConditionsName;
    Dictionary<Rigidbody2D, Vector3> objectsUnderWind = new Dictionary<Rigidbody2D, Vector3>();
    /*private void*//*private IEnumerator Start() {
        pooler = FindObjectOfType<Pooler>();
        //StartCoroutine(StartWeather());
        do
        {
            yield return StartCoroutine("SetWeather");
        }
        while (playing);
    }*/
    private void Awake()
    {
        pooler = FindObjectOfType<Pooler>();
    }
    public void OnObjectSpawn()
    {
        StartCoroutine(StartWeather());
    }
    public IEnumerator StartWeather()
    {
        hasStarted = true;
        do
        {
            yield return StartCoroutine("SetWeather");
        }
        while (playing);
    }

    private IEnumerator SetWeather()
    {
        yield return new WaitForSeconds(weatherTimer);
        SetCurrentWeather();
        SetConditions();
    }
    private IEnumerator Transition(GameObject weatherCondition)
    {
        yield return new WaitForSeconds(weatherTimer - transitionTimer);
        if(currentConditionsName == "Heavy Fog" || currentConditionsName == "Light Fog")
        {
            tansitioningFromFog = true;
        }
        ExitWind();
        weatherCondition.GetComponent<StormArea>().Kill();
    }

    private IEnumerator EndCondition(GameObject weatherCondition)
    {
        yield return new WaitForSeconds(weatherTimer);
        weatherCondition.SetActive(false);
    }

    private void SetConditions()
    {
        tansitioningFromFog = false;
        currentConditionsName = GetConditionTag();
        GameObject newCondition = pooler.SpawnFromPool(currentConditionsName, transform.position, Quaternion.identity);
        newCondition.transform.parent = this.transform;
        newCondition.GetComponent<StormArea>().SetUpParent();
        currentWindSpeed = newCondition.GetComponent<StormArea>().GetWindSpeed();
        if (currentWindSpeed > 0)
        {
            isWindy = true;
        }
        StartCoroutine(Transition(newCondition));
        StartCoroutine(EndCondition(newCondition));
    }
    private String GetConditionTag()
    {
        int index = UnityEngine.Random.Range(0, conditionTags.Length);
        return conditionTags[index];
    }
    private void SetCurrentWeather()
    {
        currentWindDir = UnityEngine.Random.Range(0f, 360f);
        weatherTimer = UnityEngine.Random.Range(120f, 180f); // WEATHER TIMER HERE!!!
    }
    private void OnTriggerStay2D(Collider2D collision) {
        var player = collision.GetComponent<PlayerShipController>();
        if (player && !player.GetUnderWind())
        {
            if (isWindy)
            {
                ApplyForce(player.GetComponentInParent<Rigidbody2D>());
                player.SetUnderWind(true);
            }
        }
        if (player)
        {
            playerInArea = true;
            if ((currentConditionsName == "Heavy Fog" && !tansitioningFromFog) || (currentConditionsName == "Light Fog" && !tansitioningFromFog))
            {
                player.SetWeatherState(currentConditionsName, true);
            } else if (tansitioningFromFog)
            {
                player.SetWeatherState(currentConditionsName, false);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerShipController>();
        if (player && !player.GetUnderWind())
        {
            if (isWindy)
            {
                ApplyForce(player.GetComponentInParent<Rigidbody2D>());
                player.SetUnderWind(true);
            }
        }
        if(player)
        {
            playerInArea = true;
            if (currentConditionsName == "Heavy Fog" || currentConditionsName == "Light Fog")
            {
                player.SetWeatherState(currentConditionsName, true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerShipController>();
        if(player)
        {
            playerInArea = false;
            ExitWind();
            if(currentConditionsName == "Heavy Fog" || currentConditionsName == "Light Fog")
            {
                player.SetWeatherState(currentConditionsName, false);
            }
        }
    }

    private void ExitWind()
    {
        foreach (Rigidbody2D rigidbody in objectsUnderWind.Keys)
        {
            rigidbody.AddForce(-(objectsUnderWind[rigidbody] * (currentWindSpeed * 20)));
            PlayerShipController player = rigidbody.GetComponent<PlayerShipController>();
            if (player != null)
            {
                player.SetUnderWind(false);
            }
        }
        objectsUnderWind.Clear();
    }

    private void ApplyForce(Rigidbody2D rigidbody)
    {
        Vector3 dir = Quaternion.AngleAxis(currentWindDir - 270f, Vector3.forward) * Vector3.right;
        rigidbody.AddForce(dir * (currentWindSpeed * 20));
        bool foundObject = false;
        foreach(Rigidbody2D boat in objectsUnderWind.Keys)
        {
            foundObject = true;
        }
        if(!foundObject)
        {
            objectsUnderWind.Add(rigidbody, dir);
        }
    }
    private void EndWind()
    {
        isWindy = false;
        foreach (Rigidbody2D rigidbody in objectsUnderWind.Keys)
        {
            rigidbody.AddForce(-(objectsUnderWind[rigidbody] * (currentWindSpeed * 20)));
            PlayerShipController player = rigidbody.GetComponent<PlayerShipController>();
            if (player != null)
            {
                player.SetUnderWind(false);
            }
        }
        objectsUnderWind.Clear();
    }
    public float GetWindDir() { return currentWindDir; }
    public float GetWeatherTimer() { return weatherTimer; }
    public bool GetPlayerInArea() { return playerInArea; }
}
