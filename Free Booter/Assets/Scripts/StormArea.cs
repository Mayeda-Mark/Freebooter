using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormArea : MonoBehaviour
{
    public int maxIntensity = 300;
    public int minIntensity = 200;
    public float windDir;
    public float windIntensity;
    ParticleSystem rainMaker;
    void Start() {
        rainMaker = GetComponent<ParticleSystem>();
        var em = rainMaker.emission.rate;
        em.mode = ParticleSystemCurveMode.Constant;
        em.constantMax = maxIntensity;
        em.constantMin = minIntensity;
        windDir = UnityEngine.Random.Range(0f, 306f);
        //this.transform.rotation.eulerAngles z = windDir;51044
    }

    // Update is called once per frame
    void Update() {
    }
}
