using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weather{
    public int id;
    public string name;
    public float windVelocity;
    public float windDirection;
    public float visibility;

    public Weather(int id, string name, float windVelocity, float windDirection, float visibility) {
        this.id = id;
        this.name = name;
        this.windVelocity = windVelocity;
        this.windDirection = windDirection;
        this.visibility = visibility;
    }
    public Weather(Weather weather) {
        this.id = weather.id;
        this.name = weather.name;
        this.windVelocity = weather.windVelocity;
        this.windDirection = weather.windDirection;
        this.visibility = weather.visibility;
    }
}
