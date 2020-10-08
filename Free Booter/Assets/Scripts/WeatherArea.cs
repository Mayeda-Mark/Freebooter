using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherArea : MonoBehaviour
{
    [SerializeField] Collider2D myCollider;
    [SerializeField] List<int> weatherIds;
    [SerializeField] WeatherDB weatherDb;
    [SerializeField] GameObject[] Conditions;
    float weatherTimer, currentWindDir, currentWindSpeed;
    int weatherIndex;
    bool playing = true;
    Weather currentWeather;
    GameObject currentConditions;
    IEnumerator Start() {
        do
        {
            yield return StartCoroutine("SetWeather");
        }
        while (playing);
    }
    private IEnumerator SetWeather()
    {
        yield return new WaitForSeconds(weatherTimer);
        RollWeather();
        SetCurrentWeather();
        SetConditions();
    }

    private void SetConditions()
    {
        if(currentConditions == null)
        {
            currentConditions = Instantiate(Conditions[0], transform.position, Quaternion.identity) as GameObject;
            currentConditions.transform.parent = this.transform;
        }
    }

    private void SetCurrentWeather()
    {
        currentWindSpeed = currentWeather.windVelocity;
        currentWindDir = currentWeather.windDirection;
    }

    private void RollWeather() {
        currentWeather = weatherDb.GetWeather(/*UnityEngine.Random.Range(0, weatherIds.Count - 1)*/4);
        weatherTimer = UnityEngine.Random.Range(60f, 180f);
    }
    private void OnTriggerStay2D(Collider2D collision) {
        if(collision.GetComponent<PlayerShipController>()) {
           //Debug.Log("Weather: " + currentWeather.name);
        }
    }
    public float GetWindDir()   { return currentWindDir; }
    public float GetWindSpeed() {  return currentWindSpeed;}
}
