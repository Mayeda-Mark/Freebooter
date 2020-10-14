﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherArea : MonoBehaviour
{
    [SerializeField] Collider2D myCollider;
    [SerializeField] GameObject[] conditions;
    float weatherTimer, currentWindDir, currentWindSpeed;
    bool playing = true;
    Weather currentWeather;
    GameObject currentConditions;
    IEnumerator Start() {
        do
        {
            CheckForPlayer();
            yield return StartCoroutine("SetWeather");
        }
        while (playing);
    }

    /*private void CheckForPlayer()
    {
        if(myCollider.IsTouchingLayers(LayerMask.GetMask("Pla")))
    }*/

    private IEnumerator SetWeather()
    {
        yield return new WaitForSeconds(weatherTimer);
        SetCurrentWeather();
        SetConditions();
    }

    private void SetConditions()
    {
        if(currentConditions == null)
        {
            currentConditions = Instantiate(conditions[UnityEngine.Random.Range(0, conditions.Length)], transform.position, Quaternion.identity) as GameObject;
            currentConditions.transform.parent = this.transform;
        } else
        {
            Destroy(currentConditions.gameObject);
            GameObject newConditions = Instantiate(conditions[UnityEngine.Random.Range(0, conditions.Length)], transform.position, Quaternion.identity) as GameObject;
            newConditions.transform.parent = this.transform;
            Destroy(newConditions.gameObject, weatherTimer);
        }
    }

    private void SetCurrentWeather()
    {
        currentWindDir = UnityEngine.Random.Range(0f, 360f);
        weatherTimer = UnityEngine.Random.Range(10f, 20f);
    }
    private void OnTriggerStay2D(Collider2D collision) {
        var player = collision.GetComponent<PlayerShipController>();
        if(player) {
            ApplyForce(player.GetComponentInParent<Rigidbody2D>());
            //player.GetComponent<Rigidbody2D>().AddForce()
        }
    }
    private void ApplyForce(Rigidbody2D rigidbody) {
        float slope = Mathf.Tan(currentWindDir);
        float xSlope = slope; //FIGURE OUT HOW TO ADD FORCE
    }
    public float GetWindDir()   { return currentWindDir; }
}
