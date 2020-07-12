using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Trident : MonoBehaviour
{
    public Transform aimOrigin;
    public int damage;

    private PlayerHealth player;

    private void Start()
    {
        player = FindObjectOfType<PlayerHealth>();
    }

    private void FixedUpdate()
    {
        if (player == null)
        {
            return;
        }
        Vector2 direction = (player.transform.position - aimOrigin.position).normalized;
        aimOrigin.right = direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerHealth>())
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}
