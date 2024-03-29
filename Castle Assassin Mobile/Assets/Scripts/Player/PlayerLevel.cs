using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    public int playerLvl = 0;

    void Start()
    {
        // LevelUp sınıfındaki event'e abone ol
        LevelUp.OnLevelUp += UpdatePlayerLevel;
    }

    // LevelUp sınıfındaki event tetiklendiğinde çağrılacak fonksiyon
    private void UpdatePlayerLevel(int newLevel)
    {
        playerLvl = newLevel;
    }

    // Abonelikten kaldırma işlemi
    private void OnDestroy()
    {
        LevelUp.OnLevelUp -= UpdatePlayerLevel;
    }
}
