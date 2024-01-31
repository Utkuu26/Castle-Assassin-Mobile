using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAttack : MonoBehaviour
{
    public EnemyPatrol enemyPatrol;
    public Animator enemyAnimator;
    public Animator playerAnimator;
    public CharacterController characterController;
    public GameObject loseGamePanel;

    void Start()
    {
        loseGamePanel.SetActive(false);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyTrigger")) 
        {
            if (enemyPatrol != null)
            {
                enemyPatrol.StopPatrol();
                enemyAnimator.SetBool("isEnemyAttack", true);

                characterController.enabled = false;
                playerAnimator.SetBool("isPlayerDying", true);

                Invoke("ShowLoseGamePanel", 3f);
            }
        }
    }

    void ShowLoseGamePanel()
    {
        loseGamePanel.SetActive(true);
        playerAnimator.SetBool("isPlayerDying", false);
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
