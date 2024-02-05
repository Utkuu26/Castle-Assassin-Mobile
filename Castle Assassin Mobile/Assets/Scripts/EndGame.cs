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
    public Transform[] nextLevelSpawnPoints;
    private int currentSpawnPointIndex = 0;
    public LevelCounter levelCounter;
    public AudioSource endGameAudioSource;
    public AudioClip endGamePanelSfx;


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
            levelCounter.UpdateWinScreenLevel();

            endGameAudioSource.clip = endGamePanelSfx;
            endGameAudioSource.Play();
        }
    }

    void NextLevel()
    {
        Transform nextSpawnPoint = nextLevelSpawnPoints[currentSpawnPointIndex];
        playerGameObject.transform.position = nextSpawnPoint.position;
        currentSpawnPointIndex = (currentSpawnPointIndex + 1) % nextLevelSpawnPoints.Length;

        levelCounter.UpdateLevelCounter();
        nextLevel.SetActive(true);
        endGamePanel.SetActive(false);
        characterController.enabled = true;
        currentLevel.SetActive(false);
    }

}
