using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherArea : MonoBehaviour
{
    [SerializeField] Collider2D myCollider;
    [SerializeField] List<int> weatherIds;
    [SerializeField] WeatherDB weatherDb;
    [SerializeField] GameObject[] conditions;
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
        //weatherTimer = UnityEngine.Random.Range(10f, 20f);
        yield return new WaitForSeconds(weatherTimer);
        RollWeather();
        SetCurrentWeather();
        SetConditions();
    }

    private void SetConditions()
    {
        if(currentConditions == null)
        {
            currentConditions = Instantiate(conditions[currentWeather.id], transform.position, Quaternion.identity) as GameObject;
            currentConditions.transform.parent = this.transform;
        } else
        {
            Destroy(currentConditions.gameObject);
            GameObject newConditions = Instantiate(conditions[currentWeather.id], transform.position, Quaternion.identity) as GameObject;
            newConditions.transform.parent = this.transform;
            Destroy(newConditions.gameObject, weatherTimer);
        }
    }

    private void SetCurrentWeather()
    {
        currentWindSpeed = currentWeather.windVelocity;
        currentWindDir = currentWeather.windDirection;
        print("Wind Direction " + currentWeather.windDirection);
        //print("Weather: " + currentWeather.name);
    }

    private void RollWeather() {
        currentWeather = weatherDb.GetWeather(UnityEngine.Random.Range(0, weatherIds.Count));
        weatherTimer = UnityEngine.Random.Range(10f, 20f);
    }
    private void OnTriggerStay2D(Collider2D collision) {
        if(collision.GetComponent<PlayerShipController>()) {
           //Debug.Log("Weather: " + currentWeather.name);
        }
    }
    public float GetWindDir()   { return currentWindDir; }
    public float GetWindSpeed() {  return currentWindSpeed;}
}
