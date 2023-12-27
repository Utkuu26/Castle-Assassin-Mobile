using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HelperMovement : MonoBehaviour
{
    [SerializeField] private CubeSpawner cubeSpawner;

    public NavMeshAgent helperAgent;
    public ZoneController zoneController;
    public HelperStacker helperStacker;
    public Animator animator;
    private Vector3 target;
    private Vector3 lastPosition;
    private float blendTreeSpeed;
    void Start()
    {
        helperAgent = GetComponent<NavMeshAgent>();
        helperStacker = GetComponent<HelperStacker>();

    }
    private void OnEnable()
    {
        target = cubeSpawner.TargetPoint1.position;
        helperAgent.SetDestination(target);
    }


    void Update()
    {

        if (target == cubeSpawner.TargetPoint1.position && (transform.position - target).magnitude < 1f)
        {
            target = cubeSpawner.TargetPoint2.position;
            helperAgent.SetDestination(target);

        }
        else if (target == cubeSpawner.TargetPoint2.position && (transform.position - target).magnitude < 1f && zoneController.itemInStorage < 40)
        {
            target = zoneController.Storage.transform.position;
            helperAgent.SetDestination(target);

        }
        else if (target == zoneController.Storage.transform.position && (transform.position - target).magnitude < 1.3f)
        {
            animator.SetBool("isRun", false);
            WaitForStackReload();
        }
        else if ((helperAgent.transform.position-cubeSpawner.TargetPoint2.position).magnitude < 0.8f && zoneController.itemInStorage == 40)
        {
            animator.SetBool("isRun", false);
        }
       
        lastPosition = transform.position;
        
    }


    public void WaitForStackReload()
    {
        StartCoroutine(Delay());
        IEnumerator Delay()
        {
            yield return new WaitForSeconds(1.5f);
            target = cubeSpawner.TargetPoint1.position;
            animator.SetBool("isRun", true);
            helperAgent.SetDestination(target);

        }
    }

    public void WaitForOtherHelper()
    {
        StartCoroutine(Delay());
        IEnumerator Delay()
        {
            yield return new WaitForSeconds(1.5f);
            helperAgent.SetDestination(lastPosition);
        }

    }

}