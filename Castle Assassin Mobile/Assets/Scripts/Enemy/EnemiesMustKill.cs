using UnityEngine;

public class EnemiesMustKill : MonoBehaviour
{
    private Collider lockedDoorCollider;
    public EnemyAttack[] enemies;
    public int deadEnemies = 0;

    public GameObject lockParticklePrefab; 

    public GameObject lockedImg1;
    public GameObject lockedImg2;

    void Start()
    {
        lockedDoorCollider = GetComponent<Collider>();
        lockedDoorCollider.enabled = true;

        // Her düşmanı takip etmek için event'e abone ol
        foreach (EnemyAttack enemy in enemies)
        {
            // Her düşmanın event'ine abone ol
            EnemyAttack.OnEnemyDied += UpdateDeadEnemies;
        }

        if(lockedImg1 != null && lockedImg2 != null)
        {
            lockedImg1.SetActive(true);
            lockedImg2.SetActive(true);
        }
    }

    // EnemyAttack sınıfındaki event tetiklendiğinde çağrılacak fonksiyon
    private void UpdateDeadEnemies(int diedEnemies)
    {
        // deadEnemies değerini güncelle
        this.deadEnemies = diedEnemies;

        // deadEnemies 2 ise kapıyı kapat
        if (this.deadEnemies == 1)
        {
            SpawnUnlockPartickle();
            lockedImg1.SetActive(false);
        }
        if (this.deadEnemies == 2)
        {
            lockedDoorCollider.enabled = false;
            SpawnUnlockPartickle();
            lockedImg2.SetActive(false);
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
}
