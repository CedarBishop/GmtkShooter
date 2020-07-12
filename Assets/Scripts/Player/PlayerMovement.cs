using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public float knockbackTime;

    [HideInInspector]public int inverseControlReferences;
    [HideInInspector]public int moveToAimReferences;


    private PlayerShoot playerShoot;
    private Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector2 direction;
    private float inverseModifier;

    private float knockbackForce;
    private Vector2 knockbackDirection;

    void Start()
    {
        playerShoot = GetComponent<PlayerShoot>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnMove (InputValue value)
    {
        direction = value.Get<Vector2>().normalized;
    }

    void OnPause ()
    {
        UIManager.instance.TogglePause();
    }

    void FixedUpdate()
    {
        if (moveToAimReferences > 0)
        {
            rigidbody.AddForce((playerShoot.gunOriginTransform.right) * moveToAimReferences * 100);
        }
        if (inverseControlReferences > 0)
        {
            inverseModifier = -1;
        }
        else
        {
            inverseModifier = 1;
        }
        rigidbody.velocity = direction * movementSpeed * Time.fixedDeltaTime * inverseModifier;
        animator.SetBool("IsMoving", (Mathf.Abs(rigidbody.velocity.x) > 0.25f || Mathf.Abs(rigidbody.velocity.y) > 0.25f));
        if (rigidbody.velocity.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (rigidbody.velocity.x < 0)
        {
            spriteRenderer.flipX = true;
        }

        if (knockbackForce > 0)
        {
            rigidbody.AddForce(knockbackForce * knockbackDirection);
        }
    }

    public void Knockback(Vector2 direction, float magnitude)
    {
        knockbackDirection = new Vector2(direction.x * -1, direction.y * -1);
        knockbackForce = magnitude;
        StartCoroutine("CoKnockback");
    }

    IEnumerator CoKnockback ()
    {
        yield return new WaitForSeconds(knockbackTime);
        knockbackForce = 0;
    }
}
