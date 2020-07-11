using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Patrol : StateMachineBehaviour
{
    private AI ai;
    private Rigidbody2D rigidbody;
    private PlayerHealth player;
    
    private float alertDistance;
    private Vector2 target;
    private float movementSpeed;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = FindObjectOfType<PlayerHealth>();
        ai = animator.GetComponent<AI>();
        rigidbody = ai.GetComponent<Rigidbody2D>();
        target = SetPatrolTarget();
        alertDistance = ai.alertDistance;
        movementSpeed = ai.movementSpeed;
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (CheckDistance(alertDistance, player.transform.position))
        {
            animator.SetBool("IsAlert", true);
        }
        if (CheckDistance(0.01f, target))
        {
            SetPatrolTarget();
        }
        else
        {
            Vector2 direction = (target - new Vector2(ai.transform.position.x, ai.transform.position.y)).normalized;
            rigidbody.velocity = direction * movementSpeed * Time.fixedDeltaTime;
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

    Vector2 SetPatrolTarget ()
    {
        Vector2 pos;
        do
        {
            pos = new Vector2(Random.Range(-ai.patrolTargetDistance, ai.patrolTargetDistance), Random.Range(-ai.patrolTargetDistance, ai.patrolTargetDistance));
        } while (GameManager.instance.IsWithinMap(pos) == false);
        Debug.Log(pos);
        return pos;
    }
}
