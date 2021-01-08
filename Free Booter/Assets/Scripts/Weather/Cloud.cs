using System;
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
    //[SerializeField]Collider2D collider;
    bool isInBounds = true;
    Vector3 minBounds, maxBounds;
    public void OnObjectSpawn()
    {
        isInBounds = true;
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
        if (!isInBounds)
        {
            Vector3 viewPos = parent.GetCamera().WorldToViewportPoint(transform.TransformPoint(position));
            if ((viewPos.x > 1.05 || viewPos.x < -0.05) || (viewPos.y > 1.05 || viewPos.y < -0.05))
            {
                parent.RemoveCLoud();
                /*this.*/gameObject.SetActive(false);
            }
        }
    }
    public void Kill()
    {
        animator.SetBool("FadeOut", true);
    }
    public void NotInBounds()
    {
        isInBounds = false;
    }
}
