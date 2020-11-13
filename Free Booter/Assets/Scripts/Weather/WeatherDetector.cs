using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherDetector : MonoBehaviour
{
    [SerializeField] GameObject weatherArea;
    WeatherArea weather;
    void Start()
    {
        weather = weatherArea.GetComponent<WeatherArea>();
        //weatherArea.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerShipController>();
        if(player != null)
        {
            weatherArea.gameObject.SetActive(true);
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
        }
    }
}
