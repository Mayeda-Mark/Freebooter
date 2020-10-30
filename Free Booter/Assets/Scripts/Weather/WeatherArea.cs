﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherArea : MonoBehaviour
{
    [SerializeField] Collider2D myCollider;
    [SerializeField] String[] conditionTags;
    float weatherTimer, currentWindDir, currentWindSpeed;
    bool playing = true;
    bool isWindy = false;
    Weather currentWeather;
    GameObject currentConditions;
    float transitionTimer = 3.0f;
    Pooler pooler;
    Dictionary<Rigidbody2D, Vector3> objectsUnderWind = new Dictionary<Rigidbody2D, Vector3>();
    IEnumerator Start() {
        pooler = FindObjectOfType<Pooler>();
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
        EndWind();
        weatherCondition.GetComponent<StormArea>().Kill();
    }

    private IEnumerator EndCondition(GameObject weatherCondition)
    {
        yield return new WaitForSeconds(weatherTimer);
        weatherCondition.SetActive(false);
    }

    private void SetConditions()
    {
        GameObject newCondition = pooler.SpawnFromPool(GetConditionTag(), transform.position, Quaternion.identity);
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
        weatherTimer = UnityEngine.Random.Range(10f, 20f);
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
    }
    private void ApplyForce(Rigidbody2D rigidbody)
    {
        print("Should only be called once");
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
}