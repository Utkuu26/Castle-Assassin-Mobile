using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public EnemyPatrol enemyPatrol;
    public Animator enemyAnimator;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyTrigger")) 
        {
            if (enemyPatrol != null)
            {
                enemyPatrol.StopPatrol();
                enemyAnimator.SetBool("isEnemyAttack", true);
            }
        }
    }
}
