using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_MeleeAttack : StateMachineBehaviour
{
    private AI ai;
    private PlayerHealth playerHealth;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ai = animator.GetComponent<AI>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        Attack();
    }

    void Attack ()
    {
        Vector2 directionToPlayer = playerHealth.transform.position - ai.transform.position;
        if (Physics2D.OverlapCircle(ai.transform.position + new Vector3(directionToPlayer.x * 0.5f , directionToPlayer.y * 0.5f, 0), ai.attackDistance, ai.playerLayer))
        {
            playerHealth.TakeDamage(ai.damage);
        }
    }
}
