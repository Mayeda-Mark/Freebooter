﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidescrollController : MonoBehaviour
{
    private void Awake()
    {
        Physics2D.gravity = new Vector2(0, -10);
        
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
