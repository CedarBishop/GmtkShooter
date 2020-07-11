using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public LayerMask playerLayer;
    public int damage;
    public float movementSpeed;
    public float attackCooldown;
    public float attackDistance;
    public float alertDistance;
    public float patrolTargetDistance;
}
