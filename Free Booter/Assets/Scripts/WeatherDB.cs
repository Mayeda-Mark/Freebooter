
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherDB : MonoBehaviour
{
    [SerializeField] public List<Weather> weathers = new List<Weather>();
    private void Awake() {
        BuildDb();
    }
    public Weather GetWeather(int id) {
        return weathers.Find(weather => weather.id == id);
    }
    public Weather GetWeather(string name) {
        return weathers.Find(weather => weather.name == name);
    }
    //int id, string name, float windVelocity, float windDirection, float visibility
    void BuildDb() {
        weathers = new List<Weather>{
            new Weather(0, "LightWind", Random.Range(0.5f, 2f), Random.Range(0f, 360f), 100f),
            new Weather(1, "HeavyWind", Random.Range(2f, 5f), Random.Range(0f, 360f), 100f),
            new Weather(2, "LightFog", 0, Random.Range(0f, 360f), 80f),
            new Weather(3, "HeavyFog", 0, Random.Range(0f, 360f), 50f),
            new Weather(4, "LightStorm", Random.Range(5f, 7f), Random.Range(0f, 360f), 90f),
            new Weather(5, "HeavyStorm", Random.Range(0.5f, 2f), Random.Range(0f, 360f), 70f),
            new Weather(6, "Sun", 0, 0, 100f),
        };
    }
}
