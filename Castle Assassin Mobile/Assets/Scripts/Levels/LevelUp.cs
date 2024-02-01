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

    private int playerLvl = 0;

    void Start()
    {
        playerLvlTxt.text = ("Level " + (playerLvl + 1));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trigger"))
        {
            playerLvl = playerLvl + 5;
            playerLvlTxt.text = ("Level " + playerLvl);

            SpawnStarPartickle();
            Destroy(gameObject); 

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
}
