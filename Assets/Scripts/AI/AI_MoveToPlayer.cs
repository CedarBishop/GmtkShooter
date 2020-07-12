using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_MoveToPlayer : StateMachineBehaviour
{
    private AI ai;
    private float movementSpeed;
    private float attackDistance;
    private float attackCooldownTimer;
    private PlayerHealth player;
    private Rigidbody2D rigidbody;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ai = animator.GetComponent<AI>();
        player = FindObjectOfType<PlayerHealth>();
        rigidbody = ai.GetComponent<Rigidbody2D>();
        attackDistance = ai.attackDistance;
        movementSpeed = ai.movementSpeed;
        attackCooldownTimer = ai.attackCooldown;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (player == null)
        {
            return;
        }
        if (CheckDistance(attackDistance))
        {
            rigidbody.velocity = Vector2.zero;
            rigidbody.angularVelocity = 0;
            if (AttackCooldown())
            {
                animator.SetTrigger("Attack");
            }
        }
        else
        {
            Vector2 directionToPlayer = (player.transform.position - ai.transform.position).normalized;
            rigidbody.velocity = directionToPlayer * ai.movementSpeed * Time.fixedDeltaTime;
        }

        if (CheckDistance(ai.alertDistance) == false)
        {
            animator.SetBool("IsAlert", false);
        }
    }

    bool CheckDistance(float distance)
    {
        if (Vector2.Distance(player.transform.position, ai.transform.position) < distance)
        {
            return true;
        }
        return false;
    }

    bool AttackCooldown ()
    {
        if (attackCooldownTimer <= 0.0f)
        {
            return true;
        }
        else
        {
            attackCooldownTimer -= Time.fixedDeltaTime;
            return false;
        }
    }
}
