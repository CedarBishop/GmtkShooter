using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    
    [HideInInspector]public int inverseControlReferences;
    [HideInInspector]public int moveToAimReferences;

    private PlayerShoot playerShoot;
    private Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector2 direction;
    private float inverseModifier;


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

    void FixedUpdate()
    {
        if (moveToAimReferences > 0)
        {
            direction = playerShoot.gunOriginTransform.right;
        }
        if (inverseControlReferences > 0 && playerShoot.isHoldingShootButton)
        {
            inverseModifier = -1;
        }
        else
        {
            inverseModifier = 1;
        }
        rigidbody.velocity = direction * movementSpeed * Time.fixedDeltaTime * inverseModifier;
        animator.SetBool("IsMoving", (Mathf.Abs(rigidbody.velocity.x) > 0.25f || Mathf.Abs(rigidbody.velocity.y) > 0.25f));
        if (rigidbody.velocity.y > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (rigidbody.velocity.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    public void Knockback(Vector2 direction, float magnitude)
    {
        Vector2 reverseDirection = new Vector2(direction.x * -1, direction.y * -1);
        rigidbody.AddForce(reverseDirection * magnitude);
    }
}
