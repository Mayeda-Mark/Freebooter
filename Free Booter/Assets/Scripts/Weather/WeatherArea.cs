using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherArea : MonoBehaviour
{
    [SerializeField] Collider2D myCollider;
    //[SerializeField] GameObject[] conditions;
    [SerializeField] String[] conditionTags;
    float weatherTimer, currentWindDir, currentWindSpeed;
    bool playing = true;
    bool isWindy = false;
    Weather currentWeather;
    GameObject currentConditions;
    float transitionTimer = 3.0f;
    Pooler pooler;
    IEnumerator Start() {
        pooler = FindObjectOfType<Pooler>();
        do
        {
            //SetCurrentWeather();
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
        //StartCoroutine("Transition");
    }
    private IEnumerator Transition(GameObject weatherCondition)
    {
        yield return new WaitForSeconds(weatherTimer - transitionTimer);
        print("Called transition");
        weatherCondition.GetComponent<StormArea>().Kill();
    }

    private void SetConditions()
    {
        GameObject newCondition = pooler.SpawnFromPool(GetConditionTag(), transform.position, Quaternion.identity);
        newCondition.transform.parent = this.transform;
        StartCoroutine(Transition(newCondition));

        /*if(currentConditions == null)
        {
            currentConditions = Instantiate(conditions[5*//*UnityEngine.Random.Range(0, conditions.Length)*//*], transform.position, Quaternion.identity) as GameObject;
            currentConditions.transform.parent = this.transform;
            currentWindSpeed = GetWindSpeed();
        } else
        {
            //Destroy(currentConditions.gameObject);
            GameObject newConditions = Instantiate(conditions[5*//*UnityEngine.Random.Range(0, conditions.Length)*//*], transform.position, Quaternion.identity) as GameObject;
            currentConditions = null;
            currentConditions = newConditions;
            //newConditions.transform.parent = this.transform;
            //currentWindSpeed = GetWindSpeed();
            //Destroy(newConditions.gameObject, weatherTimer);
        }*/
    }
    private String GetConditionTag()
    {
        int index = UnityEngine.Random.Range(0, conditionTags.Length);
        return conditionTags[index];
    }
    /*private float GetWindSpeed()
    { // REDO THIS
        StormArea childStormArea = GetComponentInChildren<StormArea>();
        if (childStormArea != null)
        {
            isWindy = true;
            return childStormArea.GetWindSpeed();
        }
        else
        {
            return 0;
        }
    }
*/
    private void SetCurrentWeather()
    {
        currentWindDir = UnityEngine.Random.Range(0f, 360f);
        weatherTimer = UnityEngine.Random.Range(10f, 20f);
        print(currentWindDir);
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
        Vector3 dir = Quaternion.AngleAxis(currentWindDir - 270f, Vector3.forward) * Vector3.right;
        rigidbody.AddForce(dir * 50f/*currentWindSpeed*/);
    }
    public float GetWindDir() { return currentWindDir; }
    public float GetWeatherTimer() { return weatherTimer; }
}
