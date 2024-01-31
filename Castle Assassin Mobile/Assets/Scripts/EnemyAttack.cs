using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EnemyAttack : MonoBehaviour 
{
    public EnemyPatrol enemyPatrol;
    public Animator enemyAnimator;
    public Animator playerAnimator;
    public CharacterController characterController;
    public GameObject loseGamePanel;
    public int enemyLvl; 
    public TextMeshProUGUI enemyLvlTxt; 
    public PlayerLevel playerLevel;

    void Start()
    {
        loseGamePanel.SetActive(false);
        enemyLvlTxt.text = ("Level " + enemyLvl);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trigger")) 
        {
            if (enemyPatrol != null && (enemyLvl > playerLevel.playerLvl))
            {
                enemyPatrol.StopPatrol();
                enemyAnimator.SetBool("isEnemyAttack", true);

                characterController.enabled = false;
                playerAnimator.SetBool("isPlayerDying", true);

                Invoke("ShowLoseGamePanel", 3f);
            }
            else if (enemyLvl < playerLevel.playerLvl)
            {
                enemyPatrol.StopPatrol();
                playerAnimator.SetBool("isPlayerAttacking", true);
                enemyAnimator.SetBool("isEnemyScared", true);
                
                Invoke("EnemyKilled", 1f);
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

    void EnemyKilled()
    {
        playerAnimator.SetBool("isPlayerAttacking", false);
    }

}
