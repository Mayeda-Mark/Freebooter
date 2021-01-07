using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherDetector : MonoBehaviour
{
    /*[SerializeField] GameObject weatherArea;
    WeatherArea weather;*/
    GameObject weatherArea;
    Pooler pooler;
    bool hasWeather = false;
    void Start()
    {
        //weather = weatherArea.GetComponent<WeatherArea>();
        //weatherArea.gameObject.SetActive(false);
        pooler = FindObjectOfType<Pooler>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerShipController>();
        if(player != null &&!hasWeather)
        {
            weatherArea = pooler.SpawnFromPool("Weather Area", transform.position, Quaternion.identity);
            //weatherArea.gameObject.SetActive(true);
            weatherArea.GetComponent<WeatherArea>().StartWeather();
            weatherArea.transform.parent = /*this.*/transform.parent;
            hasWeather = true;
            /*if(!weather.hasStarted)
            {
                StartCoroutine(weather.StartWeather());
            }*/
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerShipController>();
        if (player != null)
        {
            weatherArea.gameObject.SetActive(false);
            hasWeather = false;
        }
    }
}
