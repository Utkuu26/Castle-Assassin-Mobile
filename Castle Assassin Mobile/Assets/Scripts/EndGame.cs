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

    void Start() 
    {
        nextLevel.SetActive(false);
        continueBtn.onClick.AddListener(NextLevel);
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
            endGamePanel.SetActive(false);
            characterController.enabled = true;
        }
    }
}
