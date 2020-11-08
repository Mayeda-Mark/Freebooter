using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sails : MonoBehaviour
{
    public Health sailHealth;
    private void Awake()
    {
        sailHealth = GetComponent<Health>();
        
    }
    void Start()
    {
    }
}
