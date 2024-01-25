using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    public GameObject uiPanel;
    public GameObject keyImg1;
    public GameObject keyImg2;

    private bool uiPanelOpened = false;
    private bool key1Collected = false;
    private bool key2Collected = false;

    void Start()
    {
        uiPanel.SetActive(false);
        keyImg1.SetActive(false);
        keyImg2.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Key1") && !key1Collected)
            {
                key1Collected = true;
                keyImg1.SetActive(true);
                OpenUIPanel();
                gameObject.SetActive(false);
            }
            else if (gameObject.CompareTag("Key2") && !key2Collected)
            {
                key2Collected = true;
                keyImg2.SetActive(true);
                OpenUIPanel();
                gameObject.SetActive(false);
            }
        }
    }

    void OpenUIPanel()
    {
        if (uiPanel != null)
        {
            uiPanel.SetActive(true);
        }
    }
}
