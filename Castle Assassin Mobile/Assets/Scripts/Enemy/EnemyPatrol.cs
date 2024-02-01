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
    private Animator enemyAnimator;
    //private Transform textTransform;

    private bool isPatrolling = true;

    void Start()
    {
        currentPoint = 0;
        currentSpeed = minPatrolSpeed;

        enemyAnimator = GetComponent<Animator>();
        //textTransform = transform.Find("EnemyTxt").transform;

        StartCoroutine(PatrolRoutine());
    }

    public void StopPatrol()
    {
        isPatrolling = false;
        enemyAnimator.SetBool("IsWalking", false);
        StopAllCoroutines();
    }

    IEnumerator PatrolRoutine()
    {
        while (isPatrolling)
        {
            yield return MoveToNextPoint();
            yield return new WaitForSeconds(waitTime);
        }
    }

    IEnumerator MoveToNextPoint()
    {
        Vector3 targetPosition = patrolPoints[currentPoint].position;

        enemyAnimator.SetBool("IsWalking", true);

        while (transform.position != targetPosition)
        {
            float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
            float lerpFactor = Mathf.Clamp01(distanceToTarget / maxPatrolSpeed);

            float targetSpeed = Mathf.Lerp(minPatrolSpeed, maxPatrolSpeed, lerpFactor);
            currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, accelerationRate * Time.deltaTime);

            float step = currentSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            if (moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10.0f);
            }

            // Text'in konumunu güncelle (Y ekseni sabit kalmalı)
           // Vector3 textPosition = new Vector3(textTransform.position.x, textTransform.position.y, textTransform.position.z);
           // textTransform.position = textPosition;

            yield return null;
        }

        enemyAnimator.SetBool("IsWalking", false);

        currentPoint = (currentPoint + 1) % patrolPoints.Length;
    }
}
