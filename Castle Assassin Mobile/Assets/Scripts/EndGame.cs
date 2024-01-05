using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public GameObject endGamePanel;
    public GameObject nextLevel;
    public GameObject currentLevel;
    public CharacterController characterController;


    void Start() 
    {
        nextLevel.SetActive(false);
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
        if (nextLevel != null)
        {
            nextLevel.SetActive(true);
            currentLevel.SetActive(false);
            characterController.enabled = true;
        }
    }
}
