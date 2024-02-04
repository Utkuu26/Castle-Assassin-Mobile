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

    public GameObject lockedImg1;
    public GameObject lockedImg2;
    public bool isDoorLocked = true;

    public GameObject lockParticklePrefab; 
    public GameObject keyParticklePrefab; 

    public AudioSource keyAudioSource;
    public AudioClip keySfx;

    void Start()
    {
        uiPanel.SetActive(false);
        keyImg1.SetActive(false);
        keyImg2.SetActive(false);

        if(lockedImg1 != null && lockedImg2 != null)
        {
            lockedImg1.SetActive(true);
            lockedImg2.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Key1") && !key1Collected)
            {
                SpawnKeyPartickle();
                key1Collected = true;
                keyImg1.SetActive(true);
                OpenUIPanel();
                gameObject.SetActive(false);
                lockedImg1.SetActive(false);
                SpawnUnlockPartickle();
                PlaykeySfx();
            }
            else if (gameObject.CompareTag("Key2") && !key2Collected)
            {
                SpawnKeyPartickle();
                isDoorLocked = false;
                key2Collected = true;
                keyImg2.SetActive(true);
                OpenUIPanel();
                gameObject.SetActive(false);
                lockedImg2.SetActive(false);
                SpawnUnlockPartickle();
                PlaykeySfx();
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

    void SpawnUnlockPartickle()
    {
        if(!lockedImg1.activeSelf)
        {
            GameObject particleEffect = Instantiate(lockParticklePrefab, lockedImg1.transform.position, Quaternion.identity);
            Destroy(particleEffect, 2f);
        }
        else if(!lockedImg2.activeSelf)
        {
            GameObject particleEffect = Instantiate(lockParticklePrefab, lockedImg2.transform.position, Quaternion.identity);
            Destroy(particleEffect, 2f);
        }
    }

    void SpawnKeyPartickle()
    {
        GameObject particleEffect = Instantiate(keyParticklePrefab, transform.position, Quaternion.identity);
        Destroy(particleEffect, 2f);
    }

    void PlaykeySfx()
    {
        keyAudioSource.clip = keySfx;
        keyAudioSource.Play();
    }

}
