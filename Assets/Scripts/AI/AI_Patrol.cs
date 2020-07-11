using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Patrol : StateMachineBehaviour
{
    private AI ai;
    private float alertDistance;
    private PlayerHealth player;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = FindObjectOfType<PlayerHealth>();
        ai = animator.GetComponent<AI>();
        alertDistance = ai.alertDistance;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (CheckDistance(alertDistance))
        {
            animator.SetBool("IsHurt", true);
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
}
