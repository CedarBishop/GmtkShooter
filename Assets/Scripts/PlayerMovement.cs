using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;

    private Rigidbody2D rigidbody;
    private Vector2 direction;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();    
    }

    void OnMove (InputValue value)
    {
        direction = value.Get<Vector2>().normalized;
    }

    void FixedUpdate()
    {
        rigidbody.velocity = direction * movementSpeed * Time.fixedDeltaTime;
    }

    public void Knockback(Vector2 direction, float magnitude)
    {
        Vector2 reverseDirection = new Vector2(direction.x * -1, direction.y * -1);
        rigidbody.AddForce(reverseDirection * magnitude);
    }
}
