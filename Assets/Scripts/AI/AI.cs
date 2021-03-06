﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public LayerMask playerLayer;
    public AI_Bullet bulletPrefab;
    public Transform aimOriginTransform;
    public Transform bulletSpawnPoint;
    public int damage;
    public float movementSpeed;
    public float attackCooldown;
    public float attackDistance;
    public float alertDistance;
    public float patrolTargetDistance;
    public float bulletDeviation;
    public float bulletForce;
    public int score;

    Rigidbody2D rigidbody;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (rigidbody.velocity.y > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (rigidbody.velocity.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }
}
