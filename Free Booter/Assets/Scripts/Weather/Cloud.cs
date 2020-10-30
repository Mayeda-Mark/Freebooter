﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour, IPooledObject
{
    [SerializeField] Sprite[] cloudSprites;
    float cloudSpeed;
    StormArea parent;
    SpriteRenderer cloudSprite;
    Animator animator;
    Pooler pooler;
    public void OnObjectSpawn()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("FadeOut", false);
        int index = UnityEngine.Random.Range(0, cloudSprites.Length - 1);
        cloudSprite = GetComponent<SpriteRenderer>();
        cloudSprite.sprite= cloudSprites[index];
    }
    void Update()
    {
        Move();
    }
    public void SetUpParent()
    {
        parent = GetComponentInParent<StormArea>();
        cloudSpeed = UnityEngine.Random.Range(parent.GetWindSpeed() - 0.25f, parent.GetWindSpeed() + 0.25f);
    }
    private void Move()
    {
        transform.Translate(Vector2.up * cloudSpeed * Time.deltaTime);
        var position = transform.position;
        if(position.x < parent.GetMinBounds().x || position.x > parent.GetMaxBounds().x ||
           position.y < parent.GetMinBounds().y || position.y > parent.GetMaxBounds().y)
        {
            Vector3 viewPos = parent.GetCamera().WorldToViewportPoint(position);
            if ((viewPos.x > 1.05 || viewPos.x < -0.05) || (viewPos.y > 1.05 || viewPos.y < -0.05))
            {
                parent.RemoveCLoud();
                this.gameObject.SetActive(false);
            }
        }
    }
    public void Kill()
    {
        animator.SetBool("FadeOut", true);
    }
}