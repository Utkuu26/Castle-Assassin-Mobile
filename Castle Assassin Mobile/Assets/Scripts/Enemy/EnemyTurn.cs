using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurn : MonoBehaviour
{
    private Transform enemyTransform;
    private Quaternion initialRotation;
    private bool stopState = false;
    private float rotationSpeed = 90f;

    void Start()
    {
        enemyTransform = transform;
        initialRotation = enemyTransform.rotation;
        InvokeRepeating("ToggleRotateState", 3f, 6f); // Her 3 saniyede bir ToggleRotateState fonksiyonunu çağır
    }

    void Update()
    {
        if (!stopState)
        {
            // Düşmanın izleme mantığı burada implemente edilebilir
            // Örneğin: enemyTransform.LookAt(targetPosition);
        }
    }

    void ToggleRotateState()
    {
        stopState = !stopState; // Döndürme durumunu tersine çevir

        if (stopState)
        {
            StartCoroutine(Rotate180Degrees());
        }
        else
        {
            StartCoroutine(RotateToInitialPosition());
        }
    }

    IEnumerator Rotate180Degrees()
    {
        float elapsedTime = 0f;
        Quaternion startRotation = enemyTransform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0f, initialRotation.eulerAngles.y + 180f, 0f);

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * (rotationSpeed / 180f);
            enemyTransform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime);
            yield return null;
        }

        enemyTransform.rotation = targetRotation; // Kesin rotasyonu ayarla
    }

    IEnumerator RotateToInitialPosition()
    {
        float elapsedTime = 0f;
        Quaternion startRotation = enemyTransform.rotation;
        Quaternion targetRotation = initialRotation;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * (rotationSpeed / 180f);
            enemyTransform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime);
            yield return null;
        }

        enemyTransform.rotation = targetRotation; // Kesin rotasyonu ayarla
    }
}
