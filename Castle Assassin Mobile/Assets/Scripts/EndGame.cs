using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public GameObject endGamePanel;
    public GameObject nextLevel;
    private bool isGameEnded = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isGameEnded)
        {
            // Karakter trigger'a girdiğinde çalışacak kod
            Time.timeScale = 0f; // Oyunu durdur

            endGamePanel.SetActive(true); // endGamePaneli aktif hale getir

            StartCoroutine(EndGameWait());
        }
    }

    IEnumerator EndGameWait()
    {
        yield return new WaitForSeconds(5f); // 5 saniye bekle

        endGamePanel.SetActive(false); // endGamePaneli devre dışı bırak
        Time.timeScale = 1f; // Oyunu devam ettir

        if (nextLevel != null)
        {
            nextLevel.SetActive(true); // Başka bir GameObject'i aktif hale getir
        }

        isGameEnded = true; // Trigger'a girildiği bilgisini kaydet
    }
}
