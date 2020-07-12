using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class AI_Patrol : StateMachineBehaviour
{
    private AI ai;
    private Rigidbody2D rigidbody;
    private PlayerHealth player;
    
    private float alertDistance;
    private Vector2 target;
    private float movementSpeed;

    private float timer;
    private float timeToResetTarget = 10;



    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = FindObjectOfType<PlayerHealth>();
        ai = animator.GetComponent<AI>();
        rigidbody = ai.GetComponent<Rigidbody2D>();
        SetTarget();
        alertDistance = ai.alertDistance;
        movementSpeed = ai.movementSpeed;
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player == null)
        {
            return;
        }
        if (CheckDistance(alertDistance, player.transform.position))
        {
            animator.SetBool("IsAlert", true);
        }
        if (CheckDistance(0.1f, target))
        {
            SetTarget();
        }
        else
        {
            Vector2 pos = new Vector2(ai.transform.position.x, ai.transform.position.y);
            Vector2 direction = (target - pos).normalized;
            rigidbody.velocity = direction * movementSpeed * Time.fixedDeltaTime;
            if (TargetTimer())
            {
                SetTarget();
            }
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

    bool TargetTimer()
    {
        if (timer <= 0.0f)
        {
            return true;
        }
        else
        {
            timer -= Time.fixedDeltaTime;
            return false;
        }
    }

    void SetTarget ()
    {
        target = GameManager.instance.GetClearLocationOnMap(-ai.patrolTargetDistance + ai.transform.position.x, ai.patrolTargetDistance + ai.transform.position.x,
            -ai.patrolTargetDistance + ai.transform.position.y, ai.patrolTargetDistance + ai.transform.position.y);
        timer = timeToResetTarget;
    }
}
