using System.Collections;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    int currentPoint;
    [SerializeField] private float minPatrolSpeed;
    [SerializeField] private float maxPatrolSpeed;
    [SerializeField] private float accelerationRate;
    [SerializeField] private float waitTime = 1.0f;

    float currentSpeed;

    // Start is called before the first frame update
    void Start()
    {
        currentPoint = 0;
        currentSpeed = minPatrolSpeed;

        StartCoroutine(PatrolRoutine());
    }

    IEnumerator PatrolRoutine()
    {
        while (true)
        {
            yield return MoveToNextPoint();
            yield return new WaitForSeconds(waitTime);
        }
    }

    IEnumerator MoveToNextPoint()
    {
        while (transform.position != patrolPoints[currentPoint].position)
        {
            float distanceToTarget = Vector3.Distance(transform.position, patrolPoints[currentPoint].position);
            float lerpFactor = Mathf.Clamp01(distanceToTarget / maxPatrolSpeed);

            float targetSpeed = Mathf.Lerp(minPatrolSpeed, maxPatrolSpeed, lerpFactor);
            currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, accelerationRate * Time.deltaTime);

            float step = currentSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPoint].position, step);

            yield return null;
        }

        currentPoint = (currentPoint + 1) % patrolPoints.Length;
    }
}
