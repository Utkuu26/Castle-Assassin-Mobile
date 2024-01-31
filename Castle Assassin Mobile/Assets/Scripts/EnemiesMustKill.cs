using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesMustKill : MonoBehaviour
{
    private Collider lockedDoorCollider;
    public EnemyAttack enemyAttack;
    public int deadEnemies = 0;

    void Start()
    {
        // EnemyAttack sınıfındaki event'e abone ol
        EnemyAttack.OnEnemyDied += UpdateDeadEnemies;
        lockedDoorCollider = GetComponent<Collider>();
        lockedDoorCollider.enabled = true;
    }

    // EnemyAttack sınıfındaki event tetiklendiğinde çağrılacak fonksiyon
    private void UpdateDeadEnemies(int diedEnemies)
    {
        // deadEnemies değerini güncelle
        this.deadEnemies = diedEnemies;

        // Diğer işlemleri burada gerçekleştir
        // Örneğin:
        // Debug.Log("Dead Enemies Updated: " + this.deadEnemies);

        // deadEnemies 2 ise kapıyı kapat
        if (this.deadEnemies == 2)
        {
            lockedDoorCollider.enabled = false;
        }
    }

    // Abonelikten kaldırma işlemi
    private void OnDestroy()
    {
        EnemyAttack.OnEnemyDied -= UpdateDeadEnemies;
    }
}
