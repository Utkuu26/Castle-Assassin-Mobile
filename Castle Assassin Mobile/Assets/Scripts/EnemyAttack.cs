using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public EnemyPatrol enemyPatrol;
    public Animator enemyAnimator;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("InvisibleHand")) 
        {
            if (enemyPatrol != null)
            {
                enemyPatrol.StopPatrol();
            }

            if (enemyAnimator != null)
            {
                enemyAnimator.SetTrigger("Attack");
            }
        }
    }
}
