using UnityEngine;

public class EnemiesMustKill : MonoBehaviour
{
    private Collider lockedDoorCollider;
    public EnemyAttack[] enemies;
    public int deadEnemies = 0;

    void Start()
    {
        // Kapının Collider'ını al
        lockedDoorCollider = GetComponent<Collider>();
        lockedDoorCollider.enabled = true;

        // Her düşmanı takip etmek için event'e abone ol
        foreach (EnemyAttack enemy in enemies)
        {
            // Her düşmanın event'ine abone ol
            EnemyAttack.OnEnemyDied += UpdateDeadEnemies;
        }
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
        // Her düşmandan aboneliği kaldır
        foreach (EnemyAttack enemy in enemies)
        {
            // Her düşmanın event'inden aboneliği kaldır
            EnemyAttack.OnEnemyDied -= UpdateDeadEnemies;

        }
    }
}
