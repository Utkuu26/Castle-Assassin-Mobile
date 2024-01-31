using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public GameObject endGamePanel;
    public GameObject nextLevel;
    public GameObject currentLevel;
    public CharacterController characterController;
    public Button continueBtn;
    public GameObject playerGameObject;
    public Transform nextLevelSpawnPoint;
    public LevelCounter levelCounter;

    void Start() 
    {
        nextLevel.SetActive(false);
        continueBtn.onClick.AddListener(NextLevel);

        if (playerGameObject == null)
        {
            playerGameObject = GameObject.FindWithTag("Player");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            characterController.enabled = false;
            endGamePanel.SetActive(true);
        }
    }

    void NextLevel()
    {
        if (playerGameObject != null && nextLevelSpawnPoint != null)
        {
            playerGameObject.transform.position = nextLevelSpawnPoint.position;
        }
        else
        {
            Debug.LogError("Player GameObject or Target Transform is not assigned!");
        }
        
        levelCounter.UpdateLevelCounter();
        nextLevel.SetActive(true);
        endGamePanel.SetActive(false);
        characterController.enabled = true;
        currentLevel.SetActive(false);
    }
}
