﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Shoot : StateMachineBehaviour
{
    private AI ai;
    private Rigidbody2D rigidbody;
    private PlayerMovement playerMovement;
    private Rigidbody2D playersRigidbody;
    private bool canShoot;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ai = animator.GetComponent<AI>();
        rigidbody = ai.GetComponent<Rigidbody2D>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        playersRigidbody = playerMovement.GetComponent<Rigidbody2D>();

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (CheckDistance(ai.alertDistance, playerMovement.transform.position))
        {
            if (canShoot ) // Set attack Cooldown
            {
                Shoot();
            }
        }
        else
        {
            animator.SetBool("IsAlert", false);
        }
    }

    bool CheckDistance(float distance, Vector2 target)
    {
        if (Vector2.Distance(target, ai.transform.position) < distance)
        {
            return true;
        }
        return false;
    }

    void Shoot ()
    {
        // set gun origin rotation to be direction to player

        //set it to aim the direction the player is moving and is expected to be by time bullet hits


        AI_Bullet bullet = Instantiate(ai.bulletPrefab,
                   ai.bulletSpawnPoint.position,
                   ai.aimOriginTransform.rotation);

        bullet.Initialise(ai.damage, ai.bulletForce, ai.bulletDeviation);
    }
}
