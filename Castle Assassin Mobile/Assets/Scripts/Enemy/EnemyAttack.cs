using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EnemyAttack : MonoBehaviour 
{
    public delegate void EnemieDiedHandler(int diedEnemies);
    public static event EnemieDiedHandler OnEnemyDied;
    
    public EnemyPatrol enemyPatrol;
    public Animator enemyAnimator;
    public Animator playerAnimator;
    public CharacterController characterController;
    public GameObject loseGamePanel;
    public int enemyLvl; 
    public TextMeshProUGUI enemyLvlTxt; 
    public PlayerLevel playerLevel;
    public int deadEnemies = 0;
    //public GameObject bloodParticklePrefab;

    void Start()
    {
        loseGamePanel.SetActive(false);
        enemyLvlTxt.text = ("Level " + enemyLvl);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trigger")) 
        {
            if (enemyLvl > playerLevel.playerLvl)
            {
                enemyPatrol.StopPatrol();
                enemyAnimator.SetBool("isEnemyAttack", true);
                
                // Eğer OnEnemyDied event'ine bir abone varsa
                if (OnEnemyDied != null)
                {
                    OnEnemyDied(deadEnemies);
                }

                characterController.enabled = false;
                playerAnimator.SetBool("isPlayerDying", true);

                Invoke("ShowLoseGamePanel", 3f);
            }
            else if (enemyLvl < playerLevel.playerLvl)
            {
                enemyPatrol.StopPatrol();
                playerAnimator.SetBool("isPlayerAttacking", true);
                //SpawnBloodPartickle();
                enemyAnimator.SetBool("isEnemyScared", true);
                playerAnimator.SetBool("isPlayerAttacking", false);

                deadEnemies++;
                OnEnemyDied(deadEnemies);
                
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

    // void SpawnBloodPartickle()
    // {
    //     GameObject particleEffect = Instantiate(bloodParticklePrefab, transform.position, Quaternion.identity);
    //     Destroy(particleEffect, 4f);
    // }
}
