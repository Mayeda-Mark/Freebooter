using System;
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
            //CheckForPlayer();
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
            currentConditions = Instantiate(conditions[5/*UnityEngine.Random.Range(0, conditions.Length)*/], transform.position, Quaternion.identity) as GameObject;
            currentConditions.transform.parent = this.transform;
            //currentWindSpeed = GetWindSpeed();
        } else
        {
            Destroy(currentConditions.gameObject);
            GameObject newConditions = Instantiate(conditions[5/*UnityEngine.Random.Range(0, conditions.Length)*/], transform.position, Quaternion.identity) as GameObject;
            newConditions.transform.parent = this.transform;
            Destroy(newConditions.gameObject, weatherTimer);
        }
    }

   /* private float GetWindSpeed()
    {
        StormArea childStormArea = GetComponentInChildren<StormArea>();
        if(childStormArea != null)
        {
            return childStormArea.GetWindSpeed();
        }
        else
        {
            return 0;
        }
    }*/

    private void SetCurrentWeather()
    {
        currentWindDir = UnityEngine.Random.Range(0f, 360f);
        weatherTimer = UnityEngine.Random.Range(10f, 20f);
    }
    private void OnTriggerStay2D(Collider2D collision) {
        var player = collision.GetComponent<PlayerShipController>();
        if (player && !player.GetUnderWind())
        {
            //player.MoveFromWind(currentWindDir, 2f);
            ApplyForce(player.GetComponentInParent<Rigidbody2D>());
            player.SetUnderWind(true);
            //player.GetComponent<Rigidbody2D>().AddForce()
        }
    }
    private void ApplyForce(Rigidbody2D rigidbody)
    {
        print("Should just get called once");
        /* float slope = Mathf.Tan(currentWindDir);
         float xSlope = slope; //FIGURE OUT HOW TO ADD FORCE
         Vector3 dir = Quaternion.AngleAxis(currentWindDir, Vector3.up) * Vector3.right;
         rigidbody.AddForce(dir * 5f*//*currentWindSpeed*//*);*/

        //print(currentWindSpeed);
        float xComponent = Mathf.Cos(currentWindDir * Mathf.PI / 360) * 2f;//currentWindSpeed;
        float yComponent = Mathf.Sin(currentWindDir * Mathf.PI / 360) * 2f;//currentWindSpeed;
        rigidbody.AddForce(new Vector3(xComponent, yComponent, 0));
        /* if (rigidbody.velocity.y > 0.2f)
         {
             rigidbody.velocity.y = 0.2f;
         }*/
    }
    public float GetWindDir()   { return currentWindDir; }
}
