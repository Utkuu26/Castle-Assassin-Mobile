using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelUp : MonoBehaviour
{
    public delegate void LevelUpHandler(int newLevel);
    public static event LevelUpHandler OnLevelUp;

    public TextMeshProUGUI playerLvlTxt;
    public GameObject starParticklePrefab; 
    private static int playerLvl = 0;
    public GameObject maxlvlTxt;

    public AudioSource lvlUpAudioSource;
    public AudioClip lvlUpSfx;

    void Start()
    {
        playerLvlTxt.text = ("Level " + playerLvl);
        maxlvlTxt.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trigger"))
        {
            if (playerLvl <= 10)
            {
                playerLvl = playerLvl + 5;
                playerLvlTxt.text = ("Level " + playerLvl);
            }
            else
            {
                maxlvlTxt.SetActive(true);
            }
            
            SpawnStarPartickle();
            Destroy(gameObject); 

            lvlUpAudioSource.clip = lvlUpSfx;
            lvlUpAudioSource.Play();

            // LevelUp event'ini tetikle
            if (OnLevelUp != null)
                OnLevelUp(playerLvl);
        }
    }

    void SpawnStarPartickle()
    {
        GameObject particleEffect = Instantiate(starParticklePrefab, transform.position, Quaternion.identity);
        Destroy(particleEffect, 2f);
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
