using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDone : StateMachineBehaviour
{   
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        EnemyHealth enemyHealth = animator.GetComponent<EnemyHealth>();
        if (enemyHealth != null) {
            enemyHealth.remove(stateInfo.length * 0.9f);
        }
        else {
            PlayerHealth playerHealth = animator.GetComponent<PlayerHealth>();
            playerHealth.remove(stateInfo.length * 0.9f);
        }
    }
}
