using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherArea : MonoBehaviour
{
    [SerializeField] Collider2D myCollider;
    [SerializeField] List<int> weatherIds;
    [SerializeField] WeatherDB weatherDb;
    float weatherTimer, currentWindDir, currentWindSpeed;
    int weatherIndex;
    bool playing = true;
    Weather currentWeather;
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
    }

    private void SetCurrentWeather()
    {
        currentWindSpeed = currentWeather.windVelocity;
        currentWindDir = currentWeather.windDirection;
    }

    private void RollWeather() {
        currentWeather = weatherDb.GetWeather(UnityEngine.Random.Range(0, weatherIds.Count));
        weatherTimer = UnityEngine.Random.Range(60f, 180f);
    }
    private void OnTriggerStay2D(Collider2D collision) {
        if(collision.GetComponent<PlayerShipController>()) {
           //Debug.Log("Weather: " + currentWeather.name);
        }
    }
}
